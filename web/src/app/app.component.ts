import { Component, OnInit } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from '../environments/environment';
import * as Highcharts from 'highcharts';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  private readonly hubConnection: signalR.HubConnection;
  public highcharts = Highcharts;
  public temperaturesData = [];
  public humidityData = [];

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(environment.baseUrl + 'metering', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      })
      .build();
  }

  async ngOnInit(): Promise<void> {
    await this.hubConnection.start();
    console.log('connection started');

    this.hubConnection.on('MeasurementAdded', measurementRaw => {
      console.log(measurementRaw);
      this.temperaturesData.push([measurementRaw.timestamp, measurementRaw.temperature]);
      this.humidityData.push([measurementRaw.timestamp, measurementRaw.humidity]);
    });
  }

  public chartOptions = (): Highcharts.Options => ({
    title: {
      text: 'Pepova meření'
    },
    series: [
      {
        type: 'spline',
        name: 'Temperature',
        data: this.temperaturesData
      }
    ]
  });
}
