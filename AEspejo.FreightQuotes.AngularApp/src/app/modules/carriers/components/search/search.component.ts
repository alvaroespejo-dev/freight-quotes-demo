import { Component, OnInit, inject } from '@angular/core';
import { CarrierService } from '../../carrier.service';
import { MatTableDataSource } from '@angular/material/table';
import { CarrierDialogData, CarrierResponse } from '../../carrier.types';
import { ToastrService } from 'ngx-toastr';
import { MatDialog } from '@angular/material/dialog';
import { firstValueFrom } from 'rxjs';
import { FormComponent } from '../form/form.component';
import { SettingsComponent } from '../settings/settings.component';
import { ConfirmDialogComponent } from '../../../global/components/confirm-dialog/confirm-dialog.component';


@Component({
  selector: 'app-carriers-search',
  standalone: false,
  templateUrl: './search.component.html',
  styleUrl: './search.component.scss'
})
export class SearchComponent implements OnInit {

  displayedColumns: string[] = ['id', 'name', 'scac', 'isActive', 'isMockMode', 'actions'];
  dataSource = new MatTableDataSource<CarrierResponse>;

  totalQuantity = 0;
  pageSize = 10;
  currentPage = 0;
  paginatorOptions: number[] = [5, 10, 25, 100];

  searchText: string = '';

  constructor() { }

  carrierService = inject(CarrierService);
  toastrService = inject(ToastrService);
  dialog = inject(MatDialog);
  
  ngOnInit(): void {
    this.loadCarriers();
  }

  async loadCarriers() {
    await this.carrierService.get();
  }

  async delete(id: number) {
    const confirmation = await firstValueFrom(this.dialog.open(ConfirmDialogComponent, {
      width: '420px',
      disableClose: true,
      data: {
        title: 'Delete carrier',
        message: 'Are you sure you want to delete this carrier?',
        confirmText: 'Delete'
      }
    }).afterClosed());

    if (!confirmation) {
      return;
    }

    await this.carrierService.delete(id);
    await this.loadCarriers();
    this.toastrService.success('Carrier deleted successfully', 'Success');
  }

  create(){
    this.openCarrierDialog({
      type: 'create',
      name: '',
      scac: '',
      isActive: true,
      isMockMode: true
    });
  }

  edit(carrier: CarrierResponse) {
    this.openCarrierDialog({
      id: carrier.id,
      type: 'edit',
      name: carrier.name,
      scac: carrier.scac,
      isActive: carrier.isActive,
      isMockMode: carrier.isMockMode
    });
  }

  settings(carrier: CarrierResponse) {
    this.dialog.open(SettingsComponent, {
      disableClose: true,
      autoFocus: true,
      closeOnNavigation: true,
      position: { top: '30px' },
      width: '900px',
      data: { carrierId: carrier.id, carrierName: carrier.name }
    });
  }

  private openCarrierDialog(data: CarrierDialogData) {
    const dialogRef = this.dialog.open(FormComponent, {
      disableClose: true,
      autoFocus: true,
      closeOnNavigation: true,
      position: { top: '30px' },
      width: '700px',
      data
    });
    dialogRef.afterClosed().subscribe(async result => {
      if (result) {
        await this.loadCarriers();
      }
    });
  }

  isArray(): boolean {
    return Array.isArray(this.carrierService.carriers());
  }

  getColumnDataName(column: string)
  {
      return column.trim().toLowerCase().replace(/\s/g, '');
  }

  toCamelCase(texto: string): string 
  {
    if (!texto || typeof texto !== 'string') return '';
  
    return texto
      .trim()
      .toLowerCase()
      .split(/\s+/)
      .filter(field => field.length > 0)
      .map((field, index) => {
        if (index === 0) return field;
        return field.charAt(0).toUpperCase() + field.slice(1);
      })
      .join('');
  }

  changePageEvent(event: any) {


  }



}
