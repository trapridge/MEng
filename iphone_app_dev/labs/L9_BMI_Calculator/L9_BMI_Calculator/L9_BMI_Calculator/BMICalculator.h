//
//  BMICalculator.h
//  L9_BMI_Calculator
//
//  Created by jansaharju on 13/02/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "BMIChangeDelegate.h"

@interface BMICalculator : NSObject

@property (nonatomic) float bmi;
@property (strong, nonatomic) NSObject <BMIChangeDelegate> *delegate;

- (void)calculate: (int)mass : (int)height;

@end
