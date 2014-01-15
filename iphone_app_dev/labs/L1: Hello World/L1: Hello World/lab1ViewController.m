//
//  lab1ViewController.m
//  L1: Hello World
//
//  Created by jansaharju on 15/01/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import "lab1ViewController.h"

@interface lab1ViewController ()

@end

@implementation lab1ViewController


- (IBAction)test:(id)sender
{
    [self.label setText:@"Hello, World!"];
    //NSLog(@"ok");
}

- (void)viewDidLoad
{
    [super viewDidLoad];
	// Do any additional setup after loading the view, typically from a nib.
}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

@end
