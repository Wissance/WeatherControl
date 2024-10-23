## WeatherControl

![Manage you WeatherData with Wissance](https://github.com/Wissance/WeatherControl/blob/master/docs/cover.png)

This project uses [`Wissance.WebApiToolkit`](https://github.com/Wissance/WebApiToolkit) so please give us a star! And for this project too!

### 1. General description

This project is a **tutorial** about how to design `REST API` using `C#`, but it is also fully functional `Web API` (`REST`) for working with meteorological stations or with indoor conditions with sensors of any type.

1. `REST API` with `EntityFramework` `ORM` - `Wissance.WeatherControl.WebApi` project
2. `REST API` with `EdgeDb` `Graph DB` - `Wissance.WeatherControl.WebApi.V2` project

These 2 Project Had different data Model until version `2.0` (`1.x`, last - `1.6`), starting from `2.0` they have the same data model.

These projects targets multiple platforms - `netcore 3.1`, `net6` and `net8`.

### 2. Data Model

#### 2.1 Glossary / Domain object

* `Station` - weather station that has name, description, coordinates and it **can collect and store any number of parameters**, 1 measuring parameter - 1 `Sensor`;
* `Sensor` - sensor that measures physical values like `Temparature`, `Pressure`, `Humidity` and so on, 1 sensor measures only one 1 physical value;
* `MeasureUnit` - physical value itself that is measuring by `Sensor`.
* `Measurement` - Sensor values = timestamp + numeric (decimal) value.

### 3. REST API With Entity Framework

#### 3.1 Application Overview

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

#### 3.2 Overall usage scenario

This is a **very simple application (demo)**, if any feature is needed open new issue.

1. Application client create one or multiple station using Station (`/api/station`) resource (CRUD)
2. Client interacts with station, gets it measured data and store it using Measurements (`/api/measurements`) 
   resource (CRUD)

##### 3.2.1 Example of usage

It should be noted that Postman Requests stored in docs folder

##### 3.2.2 Operations with Station resource

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

3. There are two get endpoints:

* 3.1 to get `one by id` - `GET http://localhost:8058/api/station/1`
* 3.2 to get `collection with paging` - `GET http://localhost:8058/api/station/?page=1&size=10`

  ![Result of running get stations](https://github.com/Wissance/WeatherControl/blob/master/docs/get_stations_with_paging.png)

4. To delete station with id 1 use endpoint `DELETE http://localhost:8058/api/station/1`

##### 3.2.3 Operations with measurements resource

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

We got following result in the output:
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

### 4. REST API With EdgeDB

Here we've got a `net6.0` `REST` Service that have a slightly different data model:

* `MeasureUnit`
* `Measurement`
* `Sensor`
* `Station`

Data project is `Wissance.WeatherControl.GraphData`

![Relation between Models]()


#### 4.1 Configure Edge DB (Prerequisites)

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

1. Add proper connection string in the database section in `appsettings.Development.json`config file:
```json
"Application": {
    "Database": {
      "ConnStr": "edgedb://edgedb:VcJjK6blkKAV2MUTdJXzLPvS@localhost:10702/edgedb"
    }
  }
```

configuration string must have the following scheme: `edgedb://user:password@host:port/database`
you could see your project credential on `Windows` machine in a directory:
`%USER_PROFILE%\AppData\Local\EdgeDB\config\credentials`

#### 4.2 REST API With Edge DB

We are having following Key Items:

1. `Controllers` - we are using base classes from a `Wissance.WebApiToollit`, in this lib we have
   eithther controllers for read-only and for full `CRUD` resources.
2. `Managers` - classes that are responsible for manage all business logic, in this project we have
   only one manager class - `EdgeDbManager` that is common for `CRUD` operation over all resources 
3. `EqlResolver` - class that is responsible for association `model` (`resource`) with operation
   (`read` , `create`, `update` or `delete`)
4. `Factories` - static classes that constructs `DTO` from `Models` and params (dictionary for 
   `create` and `update` perform) from `DTO`.
   
   
##### 4.2.1 REST API Controllers

All controllers are located in a folder `Controllers`, just look how simply look full `CRUD` Controller:

```csharp
namespace Wissance.WeatherControl.WebApi.V2.Controllers
{
    public class MeasurementController : BasicCrudController<MeasurementDto, MeasurementEntity, Guid>
    {
        public MeasurementController(EdgeDBClient edgeDbClient)
        {
            Manager = new EdgeDbManager<MeasurementDto, MeasurementEntity, Guid>(ModelType.Measurement, edgeDbClient,
                MeasurementFactory.Create, MeasurementFactory.Create);
        }
    }
}
```


###### 4.2.2 Manager

We have only one manager for all controllers due to the power of C# generics we just have to pass
to `EdgeDbManager`:
* `modelType` that is using to find approptiate eql statements from `EqlResolver`
* `EdgeDbClient` client to edgedb database
* and 2 delegates that describes how to create representation (`DTO`) from model and how to convert
  `DTO` to parameters list for `insert` and `update` operations
  
##### 4.2.3 EqlResolver

Just a set of dictionaries every dictionary for one operation:
* get collection
* get one
* create
* update
* delete

##### 4.2.4 Factories for objects convertion

They are static classes in a `Factories` dicrectory, the looking quite simple:

```csharp
namespace Wissance.WeatherControl.WebApi.V2.Factories
{
    public static class SensorFactory
    {
        public static SensorDto Create(SensorEntity entity)
        {
            SensorDto dto = new SensorDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude
            };

            if (entity.Measurements.Any())
            {
                dto.Measurements = entity.Measurements.Select(m => MeasurementFactory.Create((m))).ToList();
            }

            return dto;
        }
        
        public static IDictionary<string, object?> Create(SensorDto dto, bool generateId)
        {
            IDictionary<string, object?> dict = new Dictionary<string, object?>()
            {
                {"Name", dto.Name},
                {"Latitude", dto.Latitude},
                {"Longitude", dto.Longitude},
                {"Measurements", dto.Measurements.Where(m => m.Id.HasValue)
                    .Select(m => m.Id.Value).ToArray()}
            };
            
            // TODO(this if for further getting created object)
            dict["id"] = generateId ? Guid.NewGuid() : dto.Id;

            return dict;
        }
    }
}
```

### 5. Contributors

<a href="https://github.com/Wissance/WeatherControl/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=Wissance/WeatherControl" />
</a>
