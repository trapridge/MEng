//
//  lab6ViewController.m
//  L6_Show_text_lines
//
//  Created by jansaharju on 17/01/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import "lab6ViewController.h"
#import "Lines.h"

@interface lab6ViewController ()

@property (strong, nonatomic) Lines *lines;

@end

@implementation lab6ViewController

- (IBAction)edited:(id)sender forEvent:(UIEvent *)event {
    // Add new line to model
    NSString *s = [(UITextField*)sender text];
    [_lines add:s];
    
    // Update text view
    _textView.text = [_lines description];
}

- (void)viewDidLoad
{
    [super viewDidLoad];
	// Do any additional setup after loading the view, typically from a nib.
    _lines = [[Lines alloc] init];
}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

@end
