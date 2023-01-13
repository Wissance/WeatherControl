CREATE MIGRATION m1w5nh6az53k4dqe5qpqyafbbfu4gldireok75cmfj64bjrqjfcu7q
    ONTO initial
{
  CREATE FUTURE nonrecursive_access_policies;
  CREATE TYPE default::MeasureUnit {
      CREATE REQUIRED PROPERTY Abbreviation -> std::str;
      CREATE PROPERTY Description -> std::str;
      CREATE REQUIRED PROPERTY Name -> std::str;
  };
  CREATE TYPE default::Measurement {
      CREATE REQUIRED LINK Unit -> default::MeasureUnit;
      CREATE REQUIRED PROPERTY SampleDate -> std::datetime;
      CREATE REQUIRED PROPERTY Value -> std::decimal;
  };
  CREATE TYPE default::Sensor {
      CREATE MULTI LINK Measurements -> default::Measurement;
      CREATE REQUIRED PROPERTY Lattitude -> std::str;
      CREATE REQUIRED PROPERTY Longitude -> std::str;
      CREATE REQUIRED PROPERTY Name -> std::str;
  };
  CREATE TYPE default::MeteoStation {
      CREATE MULTI LINK Sensors -> default::Sensor;
      CREATE REQUIRED PROPERTY Lattitude -> std::str;
      CREATE REQUIRED PROPERTY Longitude -> std::str;
  };
};
