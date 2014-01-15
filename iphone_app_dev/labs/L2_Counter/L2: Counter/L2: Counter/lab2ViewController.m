//
//  lab2ViewController.m
//  L2: Counter
//
//  Created by jansaharju on 16/01/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import "lab2ViewController.h"

@interface lab2ViewController ()

@end

@implementation lab2ViewController

- (IBAction)incr:(id)sender {
    NSLog(@"increasing");
    self.value++;
    [self.label setText:[NSString stringWithFormat:@"%d", self.value]];
}

- (IBAction)reset:(id)sender {
    NSLog(@"resetting");
    self.value = 0;
    [self.label setText:[NSString stringWithFormat:@"%d", self.value]];
}

- (IBAction)decr:(id)sender {
    NSLog(@"decreasing");
    self.value--;
    [self.label setText:[NSString stringWithFormat:@"%d", self.value]];
}

- (void)viewDidLoad
{
    [super viewDidLoad];
	// Do any additional setup after loading the view, typically from a nib.
    self.value = 0;
    [self.label setText:[NSString stringWithFormat:@"%d", self.value]];
}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

@end
