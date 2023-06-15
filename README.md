# CarRentalSystem
A simple car delivery and return registration system.

## Framework to highlight
* .net 6
* Spectre.Console for interactive console app
* NUnit and Moq for unit test
* NLog for logging

## IDE Used
Visual Studio 2023.1.2

## Projects
### CarRentalSystem.Business
Contains main business logic which is implemented based on the requirement document. It provides interface contracts for 
connecting with any type of database.

### CarRentalSystem.Business.Tests
Contains unit tests for _CarRentalSystem.Business_. Here unit tests are based on mocked dependencies instead of really 
instances of the dependencies.

### CarRentalSystem.InMemoryRepository
An implementation of in-memory repository based on the contracts provided by _CarRentalSystem.Business_ to support
_CarRentalSystem.SampleConsoleApp_.

### CarRentalSystem.SampleConsoleApp
A simple app to simulate user interaction with the system. This project helps to better understand use cases and 
workflows and also provides an easy way to verify the system.

### CarRentalSystem.SampleConsoleApp.IntegrationTests
Contains integration tests where test are written based on actual instances of the dependencies. Only the view classes
are mocked.

## Build from command line
Run build.bat file which builds the whole solution and runs all the unit tests.

## Log file 
A CarRentalSystem.log file is generated under CarRentalSystem\CarRentalSystem.SampleConsoleApp\bin\$(Configuration)\net6.0\
folder whereas the $(Configuration) is either Debug or Release.

## How to test
Build the solution either using _build.bat_ file or using an IDE. Run \CarRentalSystem\CarRentalSystem.SampleConsoleApp\bin\$(Configuration)\net6.0\CarRentalSystem.SampleConsoleApp.exe.
When options are presented in the console, press keyboard up/down key to navigate between options and press Enter to 
select an option. Example date time format to use for pick up time or return time is (yyyy'-'MM'-'dd'T'HH':'mm') e.g. 2023-06-18T11:30.

## Remarks
As the main requirement of the test is to implement small part of the business logic hence implementation of a proper
web interface, proper server and proper database communication are skipped to save some time. 
