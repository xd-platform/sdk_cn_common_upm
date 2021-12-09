# XD-Intl Common使用： 参考[Unity-demo](https://github.com/suguiming/Unity-demo)

## 1.在Packages/manifest.json中加入如下引用
common依赖其他六个
```
"com.xd.intl.common": "https://github.com/suguiming/XDGCommon.git#{version}",
"com.leancloud.realtime": "https://github.com/leancloud/csharp-sdk-upm.git#realtime-{version}",
"com.leancloud.storage": "https://github.com/leancloud/csharp-sdk-upm.git#storage-{version}",
"com.taptap.tds.bootstrap": "https://github.com/TapTap/TapBootstrap-Unity.git#{version}",
"com.taptap.tds.common": "https://github.com/TapTap/TapCommon-Unity.git#{version}",
"com.taptap.tds.login": "https://github.com/TapTap/TapLogin-Unity.git#{version}",
"com.taptap.tds.tapdb": "https://github.com/TapTap/TapDB-Unity.git#{version}",
```

依赖的仓库地址
* [TapTap.Common](https://github.com/TapTap/TapCommon-Unity.git)
* [TapTap.Bootstrap](https://github.com/TapTap/TapBootstrap-Unity.git)
* [TapTap.Login](https://github.com/TapTap/TapLogin-Unity.git)
* [TapTap.TapDB](https://github.com/TapTap/TapDB-Unity.git)
* [LeanCloud](https://github.com/leancloud/csharp-sdk-upm)


## 2.配置SDK
#### iOS配置
* 将TDS-Info.plist 放在 /Assets/Plugins 中
* 将XD-Info.plist 放在 /Assets/Plugins 中
* 在Capabilities中打开In-App Purchase、Push Notifications、Sign In With Apple功能

#### Android配置
* 将XDG_info.json、google-Service.json 文件放在 /Assets/Plugins/Android/assets中

## 3.命名空间

```
using XD.Cn.Common;
```

## 4.接口使用
#### 切换语言
```
XDGCommon.SetLanguage(LangType.ZH_CN);
```

#### 初始化SDK
使用sdk前需先初始化
```
 XDGCommon.InitSDK((success => {
                if (success){
              
                }else{
                
                }
            }));
```

#### 是否初始化
```
 XDGCommon.IsInitialized(b => { 
  });
```

#### 埋点
```
  XDGCommon.Report(serverId, roleId, roleName);
  XDGCommon.TrackRole(string serverId, string roleId, string roleName, int level);
  XDGCommon.TrackUser(string userId);
  XDGCommon.TrackEvent(string eventName);
  XDGCommon.TrackAchievement();
```

#### 完成引导教程
```
 XDGCommon.EventCompletedTutorial();
```

#### 创建角色事件
```
  XDGCommon.EventCreateRole();
```

#### 获取版本号
```
XDGCommon.GetVersionName((version) =>{
               
            });
```

#### 通知开关
```
XDGCommon.SetCurrentUserPushServiceEnable(pushServiceEnable);
XDGCommon.IsCurrentUserPushServiceEnable(pushEnable => { 
});
```

#### 分享
```
XDGCommon.Share(ShareFlavors shareFlavors, string imagePath, XDGShareCallback callback);
XDGCommon.Share(ShareFlavors shareFlavors, string uri, string message, XDGShareCallback callback);
```

#### 获取当前获取到的地区信息
```
  XDGCommon.GetRegionInfo(wrapper =>
            {
               if (wrapper != null && wrapper.info != null) {
                    ...
               }
            });
```
