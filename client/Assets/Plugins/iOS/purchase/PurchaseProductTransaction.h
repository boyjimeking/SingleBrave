//
//  PurchaseProductTransaction.h
//  Unity-iPhone
//
//  Created by sun on 14-3-28.
//
//

#import <Foundation/Foundation.h>
#import <StoreKit/StoreKit.h>

@interface PurchaseProductTransaction : NSObject<SKProductsRequestDelegate, SKPaymentTransactionObserver>
{
}

@property (nonatomic,strong) NSString *productId;//商品id
@property(nonatomic,strong) NSString *transactionReceiptData;//支付凭证
@property(nonatomic,strong) SKProduct *validProduct;//有效商品

-(void)purchaseProductStart;
-(int)requestProductPurchase:(const char *)productIdentifier;
-(void)setPurchaseTransactionReceipt:(NSString *)receipt;
-(const char *)getPurchaseTransactionReceipt;
- (void)completeTransaction:(SKPaymentTransaction *)transaction;
- (void)failedTransaction:(SKPaymentTransaction *)transaction;

@end
