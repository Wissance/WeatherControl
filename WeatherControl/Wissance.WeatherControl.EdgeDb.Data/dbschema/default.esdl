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
        property Description -> str;
        required property Longitude -> str;
        required property Latitude -> str;
        required link Unit -> MeasureUnit;
        multi link Measurements -> Measurement;
    }

    type Station {
        required property Name -> str;
        property Description -> str;
        required property Longitude -> str;
        required property Latitude -> str;
        multi link Sensors -> Sensor;
    }
}
