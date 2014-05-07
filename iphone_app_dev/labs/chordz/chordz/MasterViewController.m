//
//  MasterViewController.m
//  chordz
//
//  Created by jansaharju on 09/04/14.
//  Copyright (c) 2014 ___FULLUSERNAME___. All rights reserved.
//

#import "MasterViewController.h"

#import "DetailViewController.h"
#import "Chord.h"

@interface MasterViewController () {
    NSMutableArray *_objects;
    NSMutableData *_responseData;
    NSURLConnection *_getConnection;
    NSURLConnection *_deleteConnection;
    NSURLConnection *_postConnection;
    //NSMutableArray *_chords;
    NSString *_owner;
}

- (void)configureCell:(UITableViewCell *)cell atIndexPath:(NSIndexPath *)indexPath;

@end


@implementation MasterViewController


- (void)awakeFromNib
{
    [super awakeFromNib];
}

- (void)viewDidLoad
{
    [super viewDidLoad];
	// Do any additional setup after loading the view, typically from a nib.
    self.navigationItem.leftBarButtonItem = self.editButtonItem;

    UIBarButtonItem *addButton = [[UIBarButtonItem alloc] initWithBarButtonSystemItem:UIBarButtonSystemItemAdd target:self action:@selector(insertNewObject:)];
    self.navigationItem.rightBarButtonItem = addButton;
    
    _owner = @"531edf8f41efcb00028055fe";
    
}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (void)insertNewObject:(id)sender
{
    NSManagedObjectContext *context = [self.fetchedResultsController managedObjectContext];
    NSEntityDescription *entity = [[self.fetchedResultsController fetchRequest] entity];
    NSManagedObject *newManagedObject = [NSEntityDescription insertNewObjectForEntityForName:[entity name] inManagedObjectContext:context];
    
    // If appropriate, configure the new managed object.
    // Normally you should use accessor methods, but using KVC here avoids the need to add a custom class to the template.
    [newManagedObject setValue:[NSDate date] forKey:@"timeStamp"];
    [newManagedObject setValue:[NSNumber numberWithInt:[_objects count]] forKey:@"chordCount"];
    
    // Save the context.
    NSError *error = nil;
    if (![context save:&error]) {
        // Replace this implementation with code to handle the error appropriately.
        // abort() causes the application to generate a crash log and terminate. You should not use this function in a shipping application, although it may be useful during development.
        NSLog(@"Unresolved error %@, %@", error, [error userInfo]);
        abort();
    }
    
    
    if (!_objects) {
        _objects = [[NSMutableArray alloc] init];
    }
    
    [_objects insertObject:[Chord new] atIndex:0];
    NSIndexPath *indexPath = [NSIndexPath indexPathForRow:0 inSection:0];
    
    [self.tableView insertRowsAtIndexPaths:@[indexPath] withRowAnimation:UITableViewRowAnimationAutomatic];
}

#pragma mark - Table View

 
- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section
{
    return _objects.count;
}

- (NSInteger)tableView:(UITableView *)tableView sectionForSectionIndexTitle:(NSString*)title atIndex:(NSInteger)index
{
    return index;
}

- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath
{
    UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier:@"Cell" forIndexPath:indexPath];

    Chord *c = _objects[indexPath.row];
    cell.textLabel.text = [c name];
    
    return cell;
}

- (BOOL)tableView:(UITableView *)tableView canEditRowAtIndexPath:(NSIndexPath *)indexPath
{
    // Return NO if you do not want the specified item to be editable.
    return YES;
}

- (void)tableView:(UITableView *)tableView commitEditingStyle:(UITableViewCellEditingStyle)editingStyle forRowAtIndexPath:(NSIndexPath *)indexPath
{
    if (editingStyle == UITableViewCellEditingStyleDelete) {
        [_objects removeObjectAtIndex:indexPath.row];
        [tableView deleteRowsAtIndexPaths:@[indexPath] withRowAnimation:UITableViewRowAnimationFade];
    } else if (editingStyle == UITableViewCellEditingStyleInsert) {
        // Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view.
    }
}

- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender
{
    if ([[segue identifier] isEqualToString:@"showDetail"]) {
        NSIndexPath *indexPath = [self.tableView indexPathForSelectedRow];
        Chord *object = _objects[indexPath.row];
        [object setListIndex:indexPath.row];
        [object setDelegate: self];
        [[segue destinationViewController] setDetailItem:object];
    }
    
    NSLog(@"here");
}

- (IBAction)loadAction:(id)sender {
    [self load];
    NSLog(@"loading");
}

- (IBAction)saveAction:(id)sender {
    [self save];
    NSLog(@"saving");
    
}

- (void)connection:(NSURLConnection *)connection didReceiveResponse:(NSURLResponse *)response {
    if(connection == _getConnection) {
        [_responseData setLength: 0];
    }
    else if(connection == _deleteConnection) {
        NSHTTPURLResponse* httpResponse = (NSHTTPURLResponse*)response;
        int code = [httpResponse statusCode];
        if(code == 200) {
        
            NSLog(@"deleted");
            NSLog(@"posting new");
            
            NSError *error;
            NSMutableArray *copy = [NSMutableArray new];
            
            for ( Chord *chord in _objects)
            {
                NSMutableDictionary *d = [NSMutableDictionary new];
                [d setObject:chord.name forKey:@"name"];
                [d setObject:chord.positions forKey:@"positions"];
                [d setObject:_owner forKey:@"owner"];
                [copy addObject: d];
            }
            
            NSData *jsonData = [NSJSONSerialization dataWithJSONObject: copy
                                                               options: 0
                                                                 error: &error];
            if (!jsonData) {
                NSLog(@"Got an error: %@", error);
            }
            else {
                
                NSString *jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
                NSLog(@"%@", jsonString);
                
                // Create the request.
                NSMutableURLRequest *request = [NSMutableURLRequest requestWithURL:[NSURL URLWithString:@"https://chordz.herokuapp.com/chords"]];
                [request addValue:@"application/json" forHTTPHeaderField:@"Content-Type"];
                [request setHTTPMethod :@"POST"];
                [request setHTTPBody: jsonData];
                 
                _postConnection = [[NSURLConnection alloc] initWithRequest:request delegate:self];
                 
                if(!_postConnection) {
                    NSLog(@"nothing received");
                }
            }
        }
    }
    else if(connection == _postConnection) {
        NSHTTPURLResponse* httpResponse = (NSHTTPURLResponse*)response;
        int code = [httpResponse statusCode];

        if(code == 201) {
            NSLog(@"created");
            [self load];
        }
    }
}

- (void)connection:(NSURLConnection *)connection didReceiveData:(NSData *)data {
    if(connection == _getConnection) {
        [_responseData appendData:data];
    }
}

- (NSCachedURLResponse *)connection:(NSURLConnection *)connection
                  willCacheResponse:(NSCachedURLResponse*)cachedResponse {
    return nil;
}

- (void)connectionDidFinishLoading:(NSURLConnection *)connection {
    if(connection == _getConnection) {
        NSError *error;
        NSMutableArray *chords = [NSJSONSerialization JSONObjectWithData:_responseData
                                        options:NSJSONReadingMutableContainers|NSJSONReadingMutableLeaves
                                        error:&error];
        if(error)
        {
            NSLog(@"%@", [error localizedDescription]);
        }
        else {
                _objects = [[NSMutableArray alloc] init];
            
            for ( NSDictionary *item in chords)
            {
                NSLog(@"Name: %@", item[@"name"] );
                
                Chord *c = [Chord new];
                c.name = item[@"name"];
                c.positions = item[@"positions"];
                [_objects addObject: c];
            }
            [self.tableView reloadData];
        }
    }
}

- (void)connection:(NSURLConnection *)connection didFailWithError:(NSError *)error {
    NSLog(@"Connection failed: %@", error);
}

- (void)load {
    NSURLRequest *request = [NSURLRequest requestWithURL:[NSURL URLWithString:@"https://chordz.herokuapp.com/chords"]];
    _getConnection = [[NSURLConnection alloc] initWithRequest:request delegate:self];
    
    if (_getConnection) {
        _responseData = [[NSMutableData alloc] init];
    } else {
        NSLog(@"nothing received");
    }
}

- (void)save {
    NSMutableURLRequest *request = [NSMutableURLRequest requestWithURL:[NSURL URLWithString:@"https://chordz.herokuapp.com/chords"]];
    [request setHTTPMethod:@"DELETE"];
    _deleteConnection = [[NSURLConnection alloc] initWithRequest:request delegate:self];
    
    if(!_deleteConnection) {
        NSLog(@"nothing received");
    }
}


- (void)chordChanged:(Chord *)chord
{
    [_objects replaceObjectAtIndex: chord.listIndex withObject: chord];
    [self.tableView reloadData];
}


#pragma mark - Fetched results controller

- (NSFetchedResultsController *)fetchedResultsController
{
    if (_fetchedResultsController != nil) {
        return _fetchedResultsController;
    }
    
    NSFetchRequest *fetchRequest = [[NSFetchRequest alloc] init];
    // Edit the entity name as appropriate.
    NSEntityDescription *entity = [NSEntityDescription entityForName:@"Event" inManagedObjectContext:self.managedObjectContext];
    [fetchRequest setEntity:entity];
    
    // Set the batch size to a suitable number.
    [fetchRequest setFetchBatchSize:20];
    
    // Edit the sort key as appropriate.
    NSSortDescriptor *sortDescriptor = [[NSSortDescriptor alloc] initWithKey:@"timeStamp" ascending:NO];
    NSArray *sortDescriptors = @[sortDescriptor];
    
    [fetchRequest setSortDescriptors:sortDescriptors];
    
    // Edit the section name key path and cache name if appropriate.
    // nil for section name key path means "no sections".
    NSFetchedResultsController *aFetchedResultsController = [[NSFetchedResultsController alloc] initWithFetchRequest:fetchRequest managedObjectContext:self.managedObjectContext sectionNameKeyPath:nil cacheName:@"Master"];
    aFetchedResultsController.delegate = self;
    self.fetchedResultsController = aFetchedResultsController;
    
	NSError *error = nil;
	if (![self.fetchedResultsController performFetch:&error]) {
        // Replace this implementation with code to handle the error appropriately.
        // abort() causes the application to generate a crash log and terminate. You should not use this function in a shipping application, although it may be useful during development.
	    NSLog(@"Unresolved error %@, %@", error, [error userInfo]);
	    abort();
	}
    
    return _fetchedResultsController;
}



- (void)configureCell:(UITableViewCell *)cell atIndexPath:(NSIndexPath *)indexPath
{}


@end
