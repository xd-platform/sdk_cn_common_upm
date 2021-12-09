//
//  XDCallback.h
//  XDCommonSDK
//
//  Created by jessy on 2021/11/26.
//

#import <Foundation/Foundation.h>
#import <XDCommonSDK/XDUser.h>


typedef enum : NSUInteger {
    LoginSucceed         = 0,
    LoginFailed          = 1,
    loginCancel          = 2,
    LogoutSucceed        = 3,
    SwitchAccount        = 4,
    AgreeProtocol        = 5,
    InitSuccess          = 6,
    InitFail             = 7,
    InterruptByRealName  = 8,
    UserStateChangeCodeBindSuccess    = 9,
    UserStateChangeCodeUnBindSuccess  = 10,
    
} XDCallbackType;

typedef NS_ENUM(NSInteger,XDUserStateChangeCode) {
    XDUserStateChangeCodeBindSuccess     = 0x1001,   // user bind success,msg = entry type in string,eg: @"TAPTAP"
    XDUserStateChangeCodeUnBindSuccess   = 0x1002,   // user unbind success,msg = entry type in string
};


@protocol XDCallback <NSObject>

// 登录成功
- (void)onLoginSucceed:(nullable XDUser *)result;
// 登录失败
- (void)onLoginFailed:(nullable NSString *)msg;

// 登录取消
- (void)onLoginCancel;
//登出成功
- (void)onLogoutSucceed;
//切换账号
- (void)onSwitchAccount;
// 阿里云实名成功，中宣部实名失败
- (void)onInterruptByRealName;
// 初始化成功
- (void)onInitSuccess;
// 初始化失败
- (void)onInitFailed:(nullable NSString *)msg;
// 同意协议
- (void)onAgreeProtocol;
// 用户解绑 绑定
- (void)onUserStatusChanged:(XDUserStateChangeCode)code msg:(nullable NSString *)msg;
@end

