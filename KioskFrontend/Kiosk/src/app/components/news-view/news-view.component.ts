import { CommonModule, DatePipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { NewsValue } from '../../models/news-value.model';
import { Subscription, interval } from 'rxjs';
import { NewsService } from '../../services/news.service';

@Component({
  selector: 'app-news-view',
  standalone: true,
  imports: [MatCardModule, CommonModule, DatePipe],
  templateUrl: './news-view.component.html',
  styleUrl: './news-view.component.scss'
})
export class NewsViewComponent {
  private api = inject(NewsService);
  public events?: NewsValue[] = [];
  private updateSubscription?: Subscription;

  ngOnInit() {
    this.updateSubscription = interval(10000).subscribe(() => {
      this.getEvents();
    });

    this.getEvents();
  }
  
  calculateLocale(date: Date) {
    date = new Date(date);
    var offset = date.getTimezoneOffset() * -1;
    date.setTime(date.getTime() + offset * 60000);
    return date;
  }
  
  getEvents() {
    this.api.getAllEvents().then((events) => {
      this.events = events;//.slice(0, 2);
    });
  }
}
