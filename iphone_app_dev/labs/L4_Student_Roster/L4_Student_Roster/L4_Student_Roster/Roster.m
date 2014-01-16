//
//  Roster.m
//  L4_Student_Roster
//
//  Created by jansaharju on 16/01/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import "Roster.h"
#import "Student.h"

@interface Roster ()
@end

@implementation Roster

@synthesize students;

-(void)addStudent:(Student *)student {
    if(students == nil)
        students = [NSMutableSet setWithObjects: nil];
    
    [students addObject: student];
}

-(void)deleteStudent:(NSNumber *)number {
    Student *foundStudent;
    
    for(Student *s in students) {
        if(s.number.intValue == number.intValue) {
            foundStudent = s;
        }
    }
    
    [students removeObject: foundStudent];
}

@end

