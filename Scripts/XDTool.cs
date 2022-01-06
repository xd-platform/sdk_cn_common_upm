using System;
using TapTap.Common;
using UnityEngine;
using Newtonsoft.Json;

namespace XD.Cn.Common{
    public class XDTool{
        
        public static T GetModel<T>(string json) where T : BaseModel{
            if (string.IsNullOrEmpty(json)){
                return null;
            }
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string GetJson<T>(T obj){
            if (obj == null){
                return null;
            }
            return JsonConvert.SerializeObject(obj);
        }
        
        public static void Log(string msg){
            Debug.Log("\n------------------ XDSDK打印信息 ------------------\n"+msg + "\n\n");
        }
        
        public static void LogError(string msg){
            Debug.LogError("\n------------------ XDSDK报错 ------------------\n"+msg + "\n\n");
        }
        
        public  static  bool checkResultSuccess(Result result){
            return result.code == Result.RESULT_SUCCESS && !string.IsNullOrEmpty(result.content);
        }
    }
}