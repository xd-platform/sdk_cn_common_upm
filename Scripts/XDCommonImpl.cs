using System;
using System.Collections.Generic;
using TapTap.Bootstrap;
using TapTap.Common;
using UnityEditor;
using UnityEngine;

namespace XD.Cn.Common{
    public class XDCommonImpl{
        private static readonly string COMMON_MODULE_UNITY_BRIDGE_NAME = "XDCoreService";
        private static volatile XDCommonImpl _instance;
        private static readonly object locker = new object();

        private XDCommonImpl(){
            EngineBridge.GetInstance()
                .Register(XDUnityBridge.COMMON_SERVICE_NAME, XDUnityBridge.COMMON_SERVICE_IMPL);
        }

        public static XDCommonImpl GetInstance(){
            if (_instance != null) return _instance;
            lock (locker){
                if (_instance == null){
                    _instance = new XDCommonImpl();
                }
            }

            return _instance;
        }

        public void GetSDKVersionName(Action<string> callback){
            var command = new Command.Builder()
                .Service(COMMON_MODULE_UNITY_BRIDGE_NAME)
                .Method("getSDKVersionName")
                .Callback(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command, result => {
                XDTool.Log("XDSDK getSDKVersionName result: " + result.ToJSON());

                if (!checkResultSuccess(result)){
                    callback($"GetVersionName Failed:{result.message}");
                    return;
                }

                callback(result.content);
            });
        }

        public void IsInitialized(Action<bool> callback){
            var command = new Command.Builder()
                .Service(COMMON_MODULE_UNITY_BRIDGE_NAME)
                .Method("isInitialized")
                .Callback(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command, result => {
                XDTool.Log("XDSDK IsInitialized result: " + result.ToJSON());
                if (!checkResultSuccess(result)){
                    callback(false);
                    return;
                }

                callback("true".Equals(result.content.ToLower()));
            });
        }

        public void Report(string serverId, string roleId, string roleName){
            var argsDic = new Dictionary<string, object>{
                {"serverId", serverId},
                {"roleId", roleId},
                {"roleName", roleName}
            };
            var command = new Command.Builder()
                .Service(COMMON_MODULE_UNITY_BRIDGE_NAME)
                .Method("report")
                .Args(argsDic)
                .OnceTime(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command);
            XDTool.Log($"XDSDK Report:  {serverId} -- {roleId} -- {roleName}");
        }

        public void StoreReview(){
            var command = new Command.Builder()
                .Service(COMMON_MODULE_UNITY_BRIDGE_NAME)
                .Method("storeReview")
                .OnceTime(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command);
            XDTool.Log("XDSDK StoreReview");
        }

        public void TrackUser(string userId){
            var command = new Command.Builder()
                .Service(COMMON_MODULE_UNITY_BRIDGE_NAME)
                .Method("trackUser")
                .Args("userId", userId)
                .OnceTime(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command);
            XDTool.Log($"XDSDK TrackUser:  {userId}");
        }

        public void TrackRole(string serverId, string roleId, string roleName, int level){
            var argsDic = new Dictionary<string, object>{
                {"serverId", serverId},
                {"roleId", roleId},
                {"roleName", roleName},
                {"level", level}
            };
            var command = new Command.Builder()
                .Service(COMMON_MODULE_UNITY_BRIDGE_NAME)
                .Method("trackRole")
                .Args(argsDic)
                .OnceTime(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command);
            XDTool.Log($"XDSDK TrackRole:  {serverId} -- {roleId} -- {roleName} -- {level}");
        }

        public void TrackEvent(string eventName){
            var command = new Command.Builder()
                .Service(COMMON_MODULE_UNITY_BRIDGE_NAME)
                .Method("trackEvent")
                .Args("eventName", eventName)
                .OnceTime(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command);
            XDTool.Log($"XDSDK TrackEvent:  {eventName}");
        }

        public void TrackAchievement(){
            var command = new Command.Builder()
                .Service(COMMON_MODULE_UNITY_BRIDGE_NAME)
                .Method("trackAchievement")
                .OnceTime(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command);
            XDTool.Log("XDSDK trackAchievement");
        }

        public void EventCompletedTutorial(){
            var command = new Command.Builder()
                .Service(COMMON_MODULE_UNITY_BRIDGE_NAME)
                .Method("eventCompletedTutorial")
                .OnceTime(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command);
            XDTool.Log("XDSDK eventCompletedTutorial");
        }

        public void EventCreateRole(){
            var command = new Command.Builder()
                .Service(COMMON_MODULE_UNITY_BRIDGE_NAME)
                .Method("eventCreateRole")
                .OnceTime(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command);
            XDTool.Log("XDSDK eventCreateRole");
        }

        public void InitSDK(string clientID, int orientation,Action<bool> callback){
            XDCallbackWrapper.initCallback = callback;
            var command = new Command.Builder()
                .Service(COMMON_MODULE_UNITY_BRIDGE_NAME)
                .Method("initSDK")
                .Args("clientId", clientID)
                .Args("orientation", orientation)
                .Callback(false)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void SetBridgeCallBack(Action<XDCallbackType, string> callback){
            var command = new Command.Builder()
                .Service(COMMON_MODULE_UNITY_BRIDGE_NAME)
                .Method("setCallback")
                .Callback(true)
                .CommandBuilder();

            EngineBridge.GetInstance().CallHandler(command,
                (result) => {
                    XDTool.Log("XDSDK setCallback result: " + result.ToJSON());
                    var mpWrapper = new XDCallbackWrapper(result.content);
                    mpWrapper.StartCallback(callback);
                });
        }

        private bool checkResultSuccess(Result result){
            return result.code == Result.RESULT_SUCCESS && !string.IsNullOrEmpty(result.content);
        }

        public void EnterGame(){
            var command = new Command.Builder()
                .Service(COMMON_MODULE_UNITY_BRIDGE_NAME)
                .Method("enterGame")
                .OnceTime(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void LeaveGame(){
            var command = new Command.Builder()
                .Service(COMMON_MODULE_UNITY_BRIDGE_NAME)
                .Method("leaveGame")
                .OnceTime(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void GetAntiAddictionAgeRange(Action<AgeRangeType> callback){
            var command = new Command.Builder()
                .Service(COMMON_MODULE_UNITY_BRIDGE_NAME)
                .Method("getAntiAddictionAgeRange")
                .Callback(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command, result => {
                XDTool.Log("XDSDK getAntiAddictionAgeRange result: " + result.ToJSON());
                if (checkResultSuccess(result)){
                    string str = result.content;
                    AgeRangeType type = AgeRangeType.OtherError;

                    if ("\"-1\"".Equals(str)){
                        type = AgeRangeType.NoRealName;
                    } else if ("\"0\"".Equals(str)){
                        type = AgeRangeType.Zero2Seven;
                    } else if ("\"8\"".Equals(str)){
                        type = AgeRangeType.Eight2Fifteen;
                    } else if ("\"16\"".Equals(str)){
                        type = AgeRangeType.Sixteen2Seventeen;
                    } else if ("\"8\"".Equals(str)){
                        type = AgeRangeType.EighteenUpper;
                    }

                    callback(type);
                } else{
                    callback(AgeRangeType.OtherError);
                }
            });
        }

        public void ShowLoading(){
            var command = new Command.Builder()
                .Service(COMMON_MODULE_UNITY_BRIDGE_NAME)
                .Method("showLoading")
                .OnceTime(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void HideLoading(){
            var command = new Command.Builder()
                .Service(COMMON_MODULE_UNITY_BRIDGE_NAME)
                .Method("hideLoading")
                .OnceTime(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void SetDebugMode(){
            var command = new Command.Builder()
                .Service(COMMON_MODULE_UNITY_BRIDGE_NAME)
                .Method("setDebugMode")
                .Args("setDebugMode", 1)
                .OnceTime(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command);
        }
        
        public void GetDid(Action<string> callback){
            var command = new Command.Builder()
                .Service(COMMON_MODULE_UNITY_BRIDGE_NAME)
                .Method("getDid")
                .Callback(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command, result => {
                XDTool.Log("===> getDid: " + result.ToJSON());
                if (!checkResultSuccess(result)){
                    callback($"getDid Failed:{result.message}");
                    return;
                }
                callback(result.content);
            });
        }
    }
}