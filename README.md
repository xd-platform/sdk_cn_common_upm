# XDSDK接入文档

## 1.添加引用
```
//1.upm添加:
{
"com.leancloud.realtime": "https://github.com/leancloud/csharp-sdk-upm.git#realtime-0.10.5",
"com.leancloud.storage": "https://github.com/leancloud/csharp-sdk-upm.git#storage-0.10.5",
"com.taptap.tds.bootstrap": "https://github.com/TapTap/TapBootstrap-Unity.git#3.6.3",
"com.taptap.tds.common": "https://github.com/TapTap/TapCommon-Unity.git#3.6.3",
"com.taptap.tds.login": "https://github.com/TapTap/TapLogin-Unity.git#3.6.3",
"com.taptap.tds.tapdb": "https://github.com/TapTap/TapDB-Unity.git#3.6.3",
"com.xd.cn.common": "https://github.com/xd-platform/sdk_cn_common_upm.git#6.1.1",
"com.xd.cn.account": "https://github.com/xd-platform/sdk_cn_account_upm.git#6.1.1",
"com.xd.cn.payment": "https://github.com/xd-platform/sdk_cn_payment_upm.git#6.1.1",
"com.tapsdk.antiaddiction": "1.2.0",
},
//2.也可以使用npm添加，npm需要加如下scopes:
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
```

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

