//
//  MasterViewController.h
//  chordz
//
//  Created by jansaharju on 09/04/14.
//  Copyright (c) 2014 ___FULLUSERNAME___. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "Chord.h"
#import <CoreData/CoreData.h>

@interface MasterViewController : UITableViewController<NSURLConnectionDelegate, ChordDelegate, NSFetchedResultsControllerDelegate>

@property (strong, nonatomic) NSFetchedResultsController *fetchedResultsController;
@property (strong, nonatomic) NSManagedObjectContext *managedObjectContext;

@end
