//
//  lab8ViewController.m
//  L8_Calculator2
//
//  Created by jansaharju on 23/01/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import "lab8ViewController.h"
#import "Calculator.h"

@interface lab8ViewController ()

@property (strong, nonatomic) Calculator *calculator;

@end

@implementation lab8ViewController

//@synthesize current;


// digit ops
- (IBAction)digitEntered:(id)sender {
    NSString *t = [[(UIButton*)sender titleLabel] text];
    NSNumberFormatter *nf = [NSNumberFormatter new];
    NSNumber *n = [nf numberFromString:t];
    
    if(n != nil) {
        NSLog(@"digitEntered");
        [_current setText: [NSString stringWithFormat:@"%@", [self.calculator update:n]]];
    }
    /*
    else if([t isEqualToString:@"."]) {
        
    }
    else if([t isEqualToString:@"-"]) {
        [_calculator negate];
    }
    */
}

// calculus ops
- (IBAction)operationChosen:(id)sender {
    NSLog(@"operationChosen");
    [_current setText: [NSString stringWithFormat:@"%@",
                        [_calculator operation: [[(UIButton*)sender titleLabel] text]]]];
}

- (IBAction)cleared:(id)sender {
    NSLog(@"cleared");
    [_current setText: [NSString stringWithFormat:@"%@", [_calculator clear]]];
}

// memory ops
- (IBAction)memoryAction:(id)sender {
    NSLog(@"memoryAction");
}

- (void)viewDidLoad
{
    [super viewDidLoad];
    _calculator = [[Calculator alloc] init];
	// Do any additional setup after loading the view, typically from a nib.
}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

@end
