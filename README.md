# Web4 Project Sibian De Gussem: Recommendation app

- Student: [Sibian De Gussem](https://github.com/SibianDG) - 201970377
- Lecturer: [Benjamin Vertonghen](https://github.com/vertonghenb)
- App name: Recommendation app
- Description: This app gives you a recommendation on certain keywords. You can get a book, movie or serie/show as recommendation.

# How to run

## Requirements

- Microsoft SQL Server
- Angular 11 (+)

## Server

- Make sure that the SQL Server service is started.

```
cd Server/RecipeApi
dotnet watch run
```

or:
- Run the `Startup.cs` in the solution `API`

## Client

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 11.1.4.

### Development server

#### Quick run:

```
cd Client
npm install
ng build
npm start
```

#### Run with details explained:


- Go to the `client` folder and run `npm install` to install all the packages and dependencies.
- Run `npm start` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

### Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

### Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

### Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via [Protractor](http://www.protractortest.org/).

### Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.

---

## Getting started -  Production

### Client

```
cd Client
npm install
ng build --prod
```

### Server

```
cd Server/RecipeApi
dotnet run
```
