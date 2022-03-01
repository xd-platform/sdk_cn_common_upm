using System;
using System.Collections.Generic;
using System.Linq;
using TapTap.Common;

namespace XD.Cn.Common{
    [Serializable]
    public class XDUser{
        public string userId{ get; set; }
        public string avatar{ get; set; }
        public string loginType{ get; set; }
        public string name{ get; set; }
        public string nickName{ get; set; }
        
        public List<string> boundAccounts;
        public XDAccessToken token{ get; set; }
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
            user = new XDUser();
            user.userId = SafeDictionary.GetValue<string>(dic, "userId");
            user.avatar = SafeDictionary.GetValue<string>(dic, "avatar");
            user.loginType = SafeDictionary.GetValue<string>(dic, "loginType");
            user.name = SafeDictionary.GetValue<string>(dic, "name");
            user.nickName = SafeDictionary.GetValue<string>(dic, "nickName");
            user.boundAccounts = SafeDictionary.GetValue<List<object>>(dic, "boundAccounts").Cast<string>().ToList();
            
            var tkDic = SafeDictionary.GetValue<Dictionary<string, object>>(dic, "token");
            var tkMd = new XDAccessToken();
            tkMd.kid = SafeDictionary.GetValue<string>(tkDic, "kid");
            tkMd.tokenType = SafeDictionary.GetValue<string>(tkDic, "tokenType");
            tkMd.macAlgorithm = SafeDictionary.GetValue<string>(tkDic, "macAlgorithm");
            tkMd.macKey = SafeDictionary.GetValue<string>(tkDic, "macKey");
            user.token = tkMd;
            
            if (string.IsNullOrEmpty(user.userId)){
                error = new XDError(-1, "user is null");
                user = null;
                XDTool.Log("用户解析是空");
            } 
        }
    }
    
    public enum LoginType{
        Default, //自动以上次信息登录
        TapTap,
        Guest,  //游客
    }
}