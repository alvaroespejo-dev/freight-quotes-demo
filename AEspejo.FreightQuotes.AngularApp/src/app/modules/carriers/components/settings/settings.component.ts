import { HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, OnInit, inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { firstValueFrom } from 'rxjs';
import { CarrierSettingService } from '../../carrier-setting.service';
import {
  CarrierSettingResponse,
  CarrierSettingsDialogData,
  SaveCarrierSettingRequest
} from '../../carrier-setting.types';
import { ConfirmDialogComponent } from '../../../global/components/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-carriers-settings',
  standalone: false,
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.scss'
})
export class SettingsComponent implements OnInit {

  settingService = inject(CarrierSettingService);
  toastrService = inject(ToastrService);
  dialog = inject(MatDialog);

  displayedColumns: string[] = ['settingType', 'key', 'value', 'isActive', 'actions'];

  // Local editable copy of the loaded settings
  rows: CarrierSettingResponse[] = [];

  // New-setting form model
  newSetting = { settingTypeId: 0, carrierSettingTypeId: 0, value: '' };

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: CarrierSettingsDialogData,
    public dialogRef: MatDialogRef<SettingsComponent>
  ) { }

  ngOnInit(): void {
    this.reload();
  }

  async reload(): Promise<void> {
    await this.settingService.load(this.data.carrierId);
    this.rows = this.settingService.settings().map(s => ({ ...s }));
  }

  async saveRow(row: CarrierSettingResponse): Promise<void> {
    const request: SaveCarrierSettingRequest = {
      carrierId: this.data.carrierId,
      settingTypeId: row.settingTypeId,
      carrierSettingTypeId: row.carrierSettingTypeId,
      value: row.value ?? '',
      isActive: row.isActive
    };

    try {
      await this.settingService.update(row.id, request);
      this.toastrService.success('Setting updated successfully', 'Success');
    } catch {
      this.toastrService.error('Unable to update setting', 'Error');
    }
  }

  async delete(row: CarrierSettingResponse): Promise<void> {
    const confirmation = await firstValueFrom(this.dialog.open(ConfirmDialogComponent, {
      width: '420px',
      disableClose: true,
      data: {
        title: 'Delete setting',
        message: 'Are you sure you want to delete this setting?',
        confirmText: 'Delete'
      }
    }).afterClosed());

    if (!confirmation) {
      return;
    }

    try {
      await this.settingService.delete(row.id);
      await this.reload();
      this.toastrService.success('Setting deleted successfully', 'Success');
    } catch {
      this.toastrService.error('Unable to delete setting', 'Error');
    }
  }

  async add(): Promise<void> {
    if (!this.newSetting.settingTypeId || !this.newSetting.carrierSettingTypeId) {
      this.toastrService.warning('Select a setting type and a key', 'Validation');
      return;
    }

    const request: SaveCarrierSettingRequest = {
      carrierId: this.data.carrierId,
      settingTypeId: this.newSetting.settingTypeId,
      carrierSettingTypeId: this.newSetting.carrierSettingTypeId,
      value: this.newSetting.value ?? '',
      isActive: true
    };

    try {
      await this.settingService.create(request);
      this.newSetting = { settingTypeId: 0, carrierSettingTypeId: 0, value: '' };
      await this.reload();
      this.toastrService.success('Setting added successfully', 'Success');
    } catch (error) {
      if (error instanceof HttpErrorResponse && error.status === 409) {
        this.toastrService.error(
          error.error?.message ?? 'A setting with this type and key already exists for this carrier.',
          'Duplicate setting');
        return;
      }
      this.toastrService.error('Unable to add setting', 'Error');
    }
  }

  close(): void {
    this.dialogRef.close();
  }
}
