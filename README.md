## WeatherControl

This project use [`Wissance.WebApiToolkit`](https://github.com/Wissance/WebApiToolkit) so please give us a star!

### 1. General description

This project is a tutorial about how to design `REST API`, we are have here 2 simultaneously existing Web projects:

1. `REST API` with `EntityFramework` `ORM` - `Wissance.WeatherControl.WebApi` project
2. `REST API` with `EdgeDb` `Graph DB` - `Wissance.WeatherControl.WebApi.V2` project

These 2 Project Have different data Model

### 2. REST API With Net Framework

This project target platform is a `netcore 3.1`

#### 2.1 Glossary / Domain object

* `Station` - weather station that has name, description, coordinates and it **can measure one or 
  multiple weather parameters**;
* `Measurement` - one weather sample measured by station itself.

#### 2.2 Application Overview

Web API REST service (.Net Core) that could store weather data from multiple weather station. We 
assume that we are going to store/manage following physical value measurements getting from
appropriate sensors:
* `temperature`;
* `atmosphere pressure`;
* `humidity`;
* `wind speed`;

Application has 2 `resources` = Domain objects

Application uses MsSql as Database Server (this could be easily changed, but this required to re-generate migration).

1. `Station`
2. `Measurement`

#### 2.3 Overall usage scenario

This is a **very simple application (demo)**, if any feature is needed open new issue.

1. Application client create one or multiple station using Station (`/api/station`) resource (CRUD)
2. Client interacts with station, gets it measured data and store it using Measurements (`/api/measurements`) 
   resource (CRUD)

##### 2.3.1 Example of usage

It should be noted that Postman Requests stored in docs folder

##### 2.3.2 Operations with Station resource

1. Create Station:

`POST http://localhost:8058/api/station`

```json
{
	"id": 0,
	"name": "Yekaterinburg main station",
	"description": "Yekaterinburg meteo station (meteo mountain)",
	"longitude": "60°37'55\"E",
	"latitude": "56°49'36\"N"
}
```

![Result of running create station](https://github.com/Wissance/WeatherControl/blob/master/docs/create_station_example.png)

We got a Operation result response:
```json
{
    "success": true,
    "message": null,
    "data": {
        "id": 1,
        "name": "Yekaterinburg main station",
        "description": "Yekaterinburg meteo station (meteo mountain)",
        "longitude": "60°37'55\"E",
        "latitude": null
    }
}
```

2. Station data update (could be updated name, description and coordinates):

`PUT http://localhost:8058/api/station/1`

Body and response are the same as at Create operation:
```json
{
	"id": 0,
	"name": "Yekaterinburg main station",
	"description": "Yekaterinburg meteo station (meteo mountain)",
	"longitude": "60°37'55\"E",
	"latitude": "56°49'36\"N"
}
```
![Result of running update station](https://github.com/Wissance/WeatherControl/blob/master/docs/update_station_example.png)

3. There are two get operations:

* 3.1 to get one by id `GET http://localhost:8058/api/station/1`
* 3.2 to get collection with paging `GET http://localhost:8058/api/station/?page=1&size=10`

  ![Result of running get stations](https://github.com/Wissance/WeatherControl/blob/master/docs/get_stations_with_paging.png)

4. To delete station with id 1 use endpoint `DELETE http://localhost:8058/api/station/1`

##### 2.3.3 Operations with measurements resource

1. Create measurements

`POST http://localhost:8058/api/measurements`

```json
{
	"id": 0,
	"timestamp": "2022-05-24T10:13:43",
	"temperature": 16.1,
	"pressure": 742.3,
	"humidity": 60.5,
	"windSpeed": 0.5,
	"stationId": 1
}
```

We got following result in ouptup:
```json
{
    "success": true,
    "message": null,
    "data": {
        "id": 1,
        "timestamp": "2022-05-24T10:13:43",
        "temperature": 16.1,
        "pressure": 742.3,
        "humidity": 60.5,
        "windSpeed": 0.5,
        "stationId": 1
    }
}
```

![Result of running create measurements](https://github.com/Wissance/WeatherControl/blob/master/docs/create_measurements.png)

2. Update measurements: one or any number of weather parameters could be changed using 
   `PUT http://localhost:8058/api/measurements/1` with same body and result as at create measurements operation.
   
![Result of running update measurements](https://github.com/Wissance/WeatherControl/blob/master/docs/update_measurements.png)
   
3. There are two get operations:

* 3.1 to get one by id `GET http://localhost:8058/api/measurements/1`
* 3.2 to get collection with paging `GET http://localhost:8058/api/measurements/?page=1&size=10`

4. To delete measurements with id 1 use endpoint `DELETE http://localhost:8058/api/measurements/1`

### 3. REST API With EdgeDB

Here we've got a `net6.0` `REST` Service that have a slightly different data model:

* `MeasureUnit`
* `Measurement`
* `Sensor`
* `Station`

Data project is `Wissance.WeatherControl.GraphData`

![Relation between Models]()


#### 3.1 Configure Edge DB (Prerequisites)

1. Start `Edgedb` instance from `Wissance.WeatherControl.GraphData` directory

```ps1
edgedb instance start -I Wissance_WeatherControl_Data --foreground
```

apply migration via

```ps1
edgedb migrate
```

2. Configure Edgedb to allow pass own identifiers (necessary for object return after create)

```ps1
edgedb configure set allow_user_specified_id true
```

2. Start edgedb ui:

```ps1
edgedb ui
```

Once you loaded project you could use it in current application:

1. Add proper connection string in Database section in `appsettings.Development.json` file:
```json
"Application": {
    "Database": {
      "ConnStr": "edgedb://edgedb:VcJjK6blkKAV2MUTdJXzLPvS@localhost:10702/edgedb"
    }
  }
```

configuration string must have following scheme: `edgedb://user:password@host:port/database`
you could see your project credential on `Windows` machine could be found in directory:
`%USER_PROFILE%\AppData\Local\EdgeDB\config\credentials`

#### 3.2 REST API With Edge DB

We are having following Key Items:

1. `Controllers`
2. `Managers`
3. `EqlResolver`
4. `Factories`


