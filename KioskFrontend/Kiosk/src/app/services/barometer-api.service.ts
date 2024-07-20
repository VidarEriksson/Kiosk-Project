import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../environments/environment';
import { BarometerValue } from '../models/barometer-value.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BarometerApiService {
  private http = inject(HttpClient);
  private baseUrl: string = environment.barometerUrl;

  constructor() {}

  getAllValues(): Observable<BarometerValue[]> {
    // console.log(this.baseUrl);
    const url = this.baseUrl + '/barometer';
    return this.http.get<BarometerValue[]>(url);
  }
}
