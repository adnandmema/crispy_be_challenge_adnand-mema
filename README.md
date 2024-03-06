# ToDo List Application
This application is build on purpose to prove my skills. The application business logic it's pretty simple; just a normal to do list.
Here I will explain different aspects of the applications like architecture, component, decisions I have made, etc.

## Project Architecture & Project Structure
This application is build using a version of Clean Architecture called Vertical Slice Architecture. I didn't use a boilerplate, but I got my favorite features from two other boilerplates.
Most of the project was based on [MyWarehouse](https://github.com/baratgabor/MyWarehouse/) solution. The author of this solution has a readme where he explain in details his project and his decisions. Personally I like how well structured this solution is, how well-organized are the dependencies and how VSA is implemented. However, I don't like the repository pattern used there. I am not a big fan of repository pattern because it makes code more complicated. With Entity Framework we can have a cleaner solution and a built in repository pattern. So, instead of repository pattern I have used directly Entity Framework, based on [jasontaylordev/CleanArchitecture](https://github.com/jasontaylordev/CleanArchitecture/). Jason's implementation follows the principles pf Clean Architecture and at the same time allow us to use Entity Framework on the Application layer, allowing us to have the separation betwwen Aplication layer and Infrastructure layer. However, he has made a trade-off there by having a dependency from EF on Application layer, and this has raised some disscusion on his solution. In my solution I have created 2 running projects, API and UI. This way we have a standalone web service which can be used for our UI project, or for other external web applications, mobile applications or other clients. The API projects has authentication support by using JWT Authentication. In API application we have authentication support, softdeletion support, logging support, versioning support, swagger support and all the CRUD API's based on REST API Standarts. On the UI application I didn't put to much effort. I have created a simple UI interface to login, show all the todo lists, delete them and create new todo lists. There is a lot to improve on that applicationin.

## Running the project
To run the project is pretty simple, since every running instance of it and every dependency run on docker. You can pull the latest version from the dev brach (supposing that prod it's only for production purpose) and run docker compose. Here are the steps to run the project:
1. Open Visual Studio 2022 and clone the repository.
2. Switch to dev branch and pull the latest version.
3. Make sure you have docker instaled on your machine.
4. Select docker-compose as running project and run it.
5. That's all!

## How to test application modules?
### API
After all the docker containers are up & running, you can navigate to https://localhost:5001/swagger/index.html to test the Swagger API. On that interface you have all the endpoints and an UI option to help you authenticate by username and password, without having to deal with JWT token. Click Authorize, enter username as admin and password as Admin123$ . These are the credentials of a user created automatically when the database migrations are done. Database migrations are done automatically, so no need to deal with it. After you login, you can test all the endpoints.
### Database
Database it's running on 5434 port, but you can access the pgAdmin4 interface on http://localhost:8888. Enter adnand.dev@gmail.com and as pgAdmin credentials and access the todoapp database by setting server name as todoapp.database, username as postgres and password as postgres .
### UI
You can access UI on https://localhost:5003 . You have a login button there and you can use username admin and password Admin123$ to login there. Than you click ToDo List and play with them. As I said earlier, I didn't put too much effort there, so you may face some ugly exceptions there.
### SonarQube
[SonarQube](https://www.sonarsource.com/products/sonarqube/) is the tool I have implemented in order to empower development teams with a code quality and security solution that deeply integrates into your enterprise environment; enabling you to deploy clean code consistently and reliably. You can access it on http://localhost:9000 , but it may require some extra steps for you to open the interface like installing Java JDK, setting up JAVA_HOME, creating an admin account, connecting it with the local project, etc. 
I didn't went deep on it to set up environment variables maybe in order to have it  everything ready on your first run, but I will provide some screenshots here:
![image](https://github.com/adnandmema/crispy_be_challenge_adnand-mema/assets/13554827/3785d4eb-bfa0-4208-9737-48b9851cd010)
![image](https://github.com/adnandmema/crispy_be_challenge_adnand-mema/assets/13554827/67a7ee3d-2c73-4f16-98fb-297905a04d53)
![image](https://github.com/adnandmema/crispy_be_challenge_adnand-mema/assets/13554827/ef4841c4-a675-438b-b59b-1c3daa58ba70)
As you can see, it shows all the code smells, security issues and general code quality.

## Summary & Notes
There is a lot to improve and to explain on this project, like setting up a Jenkins Pipeline, logging system, lot of code refactoring, configuration refactorings for different environments, secrets, etc. I really enjoyed this project and can't wait to work on similar tech stacks, so I can improve my skills and deliver high quality systems and best practice implementations.
