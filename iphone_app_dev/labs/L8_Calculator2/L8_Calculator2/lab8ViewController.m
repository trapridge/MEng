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

// digit ops
- (IBAction)digitEntered:(id)sender {
    NSLog(@"digitEntered");
    
    NSString *t = [[(UIButton*)sender titleLabel] text];
    NSNumberFormatter *nf = [NSNumberFormatter new];
    NSNumber *n = [nf numberFromString:t];
    
    if(n != nil) {
        [_current setText: [NSString stringWithFormat:@"%@", [self.calculator update:n]]];
    }
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
    
    if([[[(UIButton*)sender titleLabel] text] isEqualToString: @"MC"]) {
        [_calculator clearMemory];
    }
    else if([[[(UIButton*)sender titleLabel] text] isEqualToString: @"M+"]) {
        [_calculator addToMemory];
    }
    else if([[[(UIButton*)sender titleLabel] text] isEqualToString: @"M-"]) {
        [_calculator subtractFromMemory];
    }
    else if([[[(UIButton*)sender titleLabel] text] isEqualToString: @"MR"]) {
        [_current setText: [NSString stringWithFormat:@"%@", [_calculator memorize]]];
    }
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
