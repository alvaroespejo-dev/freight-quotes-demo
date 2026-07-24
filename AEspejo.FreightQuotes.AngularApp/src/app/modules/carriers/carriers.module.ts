import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSelectModule } from '@angular/material/select';
import { FormComponent } from '../carriers/components/form/form.component';
import { SearchComponent } from '../carriers/components/search/search.component';
import { SettingsComponent } from '../carriers/components/settings/settings.component';
import { GlobalModule } from '../global/global.module';

@NgModule({
  declarations: [
    FormComponent,
    SearchComponent,
    SettingsComponent
  ],
  imports: [
    CommonModule,
    GlobalModule,
    MatSelectModule
  ]
})
export class CarriersModule { }
