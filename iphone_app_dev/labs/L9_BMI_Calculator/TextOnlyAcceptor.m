//
//  LowerCaseDelegate.m
//  L9_BMI_Calculator
//
//  Created by jansaharju on 06/02/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import "TextOnlyAcceptor.h"

@implementation TextOnlyAcceptor

- (BOOL)textField:(UITextField *)textField shouldChangeCharactersInRange:(NSRange)range
replacementString:(NSString *)string {
    
    // allow non-digits only
    if([string isEqualToString: @"0"] || [string intValue] != 0) {
        return NO;
    } else {
        return YES;
    }
    
}

- (BOOL)textFieldShouldReturn:(UITextField *)textField
{
    [textField.superview endEditing:YES];
    //[self.view EndEditing:YES];
    return YES;
}

@end
