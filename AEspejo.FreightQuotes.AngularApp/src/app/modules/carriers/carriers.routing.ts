import { Routes } from "@angular/router";
import { SearchComponent } from "../carriers/components/search/search.component";


export const carriersRoutes: Routes = [
  {
    path: 'carriers',
    component: SearchComponent,
    loadChildren: () => import('./carriers.module').then(m => m.CarriersModule)
  }
];
