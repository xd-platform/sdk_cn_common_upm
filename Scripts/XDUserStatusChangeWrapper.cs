using System;
using System.Collections.Generic;
using TapTap.Common;
using UnityEngine;

namespace XD.Cn.Common
{
    [Serializable]
    public class XDUserStatusChangeWrapper
    {
        public int code;

        public string message;

        public XDUserStatusChangeWrapper(string json)
        {
            Dictionary<string,object> dic = Json.Deserialize(json) as Dictionary<string,object>;
            this.code = SafeDictionary.GetValue<int>(dic,"code");
            this.message = SafeDictionary.GetValue<string>(dic,"message");
        }
    }
}