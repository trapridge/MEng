//
//  Roster.m
//  L4_Student_Roster
//
//  Created by jansaharju on 16/01/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import "Roster.h"
#import "Student.h"

@implementation Roster

@synthesize students;

- (Roster *)init {
    self = [super init];
    if(self) students = [NSMutableSet new];
    return self;
}

- (void)addStudent:(Student *)student {
    [students addObject: student];
}

- (void)deleteStudent:(NSNumber *)number {
    Student *foundStudent;
    for(Student *s in students) {
        if(s.number.intValue == number.intValue) {
            foundStudent = s;
            break;
        }
    }
    if(foundStudent != nil) [students removeObject: foundStudent];
}

@end
