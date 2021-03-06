
#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

NS_ASSUME_NONNULL_BEGIN

typedef NS_ENUM(NSInteger,XDGShareType) {
    XDGShareTypeFacebook = 0,
    XDGShareTypeLine,
    XDGShareTypeTwitter,
};

typedef NS_ENUM(NSInteger,XDGShareErrorCode) {
    XDGShareErrorCodeNone         = 0,
    XDGShareErrorCodeFail         = 0x5001,
    XDGShareErrorCodeNotSupport   = 0x5002
};

typedef void(^XDGShareCallback)(NSError * _Nullable error, BOOL cancel);

@interface XDGShare : NSObject
/// 分享图片
/// @param type 分享平台类型
/// @param image 待分享图片
/// @param completeHandler 分享结果回调
+ (void)shareWithType:(XDGShareType)type image:(UIImage *)image completeHandler:(XDGShareCallback)completeHandler;

/// 分享图片
/// @param type 分享平台类型
/// @param imagePath 待分享图片沙盒路径
/// @param completeHandler 分享结果回调
+ (void)shareWithType:(XDGShareType)type imagePath:(NSString *)imagePath completeHandler:(XDGShareCallback)completeHandler;

/// 分享URL
/// @param type 分享平台类型
/// @param url 待分享URL
/// @param completeHandler 分享结果回调
+ (void)shareWithType:(XDGShareType)type url:(NSString *)url completeHandler:(XDGShareCallback)completeHandler;

/// 分享URL
/// @param type 分享平台类型
/// @param url 待分享URL
/// @param message 文字说明
/// @param completeHandler 分享结果回调
+ (void)shareWithType:(XDGShareType)type url:(NSString *)url message:(nullable NSString *)message completeHandler:(XDGShareCallback)completeHandler;


@end

NS_ASSUME_NONNULL_END
