#Specs for pasks app

## General

pasks is a simple task management app backed by Producteev (https://www.producteev.com/) service. The app offers the user interactions defined in User stories section. An internet connection is required for the app to work properly. Networking is implemented as RESTful over HTTPS.

## Project work requirements

The project work is of type 'Own project'.

 * Compulsory baseline requirements are fulfilled as stated in the project work pdf
  * l10n: English, Finnish 
 * Compulsory UI requirements are fulfilled as stated in the project work pdf
  * Optional UI requirements would add no value to the app as it is

## User stories

I'll implement a subset of Producteev (task management cloud service) REST API interactions (https://www.producteev.com/api/doc/), specifically those parts of Tasks API that allow user to interact with tasks assigned to herself

 * User can list her tasks
      * ...that belong to a certain project or is labeled with a specific label
      * The ordering of the task list can be modified according to specific metadata
 * User can create a new task
 * User can view an existing task (specifics)
 * User can delete an existing task
 * User can update an existing task
