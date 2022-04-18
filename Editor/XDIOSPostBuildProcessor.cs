#if UNITY_EDITOR && UNITY_IOS
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;

namespace XD.Cn.Common.Editor{
    public static class XDIOSPostBuildProcessor{
        public static string plistName = "/XD-Info.plist";
    
        [PostProcessBuild(104)]
        public static void OnPostprocessBuild(BuildTarget BuildTarget, string path){
            if (BuildTarget == BuildTarget.iOS){
                Debug.Log("开始执行  XDIOSPostBuildProcessor");
                // 获得工程路径
                var projPath = PBXProject.GetPBXProjectPath(path);
                var proj = new PBXProject();
                proj.ReadFromString(File.ReadAllText(projPath));
    
                // 2019.3以上有多个target
#if UNITY_2019_3_OR_NEWER
                string unityFrameworkTarget = proj.TargetGuidByName("UnityFramework");
                string target = proj.GetUnityMainTargetGuid();
#else
                string unityFrameworkTarget = proj.TargetGuidByName("Unity-iPhone");
                string target = proj.TargetGuidByName("Unity-iPhone");
#endif

                if (target == null || unityFrameworkTarget == null){
                    Debug.LogError("XDIOSPostBuildProcessor target 是空");
                    return;
                }
                
                proj.AddBuildProperty(target, "OTHER_LDFLAGS", "-ObjC");
                proj.AddBuildProperty(unityFrameworkTarget, "OTHER_LDFLAGS", "-ObjC ");
                proj.AddFrameworkToProject(unityFrameworkTarget, "Accelerate.framework", false);

                // 添加资源文件，注意文件路径
                var resourcePath = Path.Combine(path, "XDResource");
                var parentFolder = Directory.GetParent(Application.dataPath)?.FullName;
                if (Directory.Exists(resourcePath)){
                    Directory.Delete(resourcePath, true);
                }

                Directory.CreateDirectory(resourcePath);
                Debug.Log("创建文件夹: " + resourcePath);

                //拷贝资源文件,可能拷贝多个模块，这里只有common有资源
               copyResource(target, projPath, proj, parentFolder, "com.xd.cn.common", "Common", 
                 resourcePath, new[]{"XDResources.bundle"});

                // 复制Assets的plist到工程目录
                File.Copy(parentFolder + "/Assets/Plugins" + plistName, resourcePath + plistName);

                //修改plist
                SetPlist(path, resourcePath + plistName);

                //插入代码片段
                SetScriptClass(path);
                Debug.Log("XDIOSPostBuildProcessor Xcode信息配置成功");
            }
        }

        private static void copyResource(string target, string projPath, PBXProject proj, string parentFolder,
            string npmModuleName, string localModuleName, string xcodeResourceFolder, string[] bundleNames){
           
            //拷贝文件夹里的资源
            var tdsResourcePath = XDFileHelper.FilterFile(parentFolder + "/Library/PackageCache/", $"{npmModuleName}@");
            if (string.IsNullOrEmpty(tdsResourcePath)){ //优先使用npm的，否则用本地的
                tdsResourcePath = parentFolder + "/Assets/XD/Cn/" + localModuleName;
            }
            tdsResourcePath = tdsResourcePath + "/Plugins/iOS/Resource";
            
            Debug.Log("资源路径" + tdsResourcePath);
            if (!Directory.Exists(tdsResourcePath) || tdsResourcePath == ""){
                Debug.LogError("需要拷贝的资源路径不存在");
                return;
            }
            
            XDFileHelper.CopyAndReplaceDirectory(tdsResourcePath, xcodeResourceFolder);
            foreach (var name in bundleNames){
                proj.AddFileToBuild(target,
                    proj.AddFile(Path.Combine(xcodeResourceFolder, name), Path.Combine(xcodeResourceFolder, name),
                        PBXSourceTree.Source));
            }
            File.WriteAllText(projPath, proj.WriteToString()); //保存
        }

        private static void SetPlist(string pathToBuildProject, string infoPlistPath){
            //添加info
            string _plistPath = pathToBuildProject + "/Info.plist"; //Xcode工程的Info.plist
            PlistDocument _plist = new PlistDocument();
            _plist.ReadFromString(File.ReadAllText(_plistPath));
            PlistElementDict _rootDic = _plist.root;

            List<string> items = new List<string>(){
                "weixinULAPI",
                "mqqopensdknopasteboard",
                "mqzone",
                "tapiosdk",
                "tapsdk",
                "mqqwpa",
                "wechat",
                "weixin",
                "mqqapi",
                "mqq",
                "mqqOpensdkSSoLogin",
                "mqqconnect",
                "mqqopensdkdataline",
                "mqqopensdkgrouptribeshare",
                "mqqopensdkfriend",
                "mqqopensdkapi",
                "mqqopensdkapiV2",
                "mqqopensdkapiV3",
                "mqqopensdkapiV4",
                "mqzoneopensdk",
                "wtloginmqq",
                "wtloginmqq2",
                "mqqgamebindinggroup",
                "tim"
            };
            
            //添加 Scheme, 添加 不覆盖
            PlistElementArray _list = null;
            foreach (var item in _rootDic.values){
                if (item.Key.Equals("LSApplicationQueriesSchemes")){
                    _list = (PlistElementArray) item.Value;
                    break;
                }
            }
            if (_list == null){
                _list = _rootDic.CreateArray("LSApplicationQueriesSchemes");
            }
            
            for (int i = 0; i < items.Count; i++){
                _list.AddString(items[i]);
            }

            Dictionary<string, object> dic = (Dictionary<string, object>) Plist.readPlist(infoPlistPath);
            string taptapId = null;
            string qqId = null;
            string weixinId = null;

            //读取配置的id 
            foreach (var item in dic){
                if (item.Key.Equals("taptap")){
                    Dictionary<string, object> taptapDic = (Dictionary<string, object>) item.Value;
                    foreach (var taptapItem in taptapDic){
                        if (taptapItem.Key.Equals("value")){
                            taptapId = (string) taptapItem.Value;
                        }
                    }
                }
                else if (item.Key.Equals("qq")){
                    Dictionary<string, object> googleDic = (Dictionary<string, object>) item.Value;
                    foreach (var googleItem in googleDic){
                        if (googleItem.Key.Equals("value")){
                            qqId = (string) googleItem.Value;
                        }
                    }
                }
                else if (item.Key.Equals("weixin")){
                    Dictionary<string, object> twitterDic = (Dictionary<string, object>) item.Value;
                    foreach (var twitterItem in twitterDic){
                        if (twitterItem.Key.Equals("value")){
                            weixinId = (string) twitterItem.Value;
                        }
                    }
                }
            }

            //添加url 添加 不覆盖
            PlistElementDict dict = _plist.root.AsDict();
            PlistElementArray array = null;
            foreach (var item in _rootDic.values){
                if (item.Key.Equals("CFBundleURLTypes")){
                    array = (PlistElementArray) item.Value;
                    break;
                }
            }
            if (array == null){
                array = dict.CreateArray("CFBundleURLTypes");
            }
            
            PlistElementDict dict2 = array.AddDict();
            if (taptapId != null){
                dict2.SetString("CFBundleURLName", "TapTap");
                PlistElementArray array2 = dict2.CreateArray("CFBundleURLSchemes");
                array2.AddString(taptapId);
            }

            if (qqId != null){
                dict2 = array.AddDict();
                dict2.SetString("CFBundleURLName", "QQ");
                PlistElementArray array2 = dict2.CreateArray("CFBundleURLSchemes");
                array2 = dict2.CreateArray("CFBundleURLSchemes");
                array2.AddString(qqId);
            }

            if (weixinId != null){
                dict2 = array.AddDict();
                dict2.SetString("CFBundleURLName", "WeiXin");
                PlistElementArray array2 = dict2.CreateArray("CFBundleURLSchemes");
                array2 = dict2.CreateArray("CFBundleURLSchemes");
                array2.AddString(weixinId);
            }
            
            File.WriteAllText(_plistPath, _plist.WriteToString());
        }

        private static void SetScriptClass(string pathToBuildProject){
            //读取Xcode中 UnityAppController.mm文件
            string unityAppControllerPath = pathToBuildProject + "/Classes/UnityAppController.mm";
            XDScriptHandlerProcessor UnityAppController = new XDScriptHandlerProcessor(unityAppControllerPath);

            //在指定代码后面增加一行代码
            UnityAppController.WriteBelow(@"#include <assert.h>", @"#import <XDCommonSDK/XDSDK.h>");
            UnityAppController.WriteBelow(@"[KeyboardDelegate Initialize];",
                @"[XDSDK application:application didFinishLaunchingWithOptions:launchOptions];");
            UnityAppController.WriteBelow(@"AppController_SendNotificationWithArg(kUnityOnOpenURL, notifData);",
                @"[XDSDK application:app openURL:url options:options];");
            
            if (CheckoutUniversalLinkHolder(unityAppControllerPath, @"NSURL* url = userActivity.webpageURL;")){
                UnityAppController.WriteBelow(@"NSURL* url = userActivity.webpageURL;",
                    @"[XDSDK application:application continueUserActivity:userActivity restorationHandler:restorationHandler];");
            }
            else{
                UnityAppController.WriteBelow(@"- (void)preStartUnity               {}",
                    @"-(BOOL) application:(UIApplication *)application continueUserActivity:(NSUserActivity *)userActivity restorationHandler:(void (^)(NSArray<id<UIUserActivityRestoring>> * _Nullable))restorationHandler{[XDSDK application:application continueUserActivity:userActivity restorationHandler:restorationHandler];return YES;}");
            }

            UnityAppController.WriteBelow(@"handler(UIBackgroundFetchResultNoData);",
                @"[XDSDK application:application didReceiveRemoteNotification:userInfo fetchCompletionHandler:completionHandler];");
            
            UnityAppController.WriteBelow(@"- (void)preStartUnity               {}",
                @"- (void)scene:(UIScene *)scene openURLContexts:(NSSet<UIOpenURLContext *> *)URLContexts API_AVAILABLE(ios(13.0)){
                        [XDSDK scene:scene openURLContexts:URLContexts];
                    }

                    - (void)scene:(UIScene *)scene continueUserActivity:(NSUserActivity *)userActivity  API_AVAILABLE(ios(13.0)){
                         [XDSDK scene:scene continueUserActivity:userActivity];
                    }"
                );
            
            UnityAppController.WriteBelow(@"- (NSUInteger)application:(UIApplication*)application supportedInterfaceOrientationsForWindow:(UIWindow*)window
{",
                @" return  [XDSDK application:application supportedInterfaceOrientationsForWindow:window];");
        }

        private static bool CheckoutUniversalLinkHolder(string filePath, string below){
            StreamReader streamReader = new StreamReader(filePath);
            string all = streamReader.ReadToEnd();
            streamReader.Close();
            int beginIndex = all.IndexOf(below, StringComparison.Ordinal);
            return beginIndex != -1;
        }

        private static string GetValueFromPlist(string infoPlistPath, string key){
            if (infoPlistPath == null){
                return null;
            }

            Dictionary<string, object> dic = (Dictionary<string, object>) Plist.readPlist(infoPlistPath);
            foreach (var item in dic){
                if (item.Key.Equals(key)){
                    return (string) item.Value;
                }
            }
            
            return null;
        }
    }
}
#endif