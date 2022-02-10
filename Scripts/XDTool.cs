using System;
using TapTap.Common;
using UnityEngine;

namespace XD.Cn.Common{
    public class XDTool{
        
        public static void Log(string msg){
            Debug.Log("\n------------------ XDSDK打印信息 ------------------\n"+msg + "\n\n");
        }
        
        public static void LogError(string msg){
            Debug.LogError("\n------------------ XDSDK报错 ------------------\n"+msg + "\n\n");
        }
        
        public static bool IsEmpty(string str){
            if (str == null){
                return true;
            }
            if (String.IsNullOrEmpty(str)){
                return true;
            }
            if (String.IsNullOrWhiteSpace(str)){
                return true;
            }
            return false;
        }
        
        public  static  bool checkResultSuccess(Result result){
            return result.code == Result.RESULT_SUCCESS && !string.IsNullOrEmpty(result.content);
        }
    }
}