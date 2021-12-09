using System;
using System.Collections.Generic;
using TapTap.Common;
using UnityEngine;
using System.Collections;

namespace XD.Cn.Common
{
// The user's bound accounts. eg.@[@"TAPTAP",@"GOOGLE",@"FACEBOOK"]
    [Serializable]
    public class XDUser{
        // The user's user ID.
        public string userId;
        
        // The user's user name.
        public string name;

        // The user's current loginType.
        public string loginType; //App传来的是字符串，如 TapTap。 通过 GetLoginType() 方法获取枚举

        public List<string> boundAccounts;    

        // The user's token.
        public XDAccessToken token;

        public XDUser(string json){
                Dictionary<string,object> dic = Json.Deserialize(json) as Dictionary<string,object>;
                this.userId = SafeDictionary.GetValue<string>(dic, "userId");
                this.name = SafeDictionary.GetValue<string>(dic, "name");
                this.loginType = SafeDictionary.GetValue<string>(dic, "loginType");
                this.boundAccounts = SafeDictionary.GetValue<List<string>>(dic, "boundAccounts");
                this.token = new XDAccessToken(SafeDictionary.GetValue<Dictionary<string, object>>(dic, "token"));
        }
        
        public XDUser(Dictionary<string,object> dic){   
            this.userId = SafeDictionary.GetValue<string>(dic,"userId");
            this.name = SafeDictionary.GetValue<string>(dic,"name");
            this.loginType = SafeDictionary.GetValue<string>(dic, "loginType");
            //参考老项目，这里没有 boundAccounts
            this.token  = new XDAccessToken(SafeDictionary.GetValue<Dictionary<string,object>>(dic,"token"));
        }
    }
    
    [Serializable]
        public class XDUserWrapper{
            public XDUser user;
            public XDError error;

            public XDUserWrapper(string json){
                Dictionary<string, object> contentDic = Json.Deserialize(json) as Dictionary<string, object>;
                Dictionary<string, object>
                    userDic = SafeDictionary.GetValue<Dictionary<string, object>>(contentDic, "user");
                Dictionary<string, object> errorDic =
                    SafeDictionary.GetValue<Dictionary<string, object>>(contentDic, "error");

                if (userDic != null){
                    this.user = new XDUser(userDic);
                }

                if (errorDic != null){
                    this.error = new XDError(errorDic);
                }
            }
        }

        [Serializable]
        public class XDAccessToken{
            // 唯一标志
            public string kid;

            // 认证码类型
            public string tokenType;

            // mac密钥
            public string macKey;

            // mac密钥计算方式
            public string macAlgorithm;

            public XDAccessToken(Dictionary<string, object> dic){
                if (dic == null) return;
                this.kid = SafeDictionary.GetValue<string>(dic, "kid");
                this.tokenType = SafeDictionary.GetValue<string>(dic, "tokenType");
                this.macKey = SafeDictionary.GetValue<string>(dic, "macKey");
                this.macAlgorithm = SafeDictionary.GetValue<string>(dic, "macAlgorithm");
            }
        }

    
    
}