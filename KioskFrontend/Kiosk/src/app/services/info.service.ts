import { Injectable, inject } from '@angular/core';
import { InfoApiService } from './info-api.service';
import { InfoValue } from '../models/info-value.model';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InfoService {
  private api = inject(InfoApiService);

  constructor() {}

  async getAllEvents(): Promise<InfoValue[]> {
    return await lastValueFrom(this.api.getAllEvents());
  }
}
