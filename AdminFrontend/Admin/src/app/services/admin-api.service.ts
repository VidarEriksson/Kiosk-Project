import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';
import { ServiceValues } from '../models/service-values.model';
import { KeyValue } from '@angular/common';

@Injectable({
  providedIn: 'root',
})
export class AdminApiService {
  private http = inject(HttpClient);
  private baseUrl: string = environment.adminUrl;

  constructor() {}

  getAllServices(): Observable<string[]> {
    const url = this.baseUrl + '/getservices';
    return this.http.get<string[]>(url);
  }

  getAllValues(serviceName: string): Observable<ServiceValues> {
    const url = this.baseUrl + '/getconfiguration/' + serviceName;
    return this.http.get<ServiceValues>(url);
  }

  addValue(
    serviceName: string,
    keyValue: KeyValue<string, string>
  ): Observable<unknown> {
    const url = this.baseUrl + '/getconfiguration/' + serviceName;
    return this.http.get<ServiceValues>(url);
  }

  removeValue(serviceName: string, key: string): Observable<unknown> {
    const url = this.baseUrl + '/getconfiguration/' + serviceName;
    return this.http.get<ServiceValues>(url);
  }
}
