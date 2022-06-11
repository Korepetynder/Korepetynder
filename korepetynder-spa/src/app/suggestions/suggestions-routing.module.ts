import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MsalGuard } from '@azure/msal-angular';
import { RoleGuard } from '../shared/guards/role.guard';
import { UserType } from '../shared/models/userType';
import { SuggestionsComponent } from './suggestions.component';

const routes: Routes = [
  {
    path: '',
    component: SuggestionsComponent,
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
export class SuggestionssRoutingModule { }
