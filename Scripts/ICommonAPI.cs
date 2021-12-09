using System;

namespace XD.Cn.Common{
    public interface ICommonAPI{
        void GetSDKVersionName(Action<string> callback);
        void IsInitialized(Action<bool> callback);
        void Report(string serverId, string roleId, string roleName);
        void StoreReview();
        void TrackUser(string userId);
        void TrackRole(string serverId, string roleId, string roleName, int level);
        void TrackEvent(string eventName);
        void TrackAchievement();
        void EventCompletedTutorial();
        void EventCreateRole();
        void InitSDK(string clientID, int orientation);
        void SetBridgeCallBack(Action<XDCallbackType, string, string> callback);
    }
}