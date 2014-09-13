//
//  PurchaseProductTransaction.m
//  Unity-iPhone
//
//  Created by sun on 14-3-28.
//
//

#import "PurchaseProductTransaction.h"
#import "NSData+Base64.h"

#define UNOPEND_IN_APP_PURCHASE 101;//该设备未开启应用内支付
#define PRODUCT_DISABLED 102;//商品无效
#define SUCCESS 100;//操作成功

@implementation PurchaseProductTransaction

@synthesize productId;
@synthesize transactionReceiptData;//支付凭证
@synthesize validProduct;


//2  请求支付
-(int)requestProductPurchase:(const char *)productIdentifier
{
    // Create an instance of EBPurchase.

    //[self restorePurchase];

    productId = [NSString stringWithUTF8String:productIdentifier];
    
    //检测当前设备是否开启应用内购买
    if([SKPaymentQueue canMakePayments])
    {
        //实例化一个请求
        SKProductsRequest *productRequest = [[SKProductsRequest alloc] initWithProductIdentifiers:[NSSet setWithObject:productId]];
        productRequest.delegate = self;
        [productRequest start];//开启请求
        [productRequest release];
        
        return SUCCESS;
    }else{
        NSLog(@"当前账号并未开启应用内支付");
        UnitySendMessage( "IOS", "onPayFailCallback","");
        return UNOPEND_IN_APP_PURCHASE;
    }
    
}

//获取苹果返回的支付凭证
-(void)setPurchaseTransactionReceipt:(NSString *)receipt
{
    self.transactionReceiptData = [receipt stringByReplacingOccurrencesOfString:@"+" withString:@"%2B"];
}

//获取苹果返回的支付凭证
-(const char *)getPurchaseTransactionReceipt
{
    return [self.transactionReceiptData UTF8String];
}

//3  支付检测
-(void)purchaseProductStart
{
        if(self.validProduct != nil)
        {
            if(![self purchaseProduct:self.validProduct])
            {
                //status = UNOPEND_IN_APP_PURCHASE;
                UIAlertView *alert = [[UIAlertView alloc] initWithTitle:@"提示" message:@"当前设备未开启应用内支付" delegate:self cancelButtonTitle:@"取消" otherButtonTitles:nil, nil];
                [alert show];
                [alert release];
            }
        }
    
    //return status;
}


-(bool)purchaseProduct:(SKProduct *)requestedProduct
{
    if(requestedProduct != nil)
    {
        NSLog(@"LBPurchase purchaseProduct: %@",requestedProduct.productIdentifier);
        
        //检测当前设备是否开启应用内支付
        if([SKPaymentQueue canMakePayments])
        {
            //将productid绑定到payment上
            SKPayment *paymentRequest = [SKPayment paymentWithProduct:requestedProduct];
            
            //检测支付事务的状态
            [[SKPaymentQueue defaultQueue] addTransactionObserver:self];
            
            //请求支付
            [[SKPaymentQueue defaultQueue] addPayment:paymentRequest];
            
            return YES;
        }else{
            //通知用户开启应用内支付
            NSLog(@"当前账号并未开启应用内支付");
            return  NO;
        }
    }else{
        NSLog(@"EBPurchase requestProduct: requestedProduct = NIL");
        return NO;
    }
}

-(bool)restorePurchase
{
    NSLog(@"LBPurchase restorePurchase...");
    
    //检测当前设备知否开启应用内支付
    if([SKPaymentQueue canMakePayments])
    {
        //检测支付事务的状态
        [[SKPaymentQueue defaultQueue] addTransactionObserver:self];
        //请求存储当前支付
        [[SKPaymentQueue defaultQueue] restoreCompletedTransactions];
        return YES;
    }else{
        NSLog(@"EBPurchase requestProduct: requestedProduct = NIL");
        return NO;
    }
}

# pragma mark -
# pragma mark SKProductsRequestDelegate Methors

// 返回请求的结果
-(void)productsRequest:(SKProductsRequest *)request didReceiveResponse:(SKProductsResponse *)response
{
    // 转换接收到的商品信息
	self.validProduct = nil;
	int count = [response.products count];
	if (count>0) {
        // 获取第一个商品
        NSLog(@"商品数 > 0................%d",count);
		self.validProduct = [response.products objectAtIndex:0];
	}
    //检测商品是否有效
	if (self.validProduct) {
        NSLog(@"商品有效................");
    //检测是否有未完成的交易
        NSArray *transactions = [[SKPaymentQueue defaultQueue] transactions];
        if(transactions.count > 0)
        {
            SKPaymentTransaction *transaction = [transactions firstObject];
            if(transaction.transactionState == SKPaymentTransactionStatePurchased)
            {
                [self completeTransaction:transaction];
                return;
            }
        }
        [self purchaseProduct:self.validProduct];
	} else {
        // 当前商品并非是有效商品
        NSLog(@"当前商品并非是有效商品");
        UnitySendMessage( "IOS", "onPayFailCallback","");
    }
    
}

//完成事务
- (void)completeTransaction:(SKPaymentTransaction *)transaction {
    // Your application should implement these two methods.
    NSString * productIdentifier = transaction.payment.productIdentifier;
    if ([productIdentifier length] > 0) {
        // 向自己的服务器验证购买凭证
        UnitySendMessage( "IOS", "onPaySuccessCallback", [[transaction.transactionReceipt base64EncodedString] UTF8String]);
    }
    // Remove the transaction from the payment queue.
    [[SKPaymentQueue defaultQueue] finishTransaction: transaction];
    
}

//购买失败
- (void)failedTransaction:(SKPaymentTransaction *)transaction {
    if(transaction.error.code != SKErrorPaymentCancelled) {
        NSLog(@"购买失败");
    } else {
        NSLog(@"用户取消交易");
    }
    UnitySendMessage( "IOS", "onPayFailCallback","");
    [[SKPaymentQueue defaultQueue] finishTransaction: transaction];
}

#pragma mark -
#pragma mark SKPaymentTransactionObserver Methods

// 检测购买状态
- (void)paymentQueue:(SKPaymentQueue *)queue updatedTransactions:(NSArray *)transactions {
	for(SKPaymentTransaction *transaction in transactions) {
		switch (transaction.transactionState) {
				
			case SKPaymentTransactionStatePurchasing:
                NSLog(@"购买中。。。");
				// 购买中。。
				break;
				
			case SKPaymentTransactionStatePurchased:
				// 成功购买
				
				// 返回支付事务数据. 给用户提供商品.
                
				// 当用户支付成功后，给用户发商品
				// 清除支付队列中的支付事务
                [self completeTransaction:transaction];
                
				break;
				
			case SKPaymentTransactionStateRestored:
				// 确认用户已经成功支付之后
				// 存储该用户购买的商品，使该用户账号购买的商品在所有设备上都通用
				
				// 返回事务数据
                
				// 存储完用户购买该商品的事务后，清楚支付队列中该事务
				[[SKPaymentQueue defaultQueue] finishTransaction: transaction];
				break;
				
			case SKPaymentTransactionStateFailed:
				// 支付失败

                
                [self failedTransaction:transaction];
				// 完成支付状态检测后，清除支付队列中该事务
				[[SKPaymentQueue defaultQueue] finishTransaction: transaction];
				break;
		}
	}
}

// 清除支付队列中的事务
- (void)paymentQueue:(SKPaymentQueue *)queue removedTransactions:(NSArray *)transactions
{
    NSLog(@"LBPurchase removedTransactions");
    
    // 当支付完成或者取消之后释放资源
    [[SKPaymentQueue defaultQueue] removeTransactionObserver:self];
}

// 存储完该支付事务后结束支付
- (void)paymentQueueRestoreCompletedTransactionsFinished:(SKPaymentQueue *)queue {
    
    NSLog(@"LBPurchase paymentQueueRestoreCompletedTransactionsFinished");
    
    if ([queue.transactions count] == 0) {
        // 队列中没有支付事务.
        
        NSLog(@"EBPurchase restore queue.transactions count == 0");
        
        // 如果队列中没有支付事务，则释放检测状态资源
        [[SKPaymentQueue defaultQueue] removeTransactionObserver:self];
        
        
    } else {
        // 支付完成后，app需要给用户提供商品
        
        NSLog(@"LBPurchase restore queue.transactions available");
        
        for(SKPaymentTransaction *transaction in queue.transactions) {
            
            NSLog(@"LBPurchase restore queue.transactions - transaction data found");
            
        }
    }
}

// 支付失败
- (void)paymentQueue:(SKPaymentQueue *)queue restoreCompletedTransactionsFailedWithError:(NSError *)error
{
    // 支付失败，通知用户
    
    NSLog(@"LBPurchase restoreCompletedTransactionsFailedWithError");

}



- (void)dealloc
{
    
    [super dealloc];
}
@end

extern "C"{
    void *_purchasePluginsInit();
    int _requestProductPurchase(const char *productIdentifier,void *instance);
    void _restorePurchaseStart(void *instance);
    /*
     只有当requestProductPurchase方法中返回成功的时候才能去跟苹果服务器验证，
     如果返回其他两个状态，则无法支付，不需要去验证
     */
    const char* _getPurchaseReceipt(void *instance);
}

void *_purchasePluginsInit()
{
    id instance = [[PurchaseProductTransaction alloc] init];
    return (void *)instance;
}

int _requestProductPurchase(const char *productIdentifier,void *instance)
{
    PurchaseProductTransaction *purchase = (PurchaseProductTransaction *)instance;
    return [purchase requestProductPurchase:productIdentifier];
}

//void _purchaseProductStart(void *instance)
//{
//    PurchaseProductTransaction *purchase = (PurchaseProductTransaction *)instance;
//    [purchase purchaseProductStart];
//}

const char* _getPurchaseReceipt(void *instance)
{
    PurchaseProductTransaction *purchase = (PurchaseProductTransaction *)instance;
    return [purchase getPurchaseTransactionReceipt];
}

void _restorePurchaseStart(void *instance)
{
    PurchaseProductTransaction *purchase = (PurchaseProductTransaction *)instance;;
    [purchase restorePurchaseStart];
}