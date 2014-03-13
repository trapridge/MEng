//
//  Memory.h
//  L8_Calculator2
//
//  Created by jansaharju on 25/01/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import <Foundation/Foundation.h>

@protocol Memory <NSObject>

- (void)addToMemory;
- (void)subtractFromMemory;
- (void)clearMemory;
- (NSNumber *)memorize;

@end
