//
//  lab3ViewController.m
//  L3_Color_Selector
//
//  Created by jansaharju on 16/01/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import "lab3ViewController.h"

@interface lab3ViewController ()

@end

@implementation lab3ViewController

- (IBAction)redChanged:(id)sender {
    self.red = [(UISlider*)sender value];
    
    self.view.backgroundColor = [UIColor colorWithRed: self.red green: self.green blue: self.blue alpha: 1 ];
}

- (IBAction)greenChanged:(id)sender {
    self.green = [(UISlider*)sender value];
    
    self.view.backgroundColor = [UIColor colorWithRed: self.red green: self.green blue: self.blue alpha: 1 ];
}

- (IBAction)blueChanged:(id)sender {
    self.blue = [(UISlider*)sender value];
    
    self.view.backgroundColor = [UIColor colorWithRed: self.red green: self.green blue: self.blue alpha: 1 ];
}

- (void)viewDidLoad
{
    [super viewDidLoad];
	// Do any additional setup after loading the view, typically from a nib.
    self.red = 0;
    self.green = 0;
    self.blue = 0;
    self.view.backgroundColor = [UIColor colorWithRed: self.red green: self.green blue: self.blue alpha: 1 ];
}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

@end
