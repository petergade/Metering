import time
import requests


def run():
    while True:
        response = requests.post('http://localhost:5000/api/metering',
                                 data={'timestamp': '2022-01-22T21:24:00+00:00', 'temperature': 21.6, 'humidity': 64.2})
        print(response)
        time.sleep(1)


# Press the green button in the gutter to run the script.
if __name__ == '__main__':
    run()