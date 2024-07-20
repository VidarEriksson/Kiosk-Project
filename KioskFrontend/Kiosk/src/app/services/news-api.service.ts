import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';
import { NewsValue } from '../models/news-value.model';

@Injectable({
  providedIn: 'root'
})
export class NewsApiService {
  private http = inject(HttpClient);
  private baseUrl: string = environment.newsUrl;

  constructor() {}

  getAllEvents(): Observable<NewsValue[]> {
    // console.log(this.baseUrl);
    const url = this.baseUrl + '/SogetiNews/5';
    return this.http.get<NewsValue[]>(url);
  }
}
