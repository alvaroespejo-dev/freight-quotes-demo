import { Routes } from "@angular/router";
import { FormComponent } from "./components/form/form.component";


export const quoteRoutes: Routes = [
  {
    path: 'quotes',
    component: FormComponent,
    loadChildren: () => import('./quotes.module').then(m => m.QuotesModule)
  }
];
