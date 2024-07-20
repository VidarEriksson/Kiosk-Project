import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';
import { BarometerValue } from '../models/barometer-value.model'; 
import { CalendarValue as CalendarValue } from '../models/calendar-value.model'; 
import { FileStreamValue } from '../models/file-stream-value.model';
import { InfoValue } from '../models/info-value.model'; 
import { NewsValue } from '../models/news-value.model';
import { WeatherForecastValue } from '../models/weather-forecast.value';

@Injectable({
  providedIn: 'root'
})
export class ApiDataService {
  private http = inject(HttpClient);
  private barometerUrl: string = environment.barometerUrl;
  private calendarUrl: string = environment.calendarUrl;
  private filestreamUrl: string = environment.filestreamUrl;
  private infoUrl: string = environment.infoUrl;
  private newsUrl: string = environment.newsUrl;
  private weatherUrl: string = environment.weatherUrl;

  constructor() { }

  getAllBarometerValues(): Observable<BarometerValue[]> {
    // console.log(this.baseUrl);
    const url = this.barometerUrl + '/barometer';
    return this.http.get<BarometerValue[]>(url);
  }

  getAllCalendarValues(): Observable<CalendarValue[]> {
    // console.log(this.baseUrl);
    const url = this.calendarUrl + '/events/upcoming';
    return this.http.get<CalendarValue[]>(url);
  }

  getAllFileStreamValues(): Observable<FileStreamValue[]> {
    // console.log(this.baseUrl);
    const url = this.filestreamUrl + '/api/Image/ReadAll/v1';
    return this.http.get<FileStreamValue[]>(url);
  }
  getAllInfoValues(): Observable<InfoValue[]> {
    // console.log(this.baseUrl);
    const url = this.infoUrl + '/content';
    return this.http.get<InfoValue[]>(url);
  }

  getAllNewsEvents(): Observable<NewsValue[]> {
    // console.log(this.baseUrl);
    const url = this.newsUrl + '/SogetiNews/8';
    return this.http.get<NewsValue[]>(url);
  }

  getAllForecastValues(): Observable<WeatherForecastValue[]> {
    // console.log(this.baseUrl);
    const url = this.weatherUrl + '/Weather/getforecasts';
    return this.http.get<WeatherForecastValue[]>(url);
  }
}
