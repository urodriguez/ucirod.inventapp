# UciRod.Inventapp servers
http://localhost:8080 -> Dev
http://localhost:8082 -> Jenkins
http://localhost:8083 -> Test

{username: "urodriguez-admin", password: "admin"}

## TODO list - backend
* automapper -> DONE
* move tables to own db -> DONE
* refector repositories to use no hardcoded querys -> DONE
* implement dbConectionFactory -> DONE 
* log database queries -> DONE
* spread log db queries over all repo methods -> DONE
* remove LogMessage object in Log method for LogService, idem LogLevel -> DONE
* formatter for generated sql queries to avoid copy paste query parameter -> DONE
* use swagger -> DONE
* gitignore logs -> DONE
* apply internal on classes when is necessary -> DONE
* improve logs history with more files -> DONE
* use correlation id on logs -> DONE
* try-catch wrapping in controllers -> DONE
* use local IIS -> DONE
* use Jenkins -> DONE
* implement authentication use JWT to authenticate -> DONE
* add auth to swagger -> DONE

* implement auditing
* implement mailing
* implement reporting
* implement caching
* use hangfire
* implement integration events with NServiceBus
* implement at least one soap service
* implement import using Excel library
* create application to show logs (maybe will be necessary to migrate to a database)
* implement process to delete old logs (one mounth) folder (maybe directly in LogService)
* unit test
* implement await/async without Dapper (current using version is Dapper.Extensions and it is coupled with MiniProfiler.Integration that not support async) => use EF
* implement unityOfWork
* use TeamCity
* use Docker
* use PusherServer to notify UI on server changes
* deploy app to cloud 

## Angular architercure styleguide

https://angular.io/guide/styleguide

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.