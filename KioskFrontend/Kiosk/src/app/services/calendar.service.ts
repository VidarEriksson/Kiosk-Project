import { Injectable, inject } from '@angular/core';
import { CalendarApiService } from './calendar-api.service';
import { CalendarValue } from '../models/calendar-value.model';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CalendarService {
  private api = inject(CalendarApiService);

  constructor() {}

  async getAllEvents(): Promise<CalendarValue[]> {
    return await lastValueFrom(this.api.getAllEvents());
  }
}
