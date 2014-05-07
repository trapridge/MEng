//
//  Chord.h
//  chordz
//
//  Created by jansaharju on 09/04/14.
//  Copyright (c) 2014 jansaharju. All rights reserved.
//

#import <Foundation/Foundation.h>

//
@class Chord;

@protocol ChordDelegate <NSObject>
- (void)chordChanged:(Chord *)chord;
@end
//

@interface Chord : NSObject

//
@property (nonatomic, weak) NSObject <ChordDelegate> *delegate;
//@property (strong, nonatomic) NSObject <BMIChangeDelegate> *delegate;
//

@property (strong, nonatomic) NSString *name;
@property (strong, nonatomic) NSMutableArray *positions;
@property int listIndex;

@end
