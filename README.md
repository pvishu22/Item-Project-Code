
GitHub Link for Angular Project
https://github.com/pvishu22/Angular-Project/

GitHub Link for Web API Project with test case project
https://github.com/pvishu22/Web-Api-Project-with-test-cases


List of Folders

1. ScreenShot (Word File having working application ScreenShot)
2. shop-bridge-app (Angular Project)
3. SQL Script (SQL Script for database and test cases) 
4. WebApplication1 (WEB API Project and Unit Test Case Project)

Note : SQL Script and Screenshot Folder is also in Angular Project GitHub Link

Database Deployment Steps

1. Open SQL Script folder and run the DBScript script file in SQL Server.




WEB API Project Deployment

1. Deploy ItemAPIProject (Path: WebApplication1\WebApplication1) on port number 49784 (http://localhost:49784)
Note: If ItemAPIProject is deployed on other than http://localhost:49784, then open item.service.ts file of Angular Project (Path: shop-bridge-app\src\app\Services) and change http://localhost:49784 URL in contructor to WEB API Project Hosted URL.




To run Shop Bridge Angular Application

1. Open the shop-bridge-app folder in Visual Studio Code and run the project




To Run Unit Test Cases

1. Open SQL Script folder and run the TestCasesSQLScript script file in SQL Server.
2. Open ItemProjectSolution.sln Solution File of WebApplication1 Folder in Visual Studio and run the test cases.

