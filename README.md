## WeatherControl

### Glossary / Domain object

* Station - weather station that has name, description, coordinates and it **can measure one or multiple weather
parameters**;
* Measurements - one weather sample measured by station itself.

### Application Overview

Web API REST service (.Net Core) that could store weather data from multiple weather station:
* temperature
* atmosphere pressure
* humidity
* wind speed

Application has 2 resources = Domain objects

Application uses MsSql as Database Server (this could be easily changed, but this required to re-generate migration).

1. Station
2. Measurements

### Overall scenario

This is a very simple application (demo), if any feature is needed open new issue.

1. Application client create one or multiple station using Station (`/api/station`) resource (CRUD)
2. Client interacts with station, gets it measured data and store it using Measurements (`/api/measurements`) 
   resource (CRUD)

#### Example of usage

It should be noted that Postman Requests stored in docs folder

1. Create Station: