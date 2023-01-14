CREATE MIGRATION m1dqpzrnstat7weoen7zm2gllshsgmjer2azmqjnvbqxrz4iqihv3a
    ONTO m1w5nh6az53k4dqe5qpqyafbbfu4gldireok75cmfj64bjrqjfcu7q
{
  ALTER TYPE default::Sensor {
      ALTER PROPERTY Lattitude {
          RENAME TO Latitude;
      };
  };
};
