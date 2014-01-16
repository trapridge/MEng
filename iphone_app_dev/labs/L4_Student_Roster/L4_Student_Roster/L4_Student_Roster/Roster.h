//
//  Roster.h
//  L4_Student_Roster
//
//  Created by jansaharju on 16/01/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "Student.h"

@interface Roster : NSObject

@property (strong, nonatomic) NSMutableSet *students;

-(void)addStudent:(Student *)student;
-(void)deleteStudent:(NSNumber *)number;

@end
