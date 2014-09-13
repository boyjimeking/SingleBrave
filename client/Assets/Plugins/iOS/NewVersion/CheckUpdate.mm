//
//  CheckUpdate.m
//  Unity-iPhone
//
//  Created by sun on 14-3-31.
//
//

#import "CheckUpdate.h"

@implementation CheckUpdate

@synthesize url;

-(void)CheckNewVersion:(const char *)appUrl
{
    self.url = [NSString stringWithUTF8String:appUrl];
    UIAlertView *alert = [[UIAlertView alloc] initWithTitle:@"更新" message:@"检测到新版本，请更新..." delegate:self cancelButtonTitle:@"确定" otherButtonTitles:nil, nil];
    [alert show];
    [alert release];
}

#pragma mark-
#pragma UIAlerview Delegate Method
-(void)alertView:(UIAlertView *)alertView clickedButtonAtIndex:(NSInteger)buttonIndex
{
    if (buttonIndex == 0) {
        [[UIApplication sharedApplication] openURL:[NSURL URLWithString:self.url]];
    }
}


@end

extern "C"
{
    void _Update(const char *url);
}

void _Update(const char *url)
{
    CheckUpdate *update = [[CheckUpdate alloc] init];
    [update CheckNewVersion:url];
}