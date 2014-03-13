//
//  masterdetailtestDetailViewController.h
//  masterdetailtest
//
//  Created by jansaharju on 27/02/14.
//  Copyright (c) 2014 m0000892. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface masterdetailtestDetailViewController : UIViewController

@property (strong, nonatomic) id detailItem;

@property (weak, nonatomic) IBOutlet UILabel *detailDescriptionLabel;
@end
