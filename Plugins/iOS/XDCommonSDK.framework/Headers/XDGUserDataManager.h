
#import <Foundation/Foundation.h>
#import <XDCommonSDK/XDUser.h>
#import <XDCommonSDK/XDGEntryType.h>

NS_ASSUME_NONNULL_BEGIN
@interface XDGUserDataManager : NSObject

+ (void)setCurrentUser:(XDUser *)user;
+ (XDUser *)getCurrentUserData;

+ (void)updateLoginState:(BOOL)loggedIn;
+ (BOOL)isUserLoggedIn;
+ (BOOL)isUserTokenValid;

+ (LoginEntryType)getCurLoginType;

+ (void)userLoginSuccess;
+ (void)userLogout;

/// 是否需要弹隐私协议
+ (BOOL)needShowAgreement;
+ (void)updateUserServiceAgreement;

/// 推送开关
+ (BOOL)needPushService;
+ (void)updatePushServiceStatu:(BOOL)statu;

@end

NS_ASSUME_NONNULL_END
