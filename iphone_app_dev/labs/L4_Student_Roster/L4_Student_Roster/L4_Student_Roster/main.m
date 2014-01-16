//
//  main.m
//  L4_Student_Roster
//
//  Created by jansaharju on 16/01/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "Roster.h"
#import "Student.h"

#include <stdio.h>
#include <string.h>

#define CONT 0
#define DONE 1
#define MAXLINELEN  1024

int main(int argc, const char * argv[]) {
    
    @autoreleasepool {
        
        Roster *r = [[Roster alloc] init];
        
        int selection = CONT;
        char buffer[ MAXLINELEN ];
        
        printf("Student register. Commands are: add, show, search, delete, quit\n");
        
        do {
            printf("> ");
            fgets(buffer, MAXLINELEN, stdin);
            
            if(strcmp(buffer, "add\n") == 0) {
                printf("number: ");
                fgets(buffer, MAXLINELEN, stdin);
                int num = atoi(buffer);
                NSNumber *number = [NSNumber numberWithInt:num];
                
                printf("name: ");
                fgets(buffer, MAXLINELEN, stdin);
                NSString *name = [NSString stringWithCString:buffer encoding:[NSString defaultCStringEncoding]];
                
                Student *student = [[Student alloc] initWithNumber:number andName:name];
                [r addStudent:student];
                
            } else if(strcmp(buffer, "show\n") == 0) {
                for (Student *s in [r students]) {
                    [[s name] getCString:buffer maxLength:MAXLINELEN encoding:[NSString defaultCStringEncoding]];
                    printf("number: %d, name: %s", [[s number] intValue], buffer);
                }
                
            } else if(strcmp(buffer, "search\n") == 0) {
                printf("number: ");
                fgets(buffer, MAXLINELEN, stdin);
                int num = atoi(buffer);
                NSNumber *number = [NSNumber numberWithInt:num];
                
                for (Student *s in [r students]) {
                    if([[s number] isEqualToNumber:number]) {
                        [[s name] getCString:buffer maxLength:MAXLINELEN encoding:[NSString defaultCStringEncoding]];
                        printf("number: %d, name: %s", [[s number] intValue], buffer);
                    }
                }
                
            } else if(strcmp(buffer, "delete\n") == 0) {
                printf("number: ");
                fgets(buffer, MAXLINELEN, stdin);
                int num = atoi(buffer);
                NSNumber *number = [NSNumber numberWithInt:num];
                
                [r deleteStudent:number];
                
            } else if(strcmp(buffer, "quit\n") == 0)
                selection = DONE;
            
        } while (selection != DONE);
        
        return 0;
    }
}
