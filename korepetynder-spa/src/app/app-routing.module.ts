import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MsalGuard, MsalRedirectComponent } from '@azure/msal-angular';
import {HomeComponent} from "./home/home.component";
import { RoleGuard } from './shared/guards/role.guard';
import { UserType } from './shared/models/userType';
import { FavoritesComponent } from "./favorites/favorites.component";
import { RatingComponent } from "./rating/rating.component";
import { OpinionPopupComponent } from "./opinion-popup/opinion-popup.component";

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
    path: 'suggestions',
    loadChildren: () => import('./suggestions/suggestions.module').then(m => m.SuggestionsModule)
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
  },
  {
    path: 'favorites',
    component: FavoritesComponent
  },
  {
    path: 'opinion-popup',
    component: OpinionPopupComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
