//
//  Person.h
//  L9_BMI_Calculator
//
//  Created by jansaharju on 03/03/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface Person : NSObject <NSCoding>

@property (strong, nonatomic) NSString *name;
@property (strong, nonatomic) NSNumber *bmi;

@end
