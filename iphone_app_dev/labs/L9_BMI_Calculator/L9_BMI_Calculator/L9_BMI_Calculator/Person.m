//
//  Person.m
//  L9_BMI_Calculator
//
//  Created by jansaharju on 03/03/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import "Person.h"

@implementation Person

- (void)encodeWithCoder:(NSCoder *)aCoder {
    NSLog(@"Serializing name: %@", _name);
    [aCoder encodeObject:_name forKey:@"name"];
    NSLog(@"Serializing bmi: %f", [_bmi floatValue]);
    [aCoder encodeObject:_bmi forKey:@"bmi"];
}

- (id)initWithCoder:(NSCoder *)aDecoder {
    self = [self init];
    if (!self) {
        return nil;
    }
    
    _name = [aDecoder decodeObjectForKey:@"name"];
    NSLog(@"Deserialized name: %@", _name);
    _bmi = [aDecoder decodeObjectForKey:@"bmi"];
    NSLog(@"Deserialized bmi: %f", [_bmi floatValue]);
    return self;
}

@end
