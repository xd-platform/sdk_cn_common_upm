using System;
using System.Collections.Generic;
using TapTap.Common;
using TapTap.Bootstrap;

namespace XD.Cn.Common{
    [Serializable]
    public class XDCallbackWrapper{
        public XDCallbackType type;
        public string result;
        public string errorMsg;

        public XDCallbackWrapper(string json){
            Dictionary<string, object> dic = Json.Deserialize(json) as Dictionary<string, object>;
            this.type = (XDCallbackType) SafeDictionary.GetValue<int>(dic, "type");
            this.result = SafeDictionary.GetValue<string>(dic, "result");
            this.errorMsg = SafeDictionary.GetValue<string>(dic, "errorMsg");

            if (type == XDCallbackType.InitSuccess){
                var wrapper = new XDInitResultWrapper(result);
                if (wrapper.localConfigInfo.tapSdkConfig != null){
                    var info = wrapper.localConfigInfo.tapSdkConfig;
                    var gameChannel = wrapper.localConfigInfo.channel ?? "";
                    if (Platform.IsAndroid()){
                        if (!string.IsNullOrEmpty(info.tapDBChannel)){
                            gameChannel = info.tapDBChannel;
                        }
                    }
                
                    var config = new TapConfig.Builder()
                        .ClientID(info.clientId) // 必须，开发者中心对应 Client ID
                        .ClientToken(info.clientToken) // 必须，开发者中心对应 Client Token
                        .ServerURL(info.serverUrl) // 开发者中心 > 你的游戏 > 游戏服务 > 云服务 > 数据存储 > 服务设置 > 自定义域名 绑定域名
                        .RegionType(RegionType.CN) // 非必须，默认 CN 表示国内
                        .TapDBConfig(info.enableTapDB, gameChannel, "")
                        .ConfigBuilder();
                    TapBootstrap.Init(config);
                    XDTool.Log("TapBootstrap 初始化成功 clientId:= " + info.clientId + " toke:= " + info.clientToken);
                }
            }
        }
    }
    
    public enum XDCallbackType : int{ //int值要与iOS 安卓对应！
        LoginSucceed = 0,
        LoginFailed = 1,
        loginCancel = 2,
        LogoutSucceed = 3,
        SwitchAccount = 4,
        AgreeProtocol = 5,
        InitSuccess = 6,
        InitFail = 7,
        InterruptByRealName  = 8,
        UserStateChangeCodeBindSuccess    = 9,
        UserStateChangeCodeUnBindSuccess  = 10
    }
    
    // 获取防沉迷玩家年龄段，-1：未实名，0：0-7岁，8：8到15岁，16：16到17对，18：成年玩家
    public enum AgeRangeType : int{ 
        NoRealName = -1,
        Zero2Seven = 0,
        Eight2Fifteen = 8,
        Sixteen2Seventeen = 16,
        EighteenUpper = 18,
        OtherError = -10  //未匹配到
    }
}