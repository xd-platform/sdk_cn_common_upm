using System.Collections.Generic;
using TapTap.Common;
using UnityEngine;

namespace XD.Cn.Common
{
    public class XDInitResultWrapper
    {
        public LocalConfigInfo localConfigInfo;
        public XDInitResultWrapper(string resultJson)
        {
            var dic = Json.Deserialize(resultJson) as Dictionary<string, object>;
            localConfigInfo = new LocalConfigInfo(dic);
        }
    }
}