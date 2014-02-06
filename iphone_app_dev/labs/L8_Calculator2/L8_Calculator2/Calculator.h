//
//  Calculator.h
//  L8_Calculator2
//
//  Created by jansaharju on 23/01/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "Memory.h"

@interface Calculator : NSObject <Memory>

@property (strong, nonatomic) NSNumber *current;
@property (strong, nonatomic) NSNumber *previous;
@property (strong, nonatomic) NSString *activeOperation;
@property (strong, nonatomic) NSNumber *memory;

- (NSNumber *)update:(NSNumber *)digit;
//- (NSNumber *)negate;
- (NSNumber *)operation:(NSString *)operation;
- (NSNumber *)clear;

@end
