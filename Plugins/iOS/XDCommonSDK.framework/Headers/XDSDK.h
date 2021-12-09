
#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <XDCommonSDK/XDGShare.h>
#import <XDCommonSDK/XDGTrackerManager.h>
#import <XDCommonSDK/XDGMessageManager.h>
#import <XDCommonSDK/XDGRegionInfo.h>
#import <XDCommonSDK/XDCallback.h>


NS_ASSUME_NONNULL_BEGIN

typedef void (^XDStringCallback)(NSString*);

typedef NS_ENUM(NSInteger,XDConfigOrientation) {
    XDConfigOrientationLandscapeLeft     = 0,   // 横屏
    XDConfigOrientationPortrait          = 1,   // 竖屏
       
   
};

@interface XDConfig : NSObject

+ (instancetype)sharedInstance;

@property(nonatomic,copy) NSString *clientID;
@property(nonatomic,assign) XDConfigOrientation orientation;


@end


@interface XDSDK : NSObject

#pragma mark - TODO  去除方法  getRegionInfo

#pragma mark - TODO  新增方法

// 登录及协议结果回调
+ (void)setCallBack:(nonnull id<XDCallback>)delegate;

/// 获取当前 SDK 版本
+ (NSString *)getSDKVersionName;


#pragma mark - TODO  修改方法
/// 初始化 SDK
+ (void)initSDKConfig:(XDConfig *)config;



/// 当前登录用户，打开客服中心
/// @param serverId 服务器 ID，可为空
/// @param roleId 角色 ID，可为空
/// @param roleName 角色名，可为空
+ (void)reportWithServerId:(NSString *)serverId roleId:(NSString *)roleId roleName:(NSString *)roleName;

/// 调起或跳转商店评分
+ (void)storeReview;






#pragma mark -- traker
/// 跟踪用户
/// @param userId 用户唯一ID，非角色ID
+ (void)trackUser:(NSString *)userId;

+ (void)trackUser:(NSString *)userId
        loginType:(LoginEntryType)loginType
       properties:(NSDictionary *)properties;

/// 跟踪角色
/// @param roleId 角色ID
/// @param roleName 角色名
/// @param serverId 服务器ID
/// @param level 角色等级
+ (void)trackRoleWithRoleId:(NSString *)roleId
                   roleName:(NSString *)roleName
                   serverId:(NSString *)serverId
                      level:(NSInteger)level;

/// 跟踪自定义事件
/// @param eventName 事件名
+ (void)trackEvent:(NSString *)eventName;

/// 跟踪自定义事件
/// @param eventName 事件名
/// @param properties 属性
+ (void)trackEvent:(NSString *)eventName properties:(nullable NSDictionary *)properties;


/// 跟踪完成成就
+ (void)trackAchievement;

/// 跟踪完成新手引导接口
+ (void)eventCompletedTutorial;

/// 跟踪完成创角
+ (void)eventCreateRole;

/// 设置调试模式，debug 会输出SDK日志
/// @param debug 是否 debug 模式。默认 NO
+ (void)setDebugMode:(BOOL)debug;


#pragma mark - Applicaiton Delegate
+ (void)application:(UIApplication *)application
didFinishLaunchingWithOptions:(nullable NSDictionary<UIApplicationLaunchOptionsKey, id> *)launchOptions;

+ (void)application:(UIApplication *)application openURL:(NSURL *)url options:(NSDictionary<UIApplicationOpenURLOptionsKey,id> *)options;

+ (void)application:(UIApplication *)application openURL:(NSURL *)url sourceApplication:(NSString *)sourceApplication annotation:(id)annotation;

+ (void)application:(UIApplication *)application continueUserActivity:(NSUserActivity *)userActivity restorationHandler:(void (^)(NSArray<id<UIUserActivityRestoring>> * _Nullable))restorationHandler;

+ (void)application:(UIApplication *)application didReceiveRemoteNotification:(NSDictionary *)userInfo fetchCompletionHandler:(void (^)(UIBackgroundFetchResult))completionHandler;

// 针对屏幕旋转方向使用
+ (UIInterfaceOrientationMask)application:(UIApplication *)application supportedInterfaceOrientationsForWindow:(UIWindow *)window;

+ (void)scene:(UIScene *)scene openURLContexts:(NSSet<UIOpenURLContext *> *)URLContexts API_AVAILABLE(ios(13.0));

#pragma mark --新增，unity脚本注入的时候注意

+ (void)scene:(UIScene *)scene continueUserActivity:(NSUserActivity *)userActivity  API_AVAILABLE(ios(13.0));

@end

NS_ASSUME_NONNULL_END
