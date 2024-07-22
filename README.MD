Department/SubDepartment Tree and Reminder Task


Clean Architecture Design

-Department.Core: Acting as the Domain Business Layer, and it doesn’t have any dependency on any other project 
-Department.UseCase: Which encapsulates the needed Use Cases for both the Department/SubDepartment and Reminder Modules, it depends only on the Core Project and just use Interface Plugin to be implemented by third-Party Infrastructure , also in expose Use Cases Interfaces to be Used by the Department.Web Application to ensure Loose coupling.
-Plugin.Efcore: Implement the IRepository exposed by the Department.UseCase Project using Using EFCORE
-Plugin.Service: Implement some services like the EmailService and MockEmailService which Both implement the IEmailService in the Department.UseCase
Department.Web:  this is the MVC application and the startup Project

Tests.DepartmentModule.UseCase.Tests: a sample Test Project using Xunit that includes a sample test class for one on the Department UseCase, I didn’t implement all the Test Case for the project and it is meant for Proof of Concept.

Cross-Cutting Concerns:
Although not implemented Everywhere I used the following Cross-Cutting Concern as a proof of Concept
-Logging: Serilog was added.
-Automapper: Automapper was used to map Entities to DTO and Vice versa in some Use Case.
-Error Handling: Error handling was added in some methods and UseCase 

Functionality:

I implemented the Following Functionality, I didn’t focus on UI and design but focus on the functionality as follow:

Department/Subs Module:

1-Developed a Tree View that show
-	the Whole Hierarchy of the Department/SubDepartment.
-	Has a button to add a root Department.
-	Has a Create/Add Sub-Department button beside each department to add a Sub-Department.
-	Has a View button beside Each  department to view the department Details.

2- Developed Sub-Department functionality:

It shows a dropdown that list all the top Level Department, once Selected , if this Department has a Child another drop down List will be displayed that List all its child Sub-department, and so on until the last level, if the there is no further children, it won’t display any further  dropdown and it show no further Subdepartment found

3-Developed Parent department functionality:

It shows A dropdown that show all the department and Subdepartment available , whenever we choose one and press View Parent department , it will list all its parents to the top level parent. 

Reminders Module:

1-Developed Create Reminders functionality

It takes Reminder Title and DateTime to create a reminder.

2- Developed Reminders List: to list all reminders.

3- Developed Email Service : that send email however , I enabled a mock Email service to avoid adding Email and password(we can add the email setting and enable the main Service in the Dependency Injection Containers in the program.cs

4-Developed Background Service that run periodically to check if there is reminder to send email.





   






 
