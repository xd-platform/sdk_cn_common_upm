//
//  BridgeCallbackHolder.h
//  XDCommonSDK
//
//  Created by guiming on 2021/12/2.
//

#import <Foundation/Foundation.h>
#import <XDCommonSDK/XDSDK.h>

NS_ASSUME_NONNULL_BEGIN

@interface BridgeCallbackHolder : NSObject<XDCallback>
+(instancetype)sharedInstance:(XDStringCallback)callback;
@end

NS_ASSUME_NONNULL_END
