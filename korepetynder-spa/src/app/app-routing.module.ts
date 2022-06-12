import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MsalGuard, MsalRedirectComponent } from '@azure/msal-angular';
import {HomeComponent} from "./home/home.component";
import { RoleGuard } from './shared/guards/role.guard';
import { UserType } from './shared/models/userType';
import { RatingComponent } from "./rating/rating.component";

const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
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
    component: HomeComponent,
    canActivate: [MsalGuard, RoleGuard],
    data: {
      expectedRole: UserType.Student
    }
  },
  {
    path: 'rating',
    component: RatingComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
