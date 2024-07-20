import { Component, inject } from '@angular/core';
import { BarometerService } from '../../services/barometer.service';
import { BarometerValue } from '../../models/barometer-value.model';
import { Subscription, interval } from 'rxjs';
import { MatCardModule } from '@angular/material/card';
import { CommonModule, DatePipe } from '@angular/common';
import { Color, NgxChartsModule, ScaleType } from '@swimlane/ngx-charts';

@Component({
  selector: 'app-barometer-view',
  standalone: true,
  imports: [MatCardModule, CommonModule, DatePipe, NgxChartsModule],
  templateUrl: './barometer-view.component.html',
  styleUrl: './barometer-view.component.scss',
})
export class BarometerViewComponent {
  private api = inject(BarometerService);
  public values?: BarometerValue[] = [];
  private updateSubscription?: Subscription;

  multi: any[] = [];
  view: [number, number] = [1000, 400]; //How to adjust width?

  legend: boolean = true;
  showLabels: boolean = false;
  animations: boolean = true;
  xAxis: boolean = true;
  yAxis: boolean = true;
  showYAxisLabel: boolean = true;
  showXAxisLabel: boolean = true;
  xAxisLabel: string = 'Tidpunkt';
  yAxisLabel: string = 'Värde';

  colorScheme: Color = {
    name: 'myScheme',
    selectable: true,
    group: ScaleType.Ordinal,
    domain: ['#5AA454', '#E44D25', '#CFC0BB'],
  };

  ngOnInit() {
    this.updateSubscription = interval(5000).subscribe(() => {
      this.getValues();
    });

    this.getValues();
  }

  calculateLocale(date: Date) {
    date = new Date(date);
    var offset = date.getTimezoneOffset() * -1;
    date.setTime(date.getTime() + offset * 60000);
    return date;
  }

  getValues() {
    this.api.getAllValues().then((values) => {
      this.values = values;
      let oneDayAgo = new Date();
      oneDayAgo.setDate(oneDayAgo.getDate() - 7);
      oneDayAgo = this.calculateLocale(oneDayAgo);
      let today = this.values.filter((value) => {
        return new Date(value.registered!) >= oneDayAgo;
      });
      this.multi = [
        {
          name: 'Temperatur',
          series: today.map((value) => {
            return {
              name: this.calculateLocale(value.registered!),
              value: value.temperature,
            };
          }),
        },
        {
          name: 'Fuktighet',
          series: today.map((value) => {
            return {
              name: this.calculateLocale(value.registered!),
              value: value.humidity,
            };
          }),
        },
      ];
    });
  }
}
