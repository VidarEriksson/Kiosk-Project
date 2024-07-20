import { Injectable, inject } from '@angular/core';
import { NewsApiService } from './news-api.service';
import { lastValueFrom } from 'rxjs';
import { NewsValue } from '../models/news-value.model';

@Injectable({
  providedIn: 'root'
})
export class NewsService {
  private api = inject(NewsApiService);

  constructor() {}

  async getAllEvents(): Promise<NewsValue[]> {
    return await lastValueFrom(this.api.getAllEvents());
  }
}
