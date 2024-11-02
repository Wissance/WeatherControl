CREATE MIGRATION m1dpcz442zoyt25ttqkcmbd5s3zg3btgd2nbmj6bg73cc4fuw67lpq
    ONTO initial
{
  CREATE TYPE default::MeasureUnit {
      CREATE REQUIRED PROPERTY Abbreviation: std::str;
      CREATE PROPERTY Description: std::str;
      CREATE REQUIRED PROPERTY Name: std::str;
  };
  CREATE TYPE default::Sensor {
      CREATE REQUIRED LINK Unit: default::MeasureUnit;
      CREATE PROPERTY Description: std::str;
      CREATE REQUIRED PROPERTY Latitude: std::str;
      CREATE REQUIRED PROPERTY Longitude: std::str;
      CREATE REQUIRED PROPERTY Name: std::str;
  };
  CREATE TYPE default::Measurement {
      CREATE LINK Sensor: default::Sensor;
      CREATE REQUIRED PROPERTY SampleDate: std::datetime;
      CREATE REQUIRED PROPERTY Value: std::decimal;
  };
  ALTER TYPE default::Sensor {
      CREATE MULTI LINK Measurements: default::Measurement;
  };
  CREATE TYPE default::Station {
      CREATE MULTI LINK Sensors: default::Sensor;
      CREATE PROPERTY Description: std::str;
      CREATE REQUIRED PROPERTY Latitude: std::str;
      CREATE REQUIRED PROPERTY Longitude: std::str;
      CREATE REQUIRED PROPERTY Name: std::str;
  };
  ALTER TYPE default::Sensor {
      CREATE REQUIRED LINK Station: default::Station;
  };
};