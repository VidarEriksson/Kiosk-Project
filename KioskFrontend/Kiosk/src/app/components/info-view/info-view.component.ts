import { Component, inject } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { InfoService } from '../../services/info.service';
import { InfoValue } from '../../models/info-value.model';
import { Subscription, interval } from 'rxjs';
import { CommonModule, DatePipe } from '@angular/common';

@Component({
  selector: 'app-info-view',
  standalone: true,
  imports: [MatCardModule, CommonModule, DatePipe],
  templateUrl: './info-view.component.html',
  styleUrl: './info-view.component.scss'
})
export class InfoViewComponent {
  private api = inject(InfoService);
  public events?: InfoValue[] = [];
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
      this.events = events;
    });
  }

}
