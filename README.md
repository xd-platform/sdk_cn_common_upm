# XDSDK接入文档

## 1.添加引用
```
//1.添加引用:
{
  "dependencies": {
"com.leancloud.realtime": "https://github.com/leancloud/csharp-sdk-upm.git#realtime-0.10.5",
"com.leancloud.storage": "https://github.com/leancloud/csharp-sdk-upm.git#storage-0.10.5",
"com.taptap.tds.bootstrap": "https://github.com/TapTap/TapBootstrap-Unity.git#3.6.3",
"com.taptap.tds.common": "https://github.com/TapTap/TapCommon-Unity.git#3.6.3",
"com.taptap.tds.login": "https://github.com/TapTap/TapLogin-Unity.git#3.6.3",
"com.taptap.tds.tapdb": "https://github.com/TapTap/TapDB-Unity.git#3.6.3",
"com.xd.cn.common": "https://github.com/xd-platform/sdk_cn_common_upm.git#6.2.0",
"com.xd.cn.account": "https://github.com/xd-platform/sdk_cn_account_upm.git#6.2.0",
"com.xd.cn.payment": "https://github.com/xd-platform/sdk_cn_payment_upm.git#6.2.0",
"com.tapsdk.antiaddiction": "1.2.0",
},
"scopedRegistries": [
    {
      "name": "XD CN SDK",
      "url": "http://npm.xindong.com",
      "scopes": [
        "com.xd.cn"
      ]
    },
    {
      "name": "TapTap",
      "url": "https://nexus.tapsvc.com/repository/npm-registry/",
      "scopes": [
        "com.tapsdk"
      ]
    }
  ]
 }
```
1. 防沉迷库需要通过npm引用1.2.0版本的，且删除本地之前添加的AntiSDK文件夹(如果之前有加的话)。
2. v6.1.1版本开始，内建账号采用本地构建，减少登录过程中的网络请求。在登录成功后，游戏要获取TesUser信息前需要手动调用一下fetch方法【await TDSUser.GetCurrent().Result.Fetch()】
3. 上面所有的库都可以通过npm方式添加，npm方式需要添加上面的两个scopes。
4. 推荐登录流程，先LoginByType(Default)自动登录(自动登录: 以上次登录成功的账户继续登录)，如果自动登录是吧，再显示Tap 或 游客登录按钮给用户登录。

### 1.3 添加配置文件
安卓不需要配置文件，iOS需要两个plist配置文件， [这里下载](https://github.com/xd-platform/xd_sdk_resource/tree/master/Unity_CN/Config) 添加到`Assets/Plugins/` 文件下。
![XD-Plist](https://github.com/xd-platform/xd_sdk_resource/blob/master/Images/XD-Plist.png)
![TDS-Plist](https://github.com/xd-platform/xd_sdk_resource/blob/master/Images/TDS-Plist.png)

## 2.接口使用
#### 绑定回调
使用sdk前需先绑定回调
```
 XDCommon.SetBridgeCallBack((type, msg) => {
                logText = "回调： " + type;
            });
            
type: XDCallbackType 有如下类型
LogoutSucceed 退出登录
SwitchAccount 切换账号
AgreeProtocol 同意协议
InterruptByRealName 需要实名认证
UserStateChangeCodeBindSuccess 绑定成功
UserStateChangeCodeUnBindSuccess 解绑成功
```

#### 初始化
```
//orientation: 0横屏，1竖屏。需要初始化成功后才能登录
 XDCommon.InitSDK(XdClientId, orientation, (success) => {
                if(success){
                }else{
                }
            });
```

#### 是否初始化SDK, 使用前必须初始化
```
XDCommon.IsInitialized(b => { logText = b ? "已经初始化" : "未初始化"; });
```

#### 获取版本号
```
XDCommon.GetSDKVersionName((result) => { logText = "版本号：" + result; });
```

#### 自带登录弹框的登录
```
 XDAccount.Login(user => {
                logText = "登录成功 userId：" + user.userId + "  kid: " + user.token.kid;
            },(error => {
                logText = "登录失败：" + error.error_msg;
            }));
```

#### 单点登录
```
  XDAccount.LoginByType(LoginType.Default, user => {
                logText = "成功：" + user.name;
            },(error => {
                logText = "失败：" + error.error_msg;
            }));
            
enum LoginType{
        Default, //以上次登录信息自动登录
        TapTap,  //TapTap登录
        Guest,  //游客登录
    }           
```

#### 退出登录
```
XDAccount.Logout();
```

#### 获取用户
```
   XDAccount.GetUser(user => { logText = "点击获取用户： " + user.name; }, error => { logText = "点击获取用户失败"; });
        
```

#### 打开用户中心
```
XDAccount.OpenUserCenter();
```

#### 客服反馈
```
XDCommon.Report("serverId", "roleId", "roleName");
```

#### 防沉迷时间上报
```
登录成功、进入前台时调用:
XDCommon.EnterGame();

退出登录、进入后台时调用:
XDCommon.LeaveGame();
```
![防沉迷](https://github.com/xd-platform/xd_sdk_resource/blob/master/Images/anti_time.png)

#### 获取年龄段
```
 XDCommon.GetAntiAddictionAgeRange((type) => {
            });
            
// type 是如下枚举, 获取防沉迷玩家年龄段，-1：未实名，0：0-7岁，8：8到15岁，16：16到17对，18：成年玩家
enum AgeRangeType : int{
        NoRealName = -1,
        Zero2Seven = 0,
        Eight2Fifteen = 8,
        Sixteen2Seventeen = 16,
        EighteenUpper = 18,
        OtherError = -10 //未匹配到
    }
```

#### 支付宝支付
```
XDPayment.AndroidPay(orderIdStr, productId, "roleID", "serverId", "ext",
(type, message) =>
{
XDTool.Log("支付结果:" + type + message);
});
AndroidPayResultType{
        Success = 0,
        Cancel = 1,
        Processing = 2,
        Error = -1,
    }
```
## iOS支付

#### iOS 购买商品
```
XDPayment.PayWithProduct(orderIdStr, productId, "roleID", "serverId", "ext",
wrapper =>
{
XDTool.Log("支付结果" + JsonUtility.ToJson(wrapper));
if (wrapper.xdgError != null)
{
logText = "支付商品错误 :" + wrapper.xdgError.ToJSON();
}
else
{
logText = "支付商品结果订单数据: " + JsonUtility.ToJson(wrapper);
}
});
```

#### iOS 商品查询
```
 XDPayment.QueryWithProductIds(productIds, info =>
            {
                XDTool.Log("查询商品结果" + JsonUtility.ToJson(info));
                if (info.xdgError != null)
                {
                    logText = "查询商品错误：" + info.xdgError.ToJSON();
                }
                else
                {
                    logText = "查询商品结果：";
                    foreach (var t in info.skuDetailList)
                    {
                        logText += JsonUtility.ToJson(t);
                    }
                }
            });
```

#### iOS 查看未完成订单
```
XDPayment.QueryRestoredPurchases(list =>
{
XDTool.Log("未完成订单" + JsonUtility.ToJson(list));
logText = "未完成订单: ";
foreach (var t in list)
{
logText += JsonUtility.ToJson(t);
}
});
```

#### iOS 获取补单信息
```
XDPayment.CheckRefundStatus((wrapper) =>
{
XDTool.Log("获取补单列表数据" + JsonUtility.ToJson(wrapper));
if (wrapper.xdgError != null)
{
logText = wrapper.xdgError.error_msg;
}
else
{
var list = wrapper.refundList;
if (list != null && list.Count > 0)
{
var tempText = "";
for (var i = 0; i < list.Count; i++)
{
tempText += JsonUtility.ToJson(list[i]);
}

                        logText = "需要补单：" + tempText;
                    }
                    else
                    {
                        logText = "没有需要补单的单子";
                    }
                }
            });
```

#### iOS 获取补单信息（带UI）
```
 XDPayment.CheckRefundStatusWithUI((wrapper) =>
            {
                XDTool.Log("获取补单列表数据" + JsonUtility.ToJson(wrapper));
                if (wrapper.xdgError != null)
                {
                    logText = wrapper.xdgError.error_msg;
                }else{
                    var list = wrapper.refundList;
                    if (list != null && list.Count > 0)
                    {
                        logText = "需要补单：" + JsonUtility.ToJson(list);
                    }
                    else
                    {
                        logText = "没有需要补单的单子";
                    }
                }
            });
```


## 打点
```
  XDCommon.TrackUser(string userId);  //即TapDB.setUser(id)  ，跟踪用户
  XDCommon.TrackEvent(string eventName); //事件埋点
  XDCommon.TrackAchievement(); //完成成就
  XDCommon.TrackRole(string serverId, string roleId, string roleName, int level); //跟踪角色
```
# [Change Log](https://github.com/xd-platform/sdk_cn_common_upm/blob/inner_upm/CHANGELOG.md)
