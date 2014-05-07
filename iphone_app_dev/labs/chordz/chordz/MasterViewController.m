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
    NSArray *_sections;
    NSMutableData *_responseData;
    NSURLConnection *_getConnection;
    NSURLConnection *_deleteConnection;
    NSURLConnection *_postConnection;
    //NSMutableArray *_chords;
    NSString *_owner;
}

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
    
    _sections = [NSArray arrayWithObjects:@"#", @"a", @"b", @"c", @"d", @"e", @"f", @"g", @"h",
                 @"i", @"j", @"k", @"l", @"m", @"n", @"o", @"p", @"q", @"r", @"s", @"t", @"u",
                 @"v", @"w", @"x", @"y", @"z", nil];
    
    _owner = @"531edf8f41efcb00028055fe";
    
}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (void)insertNewObject:(id)sender
{
    if (!_objects) {
        _objects = [[NSMutableArray alloc] init];
    }
    
    //Initialize the dataArray
//    dataArray = [[NSMutableArray alloc] init];
    
    //First section data
//    NSArray *firstItemsArray = [[NSArray alloc] initWithObjects:@"Item 1", @"Item 2", @"Item 3", nil];
//    NSDictionary *firstItemsArrayDict = [NSDictionary dictionaryWithObject:firstItemsArray forKey:@"data"];
//    [dataArray addObject:firstItemsArrayDict];
    
    [_objects insertObject:[Chord new] atIndex:0];
    NSIndexPath *indexPath = [NSIndexPath indexPathForRow:0 inSection:0];
    
    [self.tableView insertRowsAtIndexPaths:@[indexPath] withRowAnimation:UITableViewRowAnimationAutomatic];
}

#pragma mark - Table View

/*
- (NSInteger)numberOfSectionsInTableView:(UITableView *)tableView
{
    return [_sections count];
}

- (NSArray *)sectionIndexTitlesForTableView:(UITableView *)tableView
{
    return _sections;
}
*/
 
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
    
    //cell.textLabel.text = _objects[indexPath.row][@"name"];
    
    //NSLog(@"hmm");
    
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

/*
- (NSString *)tableView:(UITableView *)tableView titleForHeaderInSection:(NSInteger)section
{
    return [_sections objectAtIndex:section];
}
*/

/*
// Override to support rearranging the table view.
- (void)tableView:(UITableView *)tableView moveRowAtIndexPath:(NSIndexPath *)fromIndexPath toIndexPath:(NSIndexPath *)toIndexPath
{
}
*/

/*
// Override to support conditional rearranging of the table view.
- (BOOL)tableView:(UITableView *)tableView canMoveRowAtIndexPath:(NSIndexPath *)indexPath
{
    // Return NO if you do not want the item to be re-orderable.
    return YES;
}
*/

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


@end
