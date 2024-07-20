import { Component, inject } from '@angular/core';
import { Subscription, interval } from 'rxjs';
import { MatCardModule } from '@angular/material/card';
import { CommonModule, DatePipe } from '@angular/common';
import { SmhiService } from '../../services/smhi.service';
import { WeatherForecastValue } from '../../models/weather-forecast.value';

@Component({
  selector: 'app-weather-full-view',
  standalone: true,
  imports: [MatCardModule, CommonModule, DatePipe],
  templateUrl: './weather-full-view.component.html',
  styleUrl: './weather-full-view.component.scss'
})
export class WeatherFullViewComponent {
  private api = inject(SmhiService);
  public forecast?: WeatherForecastValue;
  public forecasts?: WeatherForecastValue[];
  public noonValues?: WeatherForecastValue[];
  private updateSubscription?: Subscription;

  ngOnInit() {
    this.updateSubscription = interval(10000).subscribe(() => {
      this.getForecasts();
    });

    this.getForecasts();
  }

  calculateLocale(date: Date) {
    date = new Date(date);
    var offset = date.getTimezoneOffset() * -1;
    date.setTime(date.getTime() + offset * 60000);
    return date;
  }
  getForecasts() {
    this.api.getAllForecasts().then((forecasts) => {
      // console.log(forecasts);
      this.forecasts = forecasts;
      this.forecast = forecasts[0];
      this.noonValues = forecasts!.filter((forecast) => {
        let test = new Date(forecast!.validTime!);
        return test.getHours() == 14;
      });
      });
    console.log(this.noonValues);
  }
}
