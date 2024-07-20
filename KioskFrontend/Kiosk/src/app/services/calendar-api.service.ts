import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../environments/environment';
import { CalendarValue } from '../models/calendar-value.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CalendarApiService {
  private http = inject(HttpClient);
  private baseUrl: string = environment.calendarUrl;

  constructor() {}

  getAllEvents(): Observable<CalendarValue[]> {
    // console.log(this.baseUrl);
    const url = this.baseUrl + '/events/upcoming';
    return this.http.get<CalendarValue[]>(url);
  }
}
