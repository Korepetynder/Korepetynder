import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MsalGuard } from '@azure/msal-angular';
import { RoleGuard } from '../shared/guards/role.guard';
import { UserType } from '../shared/models/userType';
import { SettingsComponent } from './settings/settings.component';

const routes: Routes = [
  {
    path: '',
    component: SettingsComponent,
    canActivate: [MsalGuard, RoleGuard],
    data: {
      expectedRole: UserType.Initialized
    }
  },
  {
    path: 'init',
    component: SettingsComponent,
    canActivate: [MsalGuard, RoleGuard],
    data: {
      expectedRole: UserType.Uninitialized
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SettingsRoutingModule { }
