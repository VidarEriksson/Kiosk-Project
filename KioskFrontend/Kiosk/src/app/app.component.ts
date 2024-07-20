import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CalendarViewComponent } from './components/calendar-view/calendar-view.component';
import { WeatherViewComponent } from './components/weather-view/weather-view.component';
import { BarometerViewComponent } from "./components/barometer-view/barometer-view.component";
import { NewsViewComponent } from './components/news-view/news-view.component';
import { NewsFullViewComponent } from './components/news-full-view/news-full-view.component';
import { InfoViewComponent } from './components/info-view/info-view.component';
import { CalendarFullViewComponent } from './components/calendar-full-view/calendar-full-view.component';
import { FileStreamViewComponent } from './components/file-stream-view/file-stream-view.component';
import { WeatherFullViewComponent } from './components/weather-full-view/weather-full-view.component';
import { NgComponentOutlet } from '@angular/common';
import { interval, Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  imports: [
    BarometerViewComponent,
    CalendarFullViewComponent,
    CalendarViewComponent,
    FileStreamViewComponent,
    InfoViewComponent,
    NewsViewComponent,
    NewsFullViewComponent,
    WeatherFullViewComponent,
    WeatherViewComponent,
    NgComponentOutlet,
    RouterOutlet,
  ]
})
export class AppComponent {
  title = 'Kiosk';
  index=0;
  availableCards = [
    BarometerViewComponent,
    CalendarFullViewComponent,
    FileStreamViewComponent,
    InfoViewComponent,
    NewsFullViewComponent,
    WeatherFullViewComponent
  ];
  private updateSubscription?: Subscription;

  constructor() {
  }
  ngOnInit() {
    this.updateSubscription = interval(60000).subscribe(() => {
      console.log("Updating component");
      this.index++;
      if(this.index >= this.availableCards.length){
        this.index = 0;
      }
    });
  }

  getComponent(){
    return this.availableCards[this.index];
  }

}

