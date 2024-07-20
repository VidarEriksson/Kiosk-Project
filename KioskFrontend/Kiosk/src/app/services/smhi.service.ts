import { inject, Injectable } from '@angular/core';
import { lastValueFrom, Observable } from 'rxjs';
import { SmhiApiService } from './smhi-api.service';
import { WeatherForecastValue } from '../models/weather-forecast.value';

@Injectable({
  providedIn: 'root',
})
export class SmhiService {
  private api = inject(SmhiApiService);

  constructor() {}

  async getAllForecasts(): Promise<WeatherForecastValue[]> {
    return await lastValueFrom(this.api.getAllForecasts());
  }
}
