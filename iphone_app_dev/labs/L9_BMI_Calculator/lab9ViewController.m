//
//  lab9ViewController.m
//  L9_BMI_Calculator
//
//  Created by jansaharju on 06/02/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import "lab9ViewController.h"
#import "TextOnlyAcceptor.h"

@interface lab9ViewController ()
    - (NSMutableArray *)populateMasses;
    - (NSMutableArray *)populateHeights;
@end

@implementation lab9ViewController

TextOnlyAcceptor *toa;


-(void)bmiChanged:(float) newBmi {
    NSLog(@"BMI is: %f", newBmi);
    
    if(newBmi <= 25) {
        [self.image setImage:[UIImage imageNamed:@"burger.jpeg"]];
    }
    else {
        [self.image setImage:[UIImage imageNamed:@"carrot.jpeg"]];
    }
    
    _person.bmi = [NSNumber numberWithFloat:newBmi];
}

- (IBAction)nameChanged:(id)sender forEvent:(UIEvent *)event {
    NSLog(@"nameChanged");
    _person.name = [_nameField text];
}

// UIPickerViewDataSource
// returns the number of 'columns' to display.
- (NSInteger)numberOfComponentsInPickerView:(UIPickerView *)pickerView {
    return 2;
}

// returns the # of rows in each component..
- (NSInteger)pickerView:(UIPickerView *)pickerView numberOfRowsInComponent:(NSInteger)component {
    //set number of rows
    if(component == 0)
    {
        return [self.masses count];
    }
    else
    {
        return [self.heights count];
    }
}

-(NSString *)pickerView:(UIPickerView *)pickerView titleForRow:(NSInteger)row   forComponent:(NSInteger)component
{
    if(component == 0)
    {
        return [self.masses objectAtIndex:row];
    }
    else
    {
        return [self.heights objectAtIndex:row];
    }
}

- (void)pickerView:(UIPickerView *)pickerView didSelectRow:(NSInteger)row   inComponent:(NSInteger)component
{
    if(component == 0) {
        self.currentMass = [[self.masses objectAtIndex:row] intValue];
    }
    else {
        self.currentHeight = [[self.heights objectAtIndex:row] intValue];
    }
    
    [self.bmiCalculator calculate: self.currentMass : self.currentHeight];
}

- (void)viewDidLoad
{
    [super viewDidLoad];
    toa = [TextOnlyAcceptor new];
    
    if(_person == nil) {
        _person = [Person new];
    }
    
    [_nameField setDelegate: toa];
    [_nameField setText: _person.name];
    
    self.masses = [self populateMasses];
    self.heights = [self populateHeights];
    
    self.bmiCalculator = [BMICalculator new];
    [self.bmiCalculator setDelegate:self];
    self.currentMass = 45;
    self.currentHeight = 140;
}

- (NSMutableArray *)populateMasses {
    NSMutableArray *output = [NSMutableArray new];
    for (int i = 45; i <= 145; i++)
    {
        [output addObject: [NSString stringWithFormat:@"%d", i]];
    }
    return output;
}

- (NSMutableArray *)populateHeights {
    NSMutableArray *output = [NSMutableArray new];
    for (int i = 140; i <= 210; i++)
    {
        [output addObject: [NSString stringWithFormat:@"%d", i]];
    }
    return output;
}
                    
- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (void)save {
    NSLog(@"saving");
    [NSKeyedArchiver archiveRootObject:_person toFile:[self getPersistencePath]];
}

- (void)resume {
    NSLog(@"resuming");
    _person = [NSKeyedUnarchiver unarchiveObjectWithFile:[self getPersistencePath]];
    
    
    [self bmiChanged:[_person.bmi floatValue]];
}

-(NSString *)getPersistencePath {
    NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
    NSString *documentsDirectory = [paths objectAtIndex:0];
    return [documentsDirectory stringByAppendingPathComponent:@"texts.plist"];
}

@end
