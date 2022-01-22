import datetime
import random
import time
import requests


def run():
    while True:
        timestamp = datetime.datetime.utcnow().strftime('%Y-%m-%dT%H:%M:%SZ')
        print(timestamp)
        temp = round(random.uniform(18, 22), 2)
        humidity = round(random.uniform(40, 80), 2)
        response = requests.post('https://meteringtest.azurewebsites.net/api/metering',
                                 json={'timestamp': timestamp, 'temperature': temp, 'humidity': humidity})
        print(response)
        time.sleep(1)


# Press the green button in the gutter to run the script.
if __name__ == '__main__':
    run()