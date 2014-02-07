//
//  lab9ViewController.m
//  L9_BMI_Calculator
//
//  Created by jansaharju on 06/02/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import "lab9ViewController.h"

@interface lab9ViewController ()

@end

@implementation lab9ViewController

- (IBAction)nameChanged:(id)sender forEvent:(UIEvent *)event {
    NSLog(@"nameChanged");
}

- (void)viewDidLoad
{
    [super viewDidLoad];
    [_nameField setDelegate:[TextOnlyAcceptor new]];
    
	// Do any additional setup after loading the view, typically from a nib.
}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

@end
