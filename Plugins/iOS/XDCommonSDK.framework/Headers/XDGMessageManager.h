
#import <Foundation/Foundation.h>


NS_ASSUME_NONNULL_BEGIN

@interface XDGMessageManager : NSObject
+ (XDGMessageManager *)shareInstance;

/// Open or close firebase push service for current user. Call after user logged in.
/// @param enable enable
+ (void)setCurrentUserPushServiceEnable:(BOOL)enable;

/// The user need push service or not. Call after user logged in.
+ (BOOL)isCurrentUserPushServiceEnable;
@end

NS_ASSUME_NONNULL_END
