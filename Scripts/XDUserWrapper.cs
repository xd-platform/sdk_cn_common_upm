using System;
using System.Collections.Generic;
using TapTap.Common;

namespace XD.Cn.Common{
    [Serializable]
    public class XDUser : BaseModel{
        public string userId{ get; set; }
        public string avatar{ get; set; }
        public string loginType{ get; set; }
        public List<string> boundAccounts{ get; set; }
        public XDAccessToken token{ get; set; }
        public string name{ get; set; }
        public string nickName{ get; set; }
    }

    [Serializable]
    public class XDAccessToken{
        public string kid{ get; set; }
        public string tokenType{ get; set; }
        public string macAlgorithm{ get; set; }
        public string macKey{ get; set; }
    }

    [Serializable]
    public class XDUserWrapper{
        public XDUser user;
        public XDError error;

        public XDUserWrapper(Dictionary<string, object> userDic){
            var dic = SafeDictionary.GetValue<Dictionary<string, object>>(userDic, "user");
            var userJson = XDTool.GetJson(dic);
            user = XDTool.GetModel<XDUser>(userJson);
            if (user == null || string.IsNullOrEmpty(user.userId)){
                error = new XDError(-1, "user is null");
                user = null;
                XDTool.Log("用户解析是空");
            } 
        }
    }
    
    public enum LoginType{
        Default,
        TapTap,
    }
}