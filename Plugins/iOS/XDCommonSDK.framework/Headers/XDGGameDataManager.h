
#import <Foundation/Foundation.h>
#import <XDCommonSDK/XDGRegionInfo.h>
@class XDGGlobalGame;

NS_ASSUME_NONNULL_BEGIN
@interface XDGGameDataManager : NSObject
+ (XDGGameDataManager *)shareInstance;

+ (XDGGlobalGame *)currentGameData;
+ (NSArray *)currentLoginEntries;
+ (NSArray *)currentBindEntries;
+ (NSString *)serviceTermsUrl;
+ (NSString *)serviceAgreementUrl;
+ (NSString *)californiaPrivacyUrl;
+ (NSArray *)gameLogos;
+ (NSString *)tapServerUrl;
+ (NSDictionary *)configData;
+ (NSDictionary *)configTapDict;
+ (NSString *)clientID;

+ (BOOL)commonWXID;

+ (void)setLanguageLocale:(NSInteger)locale;

+ (void)getClientConfigWithCommonParams:(NSMutableDictionary *)params com:(void (^)(BOOL success, NSString *msg))handler;


/// 是否已经初始化
+ (BOOL)isGameInited;
/// 是否需要客服
+ (BOOL)needReportService;
#pragma mark - configs


+ (BOOL)taptapEnable;

+ (BOOL)tapDBEnable;
@end

NS_ASSUME_NONNULL_END
