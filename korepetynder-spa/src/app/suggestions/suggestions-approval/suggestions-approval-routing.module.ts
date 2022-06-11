import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MsalGuard } from '@azure/msal-angular';
import { RoleGuard } from '../../shared/guards/role.guard';
import { UserType } from '../../shared/models/userType';
import { SuggestionsApprovalComponent } from './suggestions-approval.component';

const routes: Routes = [
  {
    path: '',
    component: SuggestionsApprovalComponent,
    canActivate: [MsalGuard, RoleGuard],
    data: {
      expectedRole: UserType.Initialized
    }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SuggestionsApprovalRoutingModule { }
