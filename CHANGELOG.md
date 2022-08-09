# XDSDK Change Log

## v6.2.6
```
修复Android隐私合规问题
```

## v6.2.5
```
修复Android人中心刘海屏适配的问题
修复iOS头像问题
修复iOS notify token重置问题
```

## v6.2.4 (iOS6.2.0 - Android 6.2.4)
```
增加Android注销
```

## v6.2.3 (iOS6.2.0 - Android 6.2.1)
```
增加iOS注销
```

## v6.2.2 (iOS6.2.0 - Android 6.2.1)
```
6.2.2正式版。 6.2.2-rc版一样，什么都没改动
```

## v6.2.2-rc (iOS6.2.0 - Android 6.2.1)
```
安卓网页支付优化版：
* 网页支付样式优化
* 网页加载超时时间改为 5 秒

* (TapCommon、TapBootstrap、TapLogin、TapDB): **3.6.3**
* (AntiAddiction、AntiAddictionUI): **1.2.0**

```

## v6.2.1 (iOS6.2.0 - Android 6.2.0)
```
安卓网页支付 (Ping++ 微信/支付宝支付)
支付回调修改：SUCCESS 支付成功  改为  OK 支付完成 
6.2.1 即 6.3.0-rc 正式版 (之前预发版本错了)
```

## v6.3.0-rc (iOS6.2.0 - Android 6.2.0)
```
安卓网页支付 (Ping++ 微信/支付宝支付)
```

## v6.2.0 (iOS6.2.0) 
```
### iOS Feature
* 新增Apple IAP
* 新增防沉迷金额上报
### iOS BugFix
* 因防沉迷拦截时不显示 SDK 内部 Toast
### TapSDK Dependencies
* (TapCommon、TapBootstrap、TapLogin、TapDB)： **3.6.3**
```

## v6.1.3
```
修复安卓隐私协议弹框链接问题
```

## v6.1.2
```
修复安卓登录取消和登录失败回调问题
```

## v6.1.1
```
增加应用配置、用户信息的缓存机制，减少初始化、自动登录失败的问题
增加初始化、登录、内建账号流程的失败原因
获取 TDSUser 信息方式变更，需游戏主动请求 await TDSUser.GetCurrent().Result.Fetch()获取
接入防沉迷方式变更，需移除 AntiSDK.unitypackage 改用 NPM 方式引用 TDS 防沉迷 1.2.0 版本
移除单点登录流程中「登录成功」toast 提示
修复 Windows 环境下无法进行 Android 打包的问题
「Android」修复登录失败、登录取消后无回调的问题
「Android」修复登录成功后无用户绑定信息的问题
「Android」修复唤起弹窗时有概率闪退的问题
「Android」修复「删除账号」/「解绑账号」确认弹窗中用户使用回车键会导致输入框换行的问题
「iOS」修复签名时间有概率不一致，导致无法登录、支付的问题
「iOS」修复初始化失败时 App ID 为空，导致用户支付掉单的问题
「iOS」修复初始化失败，导致用户国家/地区信息为空的问题
SDK 升级。TapSDK 升级 Unity 3.6.3，LC SDK 升级至 Unity  0.10.5
```
## v6.1.0-beta
```
添加Android支付宝支付
```
## v6.0.0 
```
Unity:
1.初始化和登录接口修改了，回调在当前方法里，其他还是在SetCallback里。
2. 防沉迷的库独立出来了，游戏自己下载安装，可以自己更新防沉迷的库而不用通过xdsdk发版。
iOS:
用户协议、隐私政策
实名/防沉迷
登录(仅 TapTap 登录)
修改登录逻辑bug
Android:
用户协议、隐私政策
实名/防沉迷
登录(仅 TapTap 登录)
修复 Unity 无法收到在获取同步内建账号 token 接口失败时登录失败的回调
修复弹窗概率性闪退问题
```
[防沉迷的库下载](https://github.com/xd-platform/xd_sdk_resource/blob/master/Unity_CN/AntiSDK.unitypackage)
