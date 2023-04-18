CREATE MIGRATION m17liixbuuueff2o2mjnwg5q5t3vgybkhdxoi4cqhkavsg4niyzjkq
    ONTO m1n6x6nagwpojhb7eljgxmmjenhe3dote2m35hpdl2czqkpabledhq
{
  ALTER TYPE default::MeteoStation {
      ALTER PROPERTY Lattitude {
          RENAME TO Latitude;
      };
  };
};
