import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SettingsInitComponent } from './settings-init/settings-init.component';
import { SettingsComponent } from './settings/settings.component';

const routes: Routes = [
  { path: '', redirectTo: 'account-init', pathMatch: 'full' },
  { path: 'settings', component: SettingsComponent },
  { path: 'account-init', component: SettingsInitComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
