//
//  Student.h
//  L4_Student_Roster
//
//  Created by jansaharju on 16/01/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface Student : NSObject

@property (strong, nonatomic) NSNumber *number;
@property (strong, nonatomic) NSString *name;

- (Student *)initWithNumber:(NSNumber *)number andName:(NSString *)name;

@end
