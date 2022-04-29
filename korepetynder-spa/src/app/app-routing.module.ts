import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MsalGuard, MsalRedirectComponent } from '@azure/msal-angular';
import {HomeComponent} from "./home/home.component";
import { FavoritesComponent } from "./favorites/favorites.component";

const routes: Routes = [
  {
    path: '',
    redirectTo: 'favorites',
    pathMatch: 'full'
  },
  {
    path: 'settings',
    loadChildren: () => import('./settings/settings.module').then(m => m.SettingsModule)
  },
  {
    path: 'auth',
    component: MsalRedirectComponent
  },
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'favorites',
    component: FavoritesComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
