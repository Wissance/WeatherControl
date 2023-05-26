import json
import aiohttp
import asyncio
import random
import requests
import datetime
import time


async def main():
    print("Testing API performance vs Bulk API on WeatherControl application started")
    await run_non_bulk_api(10)
    await run_bulk_api(10)
    print("Testing API performance vs Bulk API on WeatherControl application finished")


async def run_non_bulk_api(num_of_objs: int):
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

    '''
        for i in range(0, num_of_objs):
            resp = requests.post(url=measurements_res, json=measurements[i], headers=headers)
            if resp.status_code == 201:
                print("Measurements created")
    '''
    start = time.time()
    async with aiohttp.ClientSession() as session:
        for i in range(0, num_of_objs):
            body = measurements[i]
            async with session.post(measurements_res, json=body, headers=headers, ssl=False) as resp:
                if resp.status == 201:
                    created_mes = await resp.json()
    end = time.time()
    dt = (end - start)
    print(f"Elapsed time in Non-Bulk API is {dt} secs.")


async def run_bulk_api(num_of_objs: int):
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

    '''
        for i in range(0, num_of_objs):
            resp = requests.post(url=measurements_res, json=measurements[i], headers=headers)
            if resp.status_code == 201:
                print("Measurements created")
    '''
    start = time.time()
    async with aiohttp.ClientSession() as session:
        body = measurements
        async with session.post(measurements_res, json=body, headers=headers, ssl=False) as resp:
            if resp.status == 201:
                created_mes = await resp.json()
    end = time.time()
    dt = (end - start)
    print(f"Elapsed time in Bulk API is {dt} secs.")

if __name__ == "__main__":
    loop = asyncio.get_event_loop()
    loop.run_until_complete(main())
