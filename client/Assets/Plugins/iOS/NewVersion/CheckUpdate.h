//
//  CheckUpdate.h
//  Unity-iPhone
//
//  Created by sun on 14-3-31.
//
//

#import <Foundation/Foundation.h>

@interface CheckUpdate : UIView<UIAlertViewDelegate>

@property(nonatomic,strong) NSString *url;

-(void)CheckNewVersion:(const char *)url;
@end
