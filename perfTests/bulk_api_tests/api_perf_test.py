import json
import aiohttp
import asyncio
import random
import requests
import datetime
import time
import timeit


async def main():
    print("Testing API performance vs Bulk API on WeatherControl application with EF started")
    await run_non_bulk_api_with_ef(10)
    await run_bulk_api_with_ef(10)
    print("Testing API performance vs Bulk API on WeatherControl application with EF finished")
    print("Testing API performance vs Bulk API on WeatherControl application with EdgeDB started")
    await run_non_bulk_api_with_edgedb(10)
    await run_bulk_api_with_edgedb(10)
    print("Testing API performance vs Bulk API on WeatherControl application with EdgeDB finished")


async def run_non_bulk_api_with_ef(num_of_objs: int):
    measurements_res = "http://127.0.0.1:8058/api/Measurements"
    measurements = list()
    for i in range(0, num_of_objs):
        measurements.append({
            "timestamp": str(datetime.datetime.utcnow().strftime("%Y-%m-%dT%H:%M:%S")),
            "temperature": random.random() * 20,
            "pressure": random.randint(740, 770),
            "humidity": random.randint(60, 80),
            "windSpeed": random.randint(4, 15),
            "stationId": 1
        })

    headers = {
        'Content-type': 'application/json',
        'Accept': 'application/json'
    }

    start = timeit.default_timer()
    async with aiohttp.ClientSession() as session:
        for i in range(0, num_of_objs):
            body = measurements[i]
            async with session.post(measurements_res, json=body, headers=headers, ssl=False) as resp:
                if resp.status == 201:
                    created_mes = await resp.json()
    end = timeit.default_timer()
    dt = round((end - start) * 10 ** 6, 3)
        # end - start
    print(f"Elapsed time in Non-Bulk REST API with EF on creation of {num_of_objs} items is {dt} us.")


async def run_bulk_api_with_ef(num_of_objs: int):
    measurements_res = "http://127.0.0.1:8058/api/MeasurementsSeries"
    measurements = list()
    for i in range(0, num_of_objs):
        measurements.append({
            "timestamp": str(datetime.datetime.utcnow().strftime("%Y-%m-%dT%H:%M:%S")),
            "temperature": random.random() * 20,
            "pressure": random.randint(740, 770),
            "humidity": random.randint(60, 80),
            "windSpeed": random.randint(4, 15),
            "stationId": 1
        })

    headers = {
        'Content-type': 'application/json',
        'Accept': 'application/json'
    }

    start = timeit.default_timer()
    async with aiohttp.ClientSession() as session:
        body = measurements
        async with session.post(measurements_res, json=body, headers=headers, ssl=False) as resp:
            if resp.status == 201:
                created_mes = await resp.json()
    end = timeit.default_timer()
    dt = round((end-start) * 10 ** 6, 3)# end - start
    print(f"Elapsed time in Bulk API with EF on creation of {num_of_objs} items is {dt} us.")


async def run_non_bulk_api_with_edgedb(num_of_objs: int):
    measurements_res = "http://127.0.0.1:8059/api/Measurement"
    measurements = list()
    for i in range(0, num_of_objs):
        measurements.append({
            "sampleDate": str(datetime.datetime.utcnow().strftime("%Y-%m-%dT%H:%M:%S")),
            "value": random.random() * 25,
            "measureUnitId": "604a0412-ea47-11ed-b9a1-bba352c6e579",
            "sensorId": "30ac8366-ea4b-11ed-ab17-e38db76a95f0"
        })

    headers = {
        'Content-type': 'application/json',
        'Accept': 'application/json'
    }

    start = timeit.default_timer()
    async with aiohttp.ClientSession() as session:
        for i in range(0, num_of_objs):
            body = measurements[i]
            async with session.post(measurements_res, json=body, headers=headers, ssl=False) as resp:
                if resp.status == 201:
                    created_mes = await resp.json()
    end = timeit.default_timer()
    dt = round((end - start) * 10 ** 6, 3)  # end - start
    print(f"Elapsed time in Non-Bulk REST API with EdgeDB on creation of {num_of_objs} items is {dt} us.")


async def run_bulk_api_with_edgedb(num_of_objs: int):
    measurements_res = "http://127.0.0.1:8059/api/MeasurementSeries"
    measurements = list()
    for i in range(0, num_of_objs):
        measurements.append({
            "sampleDate": str(datetime.datetime.utcnow().strftime("%Y-%m-%dT%H:%M:%S")),
            "value": random.random() * 25,
            "measureUnitId": "604a0412-ea47-11ed-b9a1-bba352c6e579",
            "sensorId": "30ac8366-ea4b-11ed-ab17-e38db76a95f0"
        })

    headers = {
        'Content-type': 'application/json',
        'Accept': 'application/json'
    }

    start = timeit.default_timer()
    async with aiohttp.ClientSession() as session:
        body = measurements
        async with session.post(measurements_res, json=body, headers=headers, ssl=False) as resp:
            if resp.status == 201:
                created_mes = await resp.json()
    end = timeit.default_timer()
    dt = round((end - start) * 10 ** 6, 3)  # end - start
    print(f"Elapsed time in Bulk REST API with EdgeDB on creation of {num_of_objs} items is {dt} us.")


if __name__ == "__main__":
    loop = asyncio.get_event_loop()
    loop.run_until_complete(main())
