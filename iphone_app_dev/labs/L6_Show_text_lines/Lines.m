//
//  Lines.m
//  L6_Show_text_lines
//
//  Created by jansaharju on 17/01/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import "Lines.h"

@interface Lines ()

@property (strong, nonatomic) NSMutableArray *lines;
@property (nonatomic) int lineNumber;

@end

@implementation Lines

- (void)add:(NSString *)line {
    // Lazy init
    if(_lines == nil) {
        _lines = [[NSMutableArray alloc] init];
        _lineNumber = 1;
    }
    
    // Concat line number, and add to array
    NSString *numberedLine = [NSString stringWithFormat:@"%d %@", _lineNumber++, line];
    [_lines addObject:numberedLine];
}

- (NSString *)description {
    NSString *output = [[NSString alloc] init];
    for(NSString *line in _lines) {
        // concat all elements as lines
        output = [NSString stringWithFormat:@"%@\n%@", output, line];
    }
    return output;
}

@end
