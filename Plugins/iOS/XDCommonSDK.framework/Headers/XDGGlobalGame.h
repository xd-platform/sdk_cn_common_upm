
//  游戏数据

#import <Foundation/Foundation.h>
#import "XDGGameBindEntry.h"

NS_ASSUME_NONNULL_BEGIN

@interface XDGGlobalGame : NSObject
@property (nonatomic,copy) NSString *clientId;


@property (nonatomic,copy) NSString *region;                         // 发行地区
@property (nonatomic,copy) NSString *developerId;                    // 开发者ID
@property (nonatomic,copy) NSString *appId;
// wxID
@property (nonatomic,copy) NSString *wxAppId;
// qqWeb登录
@property (nonatomic,assign) BOOL qqWeb;

// QQID
@property (nonatomic,copy) NSString *qqAppId;
@property (nonatomic,copy) NSString *qqUniversalLink;
// wxWeb登录
@property (nonatomic,assign) BOOL wxWeb;

// 是否使用协议界面
@property(nonatomic,assign)BOOL isProtocolUiEnable;

// 游戏名称
@property (nonatomic,copy) NSString *gameName;
@property (nonatomic,strong) NSMutableArray *loginEntries;           // 登录入口
@property (nonatomic,strong) NSMutableArray *bindEntriesConfig;      // 绑定入口配置

// Tap 授权地址
@property (nonatomic,strong) NSMutableArray * tapLoginPermissions;

// 协议组
@property(nonatomic,strong)NSMutableArray *protocolLinkGroup;


@property (nonatomic,copy) NSString *reportUrl;                      // 客服地址

@property (nonatomic,assign) NSInteger languageLocale;               // 语言

//@property (nonatomic,copy) NSString *tapSdkUrl;                     // Tap服务器地址

/**
 协议
 */
@property (nonatomic,copy) NSString *serviceTermsUrl;
@property (nonatomic,copy) NSString *serviceAgreementUrl;
@property (nonatomic,copy) NSString *californiaPrivacyUrl;

// 韩国推送通知开关是否显示增加配置项
@property (nonatomic,assign) BOOL isKRPushServiceSwitchEnable;

- (void)updateBindEntriesConfig:(NSArray *)config;

- (void)compareVersionWithProtocolLinkGroup:(void(^)(BOOL isUpdate,NSArray *protocol, NSArray *updataProtocol,NSArray *showName,NSArray *showLink,BOOL haveOldSDK))com;

@end

NS_ASSUME_NONNULL_END
