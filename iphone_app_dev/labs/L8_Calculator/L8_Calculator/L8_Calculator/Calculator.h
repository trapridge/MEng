//
//  Calculator.h
//  L8_Calculator
//
//  Created by jansaharju on 23/01/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface Calculator : NSObject

@property (strong, nonatomic) NSNumber *outcome;

- (NSNumber *)sum:(NSNumber *)number;
- (NSNumber *)multiply:(NSNumber *)number;
- (NSNumber *)divide:(NSNumber *)number;

@end
