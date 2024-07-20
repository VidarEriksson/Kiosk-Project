import { Component, inject } from '@angular/core';
import { Subscription, interval } from 'rxjs';
import { MatCardModule } from '@angular/material/card';
import { CommonModule, DatePipe } from '@angular/common';
import { CalendarValue } from '../../models/calendar-value.model';
import { CalendarService } from '../../services/calendar.service';

@Component({
  selector: 'app-calendar-full-view',
  standalone: true,
  imports: [MatCardModule, CommonModule, DatePipe],
  templateUrl: './calendar-full-view.component.html',
  styleUrl: './calendar-full-view.component.scss'
})
export class CalendarFullViewComponent {
  private api = inject(CalendarService);
  public events?: CalendarValue[] = [];
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
      // console.log(events);
      this.events = events.reverse();
      // this.events=[];
    });
  }
}
