//
//  lab9ViewController.h
//  L9_BMI_Calculator
//
//  Created by jansaharju on 06/02/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "BMICalculator.h"
#import "BMIChangeDelegate.h"

@interface lab9ViewController : UIViewController <UIPickerViewDataSource,UIPickerViewDelegate,BMIChangeDelegate>

@property (strong, nonatomic) IBOutlet UIImageView *image;

@property (strong, nonatomic) IBOutlet UITextField *nameField;

@property (strong, nonatomic) IBOutlet UIPickerView *picker;

@property (strong, nonatomic) NSMutableArray *masses;
@property (strong, nonatomic) NSMutableArray *heights;

@property (strong, nonatomic) BMICalculator *bmiCalculator;

@property (nonatomic) int currentMass;
@property (nonatomic) int currentHeight;

@end
