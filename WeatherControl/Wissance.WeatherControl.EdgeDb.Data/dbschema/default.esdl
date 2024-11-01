module default {

    type MeasureUnit {
        required property Name -> str;
        required property Abbreviation -> str;
        property Description -> str;
    }

    type Measurement {
        required property SampleDate -> datetime;
        required property Value -> decimal;
        link Sensor -> Sensor;
    }

    type Sensor {
        required property Name -> str;
        required link Unit -> MeasureUnit;
        multi link Measurements -> Measurement;
        required property Longitude -> str;
        required property Latitude -> str;
    }

    type Station {
        required property Longitude -> str;
        required property Latitude -> str;
        multi link Sensors -> Sensor;
    }
}
