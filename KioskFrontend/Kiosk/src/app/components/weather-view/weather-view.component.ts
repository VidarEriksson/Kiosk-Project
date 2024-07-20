import { MatCardModule } from '@angular/material/card';
import { Component, inject } from '@angular/core';
import { SmhiService } from '../../services/smhi.service';
import { WeatherForecastValue } from '../../models/weather-forecast.value';
import { Subscription, interval } from 'rxjs';
import { CommonModule, DatePipe } from '@angular/common';

@Component({
  selector: 'app-weather-view',
  standalone: true,
  imports: [MatCardModule, CommonModule, DatePipe],
  templateUrl: './weather-view.component.html',
  styleUrl: './weather-view.component.scss',
})
export class WeatherViewComponent {
  private api = inject(SmhiService);
  public forecast?: WeatherForecastValue;
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
      this.forecast = forecasts[0];
    });
  }
}
