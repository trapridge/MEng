//
//  Calculator.m
//  L8_Calculator2
//
//  Created by jansaharju on 23/01/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import "Calculator.h"

@implementation Calculator

- (Calculator *)init {
    self = [super init];
    if(self != nil) {
        _current = [NSNumber numberWithInt:0];
        _previous = [NSNumber numberWithInt:0];
        _activeOperation = nil;
        _memory = [NSNumber numberWithInt:0];
    }
    return self;
}

- (NSNumber *)update:(NSNumber *)digit {
    if(_activeOperation != nil) {
        _previous = [NSNumber numberWithFloat:[_current floatValue]];
        _current = digit;
    }
    else {
        NSNumberFormatter *f = [[NSNumberFormatter alloc] init];
        [f setNumberStyle:NSNumberFormatterDecimalStyle];
        _current = [f numberFromString:[NSString stringWithFormat:@"%@%d",
                                        [_current stringValue], [digit intValue]]];
    }
    
    [self log];
    return _current;
}

- (NSNumber *)operation:(NSString *)operation {
    // =
    if([operation isEqualToString:@"="]) {
        [self calculate];
        _activeOperation = nil;
    }
    else {
        // + - * /
        if(_activeOperation != nil) {
            [self calculate];
        }
        _activeOperation = operation;
    }
    
    [self log];
    return _current;
}

- (NSNumber *)clear {
    _current = [NSNumber numberWithInt:0];
    _previous = [NSNumber numberWithInt:0];
    _activeOperation = nil;
    
    [self log];
    return _current;
}

- (void)calculate {
    if([_activeOperation isEqualToString:@"+"]) {
        _current = [NSNumber numberWithFloat:[_previous floatValue] + [_current floatValue]];
    }
    else if ([_activeOperation isEqualToString:@"-"]) {
        _current = [NSNumber numberWithFloat:[_previous floatValue] - [_current floatValue]];
    }
    else if ([_activeOperation isEqualToString:@"*"]) {
        _current = [NSNumber numberWithFloat:[_previous floatValue] * [_current floatValue]];
    }
    else if ([_activeOperation isEqualToString:@"/"]) {
        _current = [NSNumber numberWithFloat:[_previous floatValue] / [_current floatValue]];
    }
}

/*** Memory implementation ***/
- (void)addToMemory {
    _memory = [NSNumber numberWithFloat:[_memory floatValue] + [_current floatValue]];
}

- (void)subtractFromMemory {
    _memory = [NSNumber numberWithFloat:[_memory floatValue] - [_current floatValue]];
}

- (void)clearMemory {
    _memory = [NSNumber numberWithInt:0];
}

- (NSNumber *)memorize {
    _current = [NSNumber numberWithFloat:[_memory floatValue]];
    
    [self log];
    return _current;
}

/*** NSCoding implementation ***/
- (void)encodeWithCoder:(NSCoder *)aCoder {
    NSLog(@"Serializing memory: %f", [_memory floatValue]);
    [aCoder encodeObject:_memory forKey:@"memory"];
}

- (id)initWithCoder:(NSCoder *)aDecoder {
    self = [self init];
    if (!self) {
        return nil;
    }

    _memory = [aDecoder decodeObjectForKey:@"memory"];
    NSLog(@"Deserializing memory: %f", [_memory floatValue]);
    return self;
}

/*** Helpers ***/
- (void)log {
    NSLog(@"current %f, previous %f, op %@",
          [_current floatValue], [_previous floatValue], _activeOperation);
}

@end
