//
//  BMIChangeDelegate.h
//  L9_BMI_Calculator
//
//  Created by jansaharju on 13/02/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import <Foundation/Foundation.h>

@protocol BMIChangeDelegate <NSObject>

-(void)bmiChanged:(float) newBmi;

@end
