//
//  lab5ViewController.m
//  L5_Save_and_display_slider_values
//
//  Created by jansaharju on 16/01/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import "lab5ViewController.h"

@interface lab5ViewController ()

@property (strong, nonatomic) NSMutableArray *values;
@property (strong, nonatomic) NSNumber *current;

@end

@implementation lab5ViewController
- (IBAction)sliderValueChanged:(id)sender forEvent:(UIEvent *)event {
    _current = [NSNumber numberWithFloat:[(UISlider*)sender value]];
    [_value setText:[NSString stringWithFormat:@"%@", [_current stringValue]]];
}

- (IBAction)valueSaved:(id)sender forEvent:(UIEvent *)event {
    if(_current == nil) return;
    if(_values == nil) _values = [NSMutableArray new];
    
    [_values addObject:_current];
    
    float first = [[_values objectAtIndex:0] floatValue];
    float sum = first, min = first, max = first;
    for (unsigned i = 1; i < [_values count]; i++){
        float n = [[_values objectAtIndex:i] floatValue];
        sum += n;
        if(n < min) min = n;
        if(n > max) max = n;
    }
    
    [_avg setText:[NSString stringWithFormat:@"%f", (sum/[_values count])]];
    [_min setText:[NSString stringWithFormat:@"%f", min]];
    [_max setText:[NSString stringWithFormat:@"%f", max]];
}

- (void)viewDidLoad
{
    [super viewDidLoad];
}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

@end
