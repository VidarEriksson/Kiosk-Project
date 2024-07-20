import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';
import { WeatherForecastValue } from '../models/weather-forecast.value';

@Injectable({
  providedIn: 'root',
})
export class SmhiApiService {
  private http = inject(HttpClient);
  private baseUrl: string = environment.weatherUrl;

  constructor() {}

  getAllForecasts(): Observable<WeatherForecastValue[]> {
    // console.log(this.baseUrl);
    const url = this.baseUrl + '/Weather/getforecasts';
    return this.http.get<WeatherForecastValue[]>(url);
  }
}
