//
//  BMICalculator.m
//  L9_BMI_Calculator
//
//  Created by jansaharju on 13/02/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import "BMICalculator.h"

@implementation BMICalculator

- (BMICalculator *)init {
    self = [super init];
    return self;
}

- (void)calculate: (int)mass : (int)height {
    self.bmi = (float)mass / ((float)height/100 * (float)height/100);
    
    if(self.delegate != nil) {
        [self.delegate bmiChanged:self.bmi];        
    }

}

@end
