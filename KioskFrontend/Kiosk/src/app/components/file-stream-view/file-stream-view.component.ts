import { Component, inject } from '@angular/core';
import { Subscription, interval } from 'rxjs';
import { MatCardModule } from '@angular/material/card';
import { CommonModule, DatePipe } from '@angular/common';
import {FileStreamValue} from '../../models/file-stream-value.model';
import {FileStreamService} from '../../services/file-stream.service';
@Component({
  selector: 'app-file-stream-view',
  standalone: true,
  imports: [MatCardModule, CommonModule, DatePipe],
  templateUrl: './file-stream-view.component.html',
  styleUrl: './file-stream-view.component.scss'
})
export class FileStreamViewComponent {
  private api = inject(FileStreamService);
  public events?: FileStreamValue[] = [];
  private updateSubscription?: Subscription;

  calculateLocale(date: Date) {
    date = new Date(date);
    var offset = date.getTimezoneOffset() * -1;
    date.setTime(date.getTime() + offset * 60000);
    return date;
  }

}
