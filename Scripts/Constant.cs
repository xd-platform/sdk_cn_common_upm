namespace XD.Cn.Common
{
    public class XDUnityBridge
    {
        //Common
        public static string COMMON_SERVICE_NAME = "com.xd.cn.common.bridge.XDCoreService";
        public static string COMMON_SERVICE_IMPL = "com.xd.cn.common.bridge.XDCoreServiceImpl";
        
        //Account
        public static string ACCOUNT_SERVICE_NAME = "com.xd.cn.account.unitybridge.XDLoginService";
        public static string ACCOUNT_SERVICE_IMPL = "com.xd.cn.account.unitybridge.XDLoginServiceImpl";
        
        //Payment
        public static string PAYMENT_SERVICE_NAME = "com.xd.cn.payment.unityBridge.XDPaymentService";
        public static string PAYMENT_SERVICE_IMPL = "com.xd.cn.payment.unityBridge.XDPaymentServiceImpl";
    }

    public class GlobalUnKnowError
    {
        public static int UN_KNOW = 0x9009;
    }
}