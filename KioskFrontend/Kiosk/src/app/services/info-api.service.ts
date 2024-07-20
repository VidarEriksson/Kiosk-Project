import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../environments/environment';
import { InfoValue } from '../models/info-value.model';
import { Observable } from 'rxjs';
import { NewsValue } from '../models/news-value.model';

@Injectable({
  providedIn: 'root'
})
export class InfoApiService {
  private http = inject(HttpClient);
  private baseUrl: string = environment.infoUrl;

  constructor() {}

  getAllEvents(): Observable<InfoValue[]> {
    // console.log(this.baseUrl);
    const url = this.baseUrl + '/content';
    return this.http.get<NewsValue[]>(url);
  }
}
