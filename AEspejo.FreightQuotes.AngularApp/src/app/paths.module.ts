import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { globalRoutes } from "./modules/global/global.routing";
import { quoteRoutes } from "./modules/quotes/quotes.routing";
import { carriersRoutes } from "./modules/carriers/carriers.routing";
import { settingsRoutes } from "./modules/settings/settings.routing";


@NgModule({
  imports: [RouterModule.forChild([
    ...globalRoutes,
    ...quoteRoutes,
    ...carriersRoutes,
    ...settingsRoutes
  ])],
  exports: [RouterModule]
})

export class PathsModule { }
