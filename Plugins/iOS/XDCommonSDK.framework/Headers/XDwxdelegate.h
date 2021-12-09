//
//  XDwxdelegate.h
//  XDGAccountSDK
//
//  Created by jessy on 2021/11/4.
//

#import <Foundation/Foundation.h>
#import "WXApi.h"

NS_ASSUME_NONNULL_BEGIN

typedef void(^wxSendAuthCallBack)(SendAuthResp *resp);
typedef void(^wxSendWebAuthCallBack)(BOOL success, NSString *code);
@interface XDwxdelegate : NSObject<WXApiDelegate>

+(XDwxdelegate *)initWithAppid:(NSString*)appid universalLink:(NSString *)universalLink;

+ (instancetype)sharedInstance;

- (BOOL)isSupportWX;

- (void)sendAuthRequest;

- (BOOL)HandleWeChatURL:(NSURL*)url;
- (BOOL)handleOpenUniversalLink:(NSUserActivity *)userActivity;

- (void)sendShareTextMessage;

- (void)WXShareWithTitle:(NSString*)title
                   bText:(BOOL)bText
                   scene:(int)scene
                 message:(WXMediaMessage*)message;

@property(nonatomic,copy)wxSendAuthCallBack wxSendCallBack;
@property(nonatomic,copy)wxSendWebAuthCallBack wxSendWebCallBack;

@end

NS_ASSUME_NONNULL_END
