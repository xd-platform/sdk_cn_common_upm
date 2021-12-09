//
//  TDSGlobalTapTapInfo.h
//  XDCommonSDK
//
//  Created by JiangJiahao on 2020/11/4.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface XDGTapTapInfo : NSObject

@property (nonatomic,copy) NSString *clientId;
@property (nonatomic,copy) NSString *clientSecret;
@property (nonatomic,copy) NSString *serverUrl;

@property (nonatomic) BOOL tapdbEnabled;

//@property (nonatomic) BOOL momentEnabled;

+ (instancetype)instanceWithInfoDic:(NSDictionary *)infoDic;
@end

NS_ASSUME_NONNULL_END
