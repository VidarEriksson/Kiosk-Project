import { Component } from '@angular/core';
import { ServicesSelectComponent } from '../services-select/services-select.component';
import { ServiceEditComponent } from '../service-edit/service-edit.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [ServicesSelectComponent, ServiceEditComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent {}
