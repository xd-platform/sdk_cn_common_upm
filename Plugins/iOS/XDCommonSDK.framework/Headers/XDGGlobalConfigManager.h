//  读取配置文件

#import <Foundation/Foundation.h>
#import <XDCommonSDK/XDGTapTapInfo.h>




NS_ASSUME_NONNULL_BEGIN

@interface XDGGlobalConfigManager : NSObject

//@property (nonatomic, assign, getter=isAdvertiserIDCollectionEnabled) BOOL advertiserIDCollectionEnabled;
@property (nonatomic, assign, getter=isDebugMode) BOOL debugMode;


@property (nonatomic,copy) NSString *clientId;
//@property (nonatomic,copy) NSString *appId;
@property (nonatomic,copy) NSString *gameName;
@property (nonatomic,copy) NSString *gameVersion;
//@property (nonatomic,copy) NSString *domain;
@property (nonatomic,copy) NSString *channel;
@property (nonatomic,copy) NSArray *logos;



@property (nonatomic,strong) XDGTapTapInfo    *taptapInfo;

@property (nonatomic,strong) NSDictionary *configDict;
@property (nonatomic,strong) NSDictionary *tapDict;

- (void)setUpConfig;


+ (XDGGlobalConfigManager *)defaultInstance;


+ (BOOL)taptapEnable;


@end

NS_ASSUME_NONNULL_END
