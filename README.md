## Shop-Site
- A shopping site made with an Asp.net API and angular client-side.
---
## Build instructions
### To run backend API:
- [ASP.NET 5](https://dotnet.microsoft.com/en-us/apps/aspnet) Framework installed:
    - I'm using ***ASP.NET 5.0.404***
- Change directory to \API folder
- Execute `dotnet run` in project
    - Default: http://localhost:5000 and https://localhost:5001
---
### To run client side website:
- [Node and NPM](https://nodejs.org/en/) installed:
    - I'm using ***node.js v14.18.1*** and ***npm 6.14.15***
- Change directory into \client
- Run `npm install` to install dependancies.
- Execute `ng serve --ssl false` in folder. 
    - The ssl certificate is just locally accepted by my machine, so serve without it.
    - Default: http://localhost:4200/
---
