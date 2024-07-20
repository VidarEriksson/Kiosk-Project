import { Component, inject, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { AdminService } from '../../services/admin.service';
import { ServiceEditComponent } from '../service-edit/service-edit.component';
import { MatDividerModule } from '@angular/material/divider';

@Component({
  selector: 'app-services-select',
  standalone: true,
  imports: [
    MatDividerModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    FormsModule,
    ServiceEditComponent,
  ],
  templateUrl: './services-select.component.html',
  styleUrl: './services-select.component.scss',
})
export class ServicesSelectComponent {
  private api = inject(AdminService);
  public services?: string[] = [];

  @Input()
  get selectedService(): string {
    return this._name;
  }
  set selectedService(name: string) {
    this._name = (name && name.trim()) || '<no name set>';
  }
  private _name = '';

  ngOnInit() {
    this.getServices();
  }

  getServices() {
    this.api.getAllServices().then((services) => {
      console.log(services);
      this.services = services;
    });
  }
}
