CREATE MIGRATION m1n6x6nagwpojhb7eljgxmmjenhe3dote2m35hpdl2czqkpabledhq
    ONTO m1dqpzrnstat7weoen7zm2gllshsgmjer2azmqjnvbqxrz4iqihv3a
{
  ALTER TYPE default::Measurement {
      CREATE LINK Sensor -> default::Sensor;
  };
};
