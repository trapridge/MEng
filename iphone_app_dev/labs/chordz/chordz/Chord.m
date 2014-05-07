//
//  Chord.m
//  chordz
//
//  Created by jansaharju on 09/04/14.
//  Copyright (c) 2014 jansaharju. All rights reserved.
//

#import "Chord.h"

@interface Chord ()



@end

@implementation Chord

- (Chord *)init {
    self = [super init];
    _name = @"draft";
    _positions = [NSMutableArray new];
    [_positions addObject: [NSNumber numberWithInt:-1]];
    [_positions addObject: [NSNumber numberWithInt:-1]];
    [_positions addObject: [NSNumber numberWithInt:-1]];
    [_positions addObject: [NSNumber numberWithInt:-1]];
    [_positions addObject: [NSNumber numberWithInt:-1]];
    [_positions addObject: [NSNumber numberWithInt:-1]];
    return self;
}

@end
