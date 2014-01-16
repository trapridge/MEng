//
//  Student.m
//  L4_Student_Roster
//
//  Created by jansaharju on 16/01/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import "Student.h"

@implementation Student

@synthesize number, name;

-(Student *)initWithNumber:(NSNumber *)thisNumber andName:(NSString *)thisName {
    self = [super init];
    if(self) {
        number = thisNumber;
        name = thisName;
    }
    return self;
}

@end
