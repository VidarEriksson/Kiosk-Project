import { Injectable, inject } from '@angular/core';
import { AdminApiService } from './admin-api.service';
import { lastValueFrom } from 'rxjs';
import { ServiceValues } from '../models/service-values.model';
import { KeyValue } from '@angular/common';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  private api = inject(AdminApiService);

  constructor() {}

  async getAllServices(): Promise<string[]> {
    return await lastValueFrom(this.api.getAllServices());
  }

  async getAllValues(serviceName: string): Promise<ServiceValues> {
    return await lastValueFrom(this.api.getAllValues(serviceName));
  }

  async removeValue(serviceName: string, key: string) {
    return await lastValueFrom(this.api.removeValue(serviceName, key));
  }

  async addValue(serviceName: string, keyValue: KeyValue<string, string>) {
    return await lastValueFrom(this.api.addValue(serviceName, keyValue));
  }
}
