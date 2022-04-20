using System;
using TapTap.Common;
using UnityEngine;

namespace XD.Cn.Common{
    public class XDTool{
        
        private static string userId = "";  //错误日志打印用
        
        public static void Log(string msg){
            Debug.Log("\n------------------ XDSDK打印信息 ------------------\n"+msg + "\n\n");
        }
        
        public static void LogError(string msg){
            try{
                XDCommon.GetDid(did => {
                    Print("userId:【" + userId + "】," + "device did:【" + did + "】。" + msg);
                });
            } catch (Exception e){
                Print("userId:【" + userId + "】"  + msg + e.Message);
            }
        }
        
        private static void Print(string msg){
            Debug.LogError("\n------------------ XDSDK报错 ------------------\n" + msg + "\n\n");
        }

        public static void SetUserId(String userId){
            XDTool.userId = userId;
            Log("设置Log userId : " + userId);
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