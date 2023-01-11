module default {

    type MeasureUnit {
        required property Name -> str;
        required property Abbreviation -> str;
        property Description -> str;
    }

    type Measurement {
        required link Unit -> MeasureUnit;
        required property SampleDate -> datetime;
        required property Value -> Double;
    }

    type Sensor {
    
    }

    typ MeteoStation {

    }
}
