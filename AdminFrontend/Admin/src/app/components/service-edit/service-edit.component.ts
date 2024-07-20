import { KeyValue, NgIf } from '@angular/common';
import { Component, inject, Input } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { AdminService } from '../../services/admin.service';
import { ServiceValues } from '../../models/service-values.model';
import { MatIconModule } from '@angular/material/icon';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatDividerModule } from '@angular/material/divider';
import { MatInputModule } from '@angular/material/input';
import { MatButton } from '@angular/material/button';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-service-edit',
  standalone: true,
  imports: [
    FormsModule,
    MatTableModule,
    MatIconModule,
    MatFormField,
    MatDividerModule,
    MatLabel,
    MatInputModule,
    MatButton,
    NgIf,
  ],
  templateUrl: './service-edit.component.html',
  styleUrl: './service-edit.component.scss',
})
export class ServiceEditComponent {
  private api = inject(AdminService);
  public services?: ServiceValues;

  keyEdit: string = '';
  valueEdit: string = '';
  activeForm: string = '';

  columnsToDisplay = ['key', 'value', 'actions'];
  dataSource!: KeyValue<string, string>[];

  @Input()
  get selectedService(): string {
    return this._name;
  }
  set selectedService(name: string) {
    this._name = (name && name.trim()) || '<no name set>';
    this.getAllValues(name);
  }
  private _name = '';

  ngOnInit() {}

  getAllValues(serviceName: string) {
    if (serviceName === '<no name set>' || serviceName === '') {
    } else {
      this.api.getAllValues(serviceName).then((services) => {
        this.services = services;
        this.dataSource = services.configuration || [];
      });
    }
  }

  async removeValue() {
    this.activeForm = 'delete';
  }

  async editValue() {
    this.activeForm = 'edit';
  }

  async addValue(): Promise<void> {
    this.keyEdit = '';
    this.valueEdit = '';
    this.activeForm = 'add';
  }

  async saveEditedValue() {
    // const res = await this.api.removeValue(this.selectedService, key);
    // await this.getAllValues(this.selectedService);
    console.log('editValue', this.keyEdit, this.valueEdit);
    this.toggleForm('');
  }
  async saveAddedValue() {
    // const res = await this.api.addValue(this.selectedService, result);
    // await this.getAllValues(this.selectedService);
    console.log('addValue', this.keyEdit, this.valueEdit);
    this.toggleForm('');
  }
  async saveDeletedValue() {
    // const res = await this.api.removeValue(this.selectedService, key);
    // await this.getAllValues(this.selectedService);
    console.log('removeValue', this.keyEdit, this.valueEdit);
    this.toggleForm('');
  }

  toggleForm(form: string) {
    this.activeForm = form;
  }
}
