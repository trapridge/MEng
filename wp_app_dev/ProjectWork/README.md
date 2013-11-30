#Project report

## 1 Description

pasks is a simple task management app backed by Producteev (https://www.producteev.com/) service. The app offers the user interactions defined in User stories section. An internet connection is required for the app to work properly. Networking is implemented as RESTful over HTTPS.

The project work is of type 'Own project'. All compulsory features dictated by course's final project assignment have been implemented - none of the optional ones were implemented.

A test account can be used the try out the app: pasks13@gmail.com:aaaaaa

### User stories

I implemented a subset of Producteev (task management cloud service) REST API interactions (https://www.producteev.com/api/doc/), specifically those parts of Tasks API that allow user to interact with tasks assigned to herself:

 * User can list her tasks
      * ...that belong to a certain project or is labeled with a specific label (1)
      * The ordering of the task list can be modified according to specific metadata
 * User can create a new task (2)
 * User can view an existing task (specifics) (3)
 * User can delete an existing task
 * User can update an existing task

## 2 What was done

### Differences in plan vs final implementation

Numbers below correspond with user story listing above:

 1. Label filtering was omitted from final version
 2. Adding a new task was not implemented
 3. 'Edit/add task' view displays the same infomation that is visible in the task list

Other things that should be improved if development was to continue:

 * Most importantly the app should be ported from WP7 to WP8 - having to use an old platform was a bit silly
 * App bar should be localized
 * Code needs refactoring from MVVM point-of-view (esp. utilizing Command pattern)
 * Live tile could show information that actually added some value to the user
 * Error handling should be elaborated significantly
 * There should be a way to destroy login session to Producteev (WP7 API has serious limitations in modifying browser session data)
 * App could be refactored to have more offline features. Currently only authencation identifier is saved to persistent storage and all other information is short-lived and online connection is mandatory for all user intercations

## 3 Libraries etc.

 * Snippets from blogs and StackOverflow were used to find ways around the inconveniences of WP7 API
 * Tappable support was implemented with WP7 Toolkit's TiltEffect
 * Producteev JOSN data mapping was done with the help from: http://json2csharp.com/
 * Icons: http://yankoa.deviantart.com/art/MetroStation-183210118

