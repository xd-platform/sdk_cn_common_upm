using System.Collections.Generic;
using TapTap.Common;
using UnityEngine;

namespace XD.Cn.Common
{
    public class XDInitResultWrapper
    {
        public LocalConfigInfo localConfigInfo;
        public XDInitResultWrapper(Dictionary<string, object> dic){
            localConfigInfo = new LocalConfigInfo(dic);
        }
    }
}