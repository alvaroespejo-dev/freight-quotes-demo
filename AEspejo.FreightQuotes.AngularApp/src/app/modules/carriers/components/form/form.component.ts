import { Component, Inject, inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { CarrierDialogData, SaveCarrierRequest } from '../../carrier.types';
import { CarrierService } from '../../carrier.service';

@Component({
  selector: 'app-carriers-form',
  standalone: false,
  templateUrl: './form.component.html',
  styleUrl: './form.component.scss'
})
export class FormComponent implements OnInit {
  carrierService = inject(CarrierService);
  toastrService = inject(ToastrService);

  title = 'Create Carrier';
 
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: CarrierDialogData,
    public dialogRef: MatDialogRef<FormComponent>
  ) { }

  ngOnInit(): void {
    this.title = this.data.type === 'edit' ? 'Edit Carrier' : 'Create Carrier';
  }

  cancel() {
    this.dialogRef.close();
  }

  async save() {
    const request: SaveCarrierRequest = {
      name: this.data.name,
      scac: this.data.scac,
      isActive: this.data.isActive,
      isMockMode: this.data.isMockMode
    };

    try {
      if (this.data.type === 'edit' && this.data.id) {
        await this.carrierService.update(this.data.id, request);
        this.toastrService.success('Carrier updated successfully', 'Success');
      } else {
        await this.carrierService.create(request);
        this.toastrService.success('Carrier created successfully', 'Success');
      }

      this.dialogRef.close(true);
    } catch {
      this.toastrService.error('Unable to save carrier', 'Error');
    }
  }

}
