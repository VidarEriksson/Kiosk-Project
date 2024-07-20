import { Component, inject } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { CalendarValue } from '../../models/calendar-value.model';
import { CalendarService } from '../../services/calendar.service';
import { Subscription, interval } from 'rxjs';

@Component({
  selector: 'app-calendar-view',
  standalone: true,
  imports: [MatCardModule, CommonModule, DatePipe],
  templateUrl: './calendar-view.component.html',
  styleUrl: './calendar-view.component.scss',
})
export class CalendarViewComponent {
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
    });
  }
}
