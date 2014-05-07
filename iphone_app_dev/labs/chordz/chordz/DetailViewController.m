//
//  DetailViewController.m
//  chordz
//
//  Created by jansaharju on 09/04/14.
//  Copyright (c) 2014 ___FULLUSERNAME___. All rights reserved.
//

#import "DetailViewController.h"
#import "Chord.h"

@interface DetailViewController () {
    int layerCount;
}

- (void)configureView;

@end

@implementation DetailViewController

#pragma mark - Managing the detail item
- (IBAction)editedName:(id)sender {
    NSLog(@"edited name");
    [_detailItem setName:self.nameField.text];
    self.title = [self.detailItem name];

    // call delegate
    [[_detailItem delegate] chordChanged: _detailItem];

}

- (void)setDetailItem:(id)newDetailItem
{
    if (_detailItem != newDetailItem) {
        _detailItem = newDetailItem;
        
        // Update the view.
        [self configureView];
    }
    
}

- (void)configureView
{
    // Update the user interface for the detail item.

    if (self.detailItem) {
        self.detailDescriptionLabel.text = [self.detailItem name];
        self.nameField.text = [self.detailItem name];
        
        self.title = [self.detailItem name];
    }
}

- (void)viewDidLoad
{
    [super viewDidLoad];
	// Do any additional setup after loading the view, typically from a nib.
    [self configureView];
    
    layerCount = 0;
    [self drawPositions];
    
    UITapGestureRecognizer *recognizer = [[UITapGestureRecognizer alloc]
                                            initWithTarget:self action:@selector(handleTapFrom:)];
    [self.view addGestureRecognizer:recognizer];
    
    
}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (void)drawPositions {
    const int basex = 70;
    const int basey = 220;
    const int height = 250;
    const int hspace = 36;
    const int vspace = 55;
    const int width = 36 * 5;
    
    // strings
    for (int i = 0; i < 6; i++) {
        UIBezierPath *path = [UIBezierPath bezierPath];
        [path moveToPoint:CGPointMake(basex + i * hspace, basey)];
        [path addLineToPoint:CGPointMake(basex + i * hspace, basey + height)];
        
        CAShapeLayer *shapeLayer = [CAShapeLayer layer];
        shapeLayer.path = [path CGPath];
        shapeLayer.strokeColor = [[UIColor blueColor] CGColor];
        shapeLayer.lineWidth = 3.0;
        shapeLayer.fillColor = [[UIColor clearColor] CGColor];
        
        layerCount++;
        [self.view.layer addSublayer:shapeLayer];
    }
    
    // frets
    for (int i = 0; i < 5; i++) {
        UIBezierPath *path = [UIBezierPath bezierPath];
        [path moveToPoint:CGPointMake(basex, basey + i * vspace)];
        [path addLineToPoint:CGPointMake(basex + width, basey + i * vspace)];
        
        CAShapeLayer *shapeLayer = [CAShapeLayer layer];
        shapeLayer.path = [path CGPath];
        shapeLayer.strokeColor = [[UIColor redColor] CGColor];
        shapeLayer.lineWidth = 6.0;
        shapeLayer.fillColor = [[UIColor clearColor] CGColor];
        
        layerCount++;
        [self.view.layer addSublayer:shapeLayer];
    }
    
    // positions
    int i = 0;
    for ( NSNumber *position in [_detailItem positions]) {
        //NSLog(@"%i", [position intValue]);
        
        int pos = [position intValue];
        if(pos > 0) {
        
            // Set up the shape of the circle
            int radius = 8;
            
            CAShapeLayer *circle = [CAShapeLayer layer];
            
            // Make a circular shape
            circle.path = [UIBezierPath bezierPathWithRoundedRect:CGRectMake(0, 0, 2.0*radius, 2.0*radius)
                                                     cornerRadius:radius].CGPath;
            // Center the shape in self.view
            //circle.position = CGPointMake(CGRectGetMidX(self.view.frame)-radius,
            //                              CGRectGetMidY(self.view.frame)-radius);
            
            circle.position = CGPointMake(basex - radius + i * hspace, basey - radius + pos * vspace - vspace / 2);
            
            // Configure the apperence of the circle
            circle.fillColor = [UIColor clearColor].CGColor;
            circle.strokeColor = [UIColor blackColor].CGColor;
            circle.lineWidth = 16;
            
            // Add to parent layer
            layerCount++;
            [self.view.layer addSublayer:circle];

        }
        i++;
    }
        
}

- (void)handleTapFrom:(UITapGestureRecognizer *)recognizer {
    
    
    //NSLog(@"handleTapFrom: %@", recognizer);
    if(recognizer.state == UIGestureRecognizerStateRecognized)
    {
        CGPoint point = [recognizer locationInView:recognizer.view];
        // again, point.x and point.y have the coordinates
        //NSLog(@"x: %f, y: %f", point.x, point.y);
        
        // check if positions should change
        const int basex = 70;
        const int basey = 220;
        //const int height = 250;
        const int hspace = 36;
        const int vspace = 55;
        //const int width = 36 * 5;
        
        // string 6
        if(point.x > basex - hspace/2 && point.x < basex + hspace/2 &&
           point.y > basey && point.y < basey + vspace) {
            NSLog(@"string 6 fret 1 tapped!");
            [self togglePosition:6 forFret:1];
        }
        else if(point.x > basex - hspace/2 && point.x < basex + hspace/2 &&
                point.y > basey + vspace && point.y < basey + vspace*2) {
            NSLog(@"string 6 fret 2 tapped!");
            [self togglePosition:6 forFret:2];
        }
        else if(point.x > basex - hspace/2 && point.x < basex + hspace/2 &&
                point.y > basey + vspace*2 && point.y < basey + vspace*3) {
            NSLog(@"string 6 fret 3 tapped!");
            [self togglePosition:6 forFret:3];
        }
        else if(point.x > basex - hspace/2 && point.x < basex + hspace/2 &&
                point.y > basey + vspace*3 && point.y < basey + vspace*4) {
            NSLog(@"string 6 fret 4 tapped!");
            [self togglePosition:6 forFret:4];
        }
        // string 5
        else if(point.x > basex - hspace/2 + hspace && point.x < basex + hspace/2 + hspace &&
           point.y > basey && point.y < basey + vspace) {
            NSLog(@"string 5 fret 1 tapped!");
            [self togglePosition:5 forFret:1];
        }
        else if(point.x > basex - hspace/2 + hspace && point.x < basex + hspace/2 + hspace &&
                point.y > basey + vspace && point.y < basey + vspace*2) {
            NSLog(@"string 5 fret 2 tapped!");
            [self togglePosition:5 forFret:2];
        }
        else if(point.x > basex - hspace/2 + hspace && point.x < basex + hspace/2 + hspace &&
                point.y > basey + vspace*2 && point.y < basey + vspace*3) {
            NSLog(@"string 5 fret 3 tapped!");
            [self togglePosition:5 forFret:3];
        }
        else if(point.x > basex - hspace/2 + hspace && point.x < basex + hspace/2 + hspace &&
                point.y > basey + vspace*3 && point.y < basey + vspace*4) {
            NSLog(@"string 5 fret 4 tapped!");
            [self togglePosition:5 forFret:4];
        }
        // string 4
        else if(point.x > basex - hspace/2 + hspace*2 && point.x < basex + hspace/2 + hspace*2 &&
                point.y > basey && point.y < basey + vspace) {
            NSLog(@"string 4 fret 1 tapped!");
            [self togglePosition:4 forFret:1];
        }
        else if(point.x > basex - hspace/2 + hspace*2 && point.x < basex + hspace/2 + hspace*2 &&
                point.y > basey + vspace && point.y < basey + vspace*2) {
            NSLog(@"string 4 fret 2 tapped!");
            [self togglePosition:4 forFret:2];
        }
        else if(point.x > basex - hspace/2 + hspace*2 && point.x < basex + hspace/2 + hspace*3 &&
                point.y > basey + vspace*2 && point.y < basey + vspace*3) {
            NSLog(@"string 4 fret 3 tapped!");
            [self togglePosition:4 forFret:3];
        }
        else if(point.x > basex - hspace/2 + hspace*2 && point.x < basex + hspace/2 + hspace*2 &&
                point.y > basey + vspace*3 && point.y < basey + vspace*4) {
            NSLog(@"string 4 fret 4 tapped!");
            [self togglePosition:4 forFret:4];
        }
        // string 3
        else if(point.x > basex - hspace/2 + hspace*3 && point.x < basex + hspace/2 + hspace*3 &&
                point.y > basey && point.y < basey + vspace) {
            NSLog(@"string 3 fret 1 tapped!");
            [self togglePosition:3 forFret:1];
        }
        else if(point.x > basex - hspace/2 + hspace*3 && point.x < basex + hspace/2 + hspace*3 &&
                point.y > basey + vspace && point.y < basey + vspace*2) {
            NSLog(@"string 3 fret 2 tapped!");
            [self togglePosition:3 forFret:2];
        }
        else if(point.x > basex - hspace/2 + hspace*3 && point.x < basex + hspace/2 + hspace*3 &&
                point.y > basey + vspace*2 && point.y < basey + vspace*3) {
            NSLog(@"string 3 fret 3 tapped!");
            [self togglePosition:3 forFret:3];
        }
        else if(point.x > basex - hspace/2 + hspace*3 && point.x < basex + hspace/2 + hspace*3 &&
                point.y > basey + vspace*3 && point.y < basey + vspace*4) {
            NSLog(@"string 3 fret 4 tapped!");
            [self togglePosition:3 forFret:4];
        }
        // string 2
        else if(point.x > basex - hspace/2 + hspace*4 && point.x < basex + hspace/2 + hspace*4 &&
                point.y > basey && point.y < basey + vspace) {
            NSLog(@"string 2 fret 1 tapped!");
            [self togglePosition:2 forFret:1];
        }
        else if(point.x > basex - hspace/2 + hspace*4 && point.x < basex + hspace/2 + hspace*4 &&
                point.y > basey + vspace && point.y < basey + vspace*2) {
            NSLog(@"string 2 fret 2 tapped!");
            [self togglePosition:2 forFret:2];
        }
        else if(point.x > basex - hspace/2 + hspace*4 && point.x < basex + hspace/2 + hspace*4 &&
                point.y > basey + vspace*2 && point.y < basey + vspace*3) {
            NSLog(@"string 2 fret 3 tapped!");
            [self togglePosition:2 forFret:3];
        }
        else if(point.x > basex - hspace/2 + hspace*4 && point.x < basex + hspace/2 + hspace*4 &&
                point.y > basey + vspace*3 && point.y < basey + vspace*4) {
            NSLog(@"string 2 fret 4 tapped!");
            [self togglePosition:2 forFret:4];
        }
        // string 1
        else if(point.x > basex - hspace/2 + hspace*5 && point.x < basex + hspace/2 + hspace*5 &&
                point.y > basey && point.y < basey + vspace) {
            NSLog(@"string 1 fret 1 tapped!");
            [self togglePosition:1 forFret:1];
        }
        else if(point.x > basex - hspace/2 + hspace*5 && point.x < basex + hspace/2 + hspace*5 &&
                point.y > basey + vspace && point.y < basey + vspace*2) {
            NSLog(@"string 1 fret 2 tapped!");
            [self togglePosition:1 forFret:2];
        }
        else if(point.x > basex - hspace/2 + hspace*5 && point.x < basex + hspace/2 + hspace*5 &&
                point.y > basey + vspace*2 && point.y < basey + vspace*3) {
            NSLog(@"string 1 fret 3 tapped!");
            [self togglePosition:1 forFret:3];
        }
        else if(point.x > basex - hspace/2 + hspace*5 && point.x < basex + hspace/2 + hspace*5 &&
                point.y > basey + vspace*3 && point.y < basey + vspace*4) {
            NSLog(@"string 1 fret 4 tapped!");
            [self togglePosition:1 forFret:4];
        }
        
        // if changed then redraw
        NSEnumerator *enumerator = [self.view.layer.sublayers reverseObjectEnumerator];
        for(CALayer *layer in enumerator) {
            //NSLog(@"layerCount: %i", layerCount);
            if(layerCount > 0) {
                [layer removeFromSuperlayer];
            }
            layerCount--;
        }   
        
        layerCount = 0;
        [self drawPositions];
    }
}

- (void) togglePosition: (int)string forFret: (int)fret {
    int currentState = [[[_detailItem positions] objectAtIndex: string-1] intValue];
    
    if(currentState == fret) {
        [[_detailItem positions] replaceObjectAtIndex: string-1 withObject: [NSNumber numberWithInt:0]];
    }
    else {
        [[_detailItem positions] replaceObjectAtIndex: string-1 withObject: [NSNumber numberWithInt:fret]];
    }
        
    // update master list via delegate
    [[_detailItem delegate] chordChanged: _detailItem];
}

@end
