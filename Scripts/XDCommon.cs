using System;

namespace XD.Cn.Common
{
    public class XDCommon
    {
        public static void GetSDKVersionName(Action<string> callback){
            XDCommonImpl.GetInstance().GetSDKVersionName(callback);
        }
        public static void IsInitialized(Action<bool> callback){
            XDCommonImpl.GetInstance().IsInitialized(callback);
        }
        public static void Report(string serverId, string roleId, string roleName){
            XDCommonImpl.GetInstance().Report(serverId, roleId, roleName);
        }
        public static void StoreReview(){
            XDCommonImpl.GetInstance().StoreReview();
        }
        public static void TrackUser(string userId){
            XDCommonImpl.GetInstance().TrackUser(userId);
        }
        public static void TrackRole(string serverId, string roleId, string roleName, int level){
            XDCommonImpl.GetInstance().TrackRole(serverId, roleId, roleName, level);
        }
        public static void TrackEvent(string eventName){
            XDCommonImpl.GetInstance().TrackEvent(eventName);
        }
        public static void TrackAchievement(){
            XDCommonImpl.GetInstance().TrackAchievement();
        }
        public static void EventCompletedTutorial(){
            XDCommonImpl.GetInstance().EventCompletedTutorial();
        }
        public static void EventCreateRole(){
            XDCommonImpl.GetInstance().EventCreateRole();
        }
        public static void InitSDK(string clientID, int orientation){
            XDCommonImpl.GetInstance().InitSDK(clientID, orientation);
        }
        public static void SetBridgeCallBack(Action<XDCallbackType, string, string> callback){
            XDCommonImpl.GetInstance().SetBridgeCallBack(callback);
        }
        public static void EnterGame(){
            XDCommonImpl.GetInstance().EnterGame();
        }
        public static void LeaveGame(){
            XDCommonImpl.GetInstance().LeaveGame();
        }
        public static void GetAntiAddictionAgeRange(Action<AgeRangeType> callback){
            XDCommonImpl.GetInstance().GetAntiAddictionAgeRange(callback);
        }
    }
}