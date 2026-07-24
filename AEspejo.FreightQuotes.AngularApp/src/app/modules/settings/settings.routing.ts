import { Routes } from "@angular/router";
import { SearchComponent } from "./components/search/search.component";


export const settingsRoutes: Routes = [
  {
    path: 'settings',
    component: SearchComponent,
    loadChildren: () => import('./settings.module').then(m => m.SettingsModule)
  }
];
