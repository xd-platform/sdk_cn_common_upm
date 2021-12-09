using System.Collections.Generic;
using TapTap.Common;
using UnityEngine;

namespace XD.Cn.Common{
    public class LocalConfigInfo{
        public string channel;
        public TapSdkConfig tapSdkConfig;

        public LocalConfigInfo(Dictionary<string, object> dic){
            if (dic == null){
                XDTool.Log("LocalConfigInfo 解析失败");
                return;
            }
            channel = SafeDictionary.GetValue<string>(dic, "channel");
            var configInfoDic = SafeDictionary.GetValue<Dictionary<string, object>>(dic, "tapSdkConfig");
            tapSdkConfig = new TapSdkConfig(configInfoDic);
        }
    }

    public class TapSdkConfig{
        public string clientId;
        public string clientToken;
        public bool enableTapDB;
        public string tapDBChannel;
        public string serverUrl;

        public TapSdkConfig(Dictionary<string, object> dic){
            if (dic == null) return;
            clientId = SafeDictionary.GetValue<string>(dic, "clientId");
            clientToken = SafeDictionary.GetValue<string>(dic, "clientToken");
            enableTapDB = SafeDictionary.GetValue<bool>(dic, "enableTapDB");
            tapDBChannel = SafeDictionary.GetValue<string>(dic, "tapDBChannel");
            serverUrl = SafeDictionary.GetValue<string>(dic, "serverUrl");
        }
    }
}