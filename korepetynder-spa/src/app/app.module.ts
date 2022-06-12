import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
  MsalBroadcastService,
  MsalGuard,
  MsalInterceptor,
  MsalModule,
  MsalRedirectComponent,
  MsalService,
  MSAL_GUARD_CONFIG,
  MSAL_INSTANCE,
  MSAL_INTERCEPTOR_CONFIG
} from '@azure/msal-angular';
import { msalGuardConfigFactory, msalInstanceFactory, msalInterceptorConfigFactory } from './auth-config';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { LayoutModule } from '@angular/cdk/layout';
import { HomeComponent } from './home/home.component';
import { MatCardModule } from "@angular/material/card";
import { MatFormFieldModule } from "@angular/material/form-field";
import { TutorCardComponent } from './home/tutor-card/tutor-card.component';
import { MatExpansionModule } from "@angular/material/expansion";
import { GalleryModule } from "ng-gallery";
import { RatingComponent } from './rating/rating.component';
import { MatInputModule } from "@angular/material/input";
import { NgbRatingModule } from "@ng-bootstrap/ng-bootstrap";
import { RatingCardComponent } from './rating/rating-card/rating-card.component';
import { StarRatingComponent } from './rating/star-rating/star-rating.component';
import { FavoritesComponent } from './favorites/favorites.component';
import { FavoriteTutorCardComponent } from './favorites/favorite-tutor-card/favorite-tutor-card.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    TutorCardComponent,
    FavoritesComponent,
    FavoriteTutorCardComponent
    RatingComponent,
    RatingCardComponent,
    StarRatingComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatSidenavModule,
    MatListModule,
    MatMenuModule,
    MsalModule,
    LayoutModule,
    MatCardModule,
    MatFormFieldModule,
    MatExpansionModule,
    GalleryModule,
    MatInputModule,
    NgbRatingModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true
    },
    {
      provide: MSAL_INSTANCE,
      useFactory: msalInstanceFactory
    },
    {
      provide: MSAL_GUARD_CONFIG,
      useFactory: msalGuardConfigFactory
    },
    {
      provide: MSAL_INTERCEPTOR_CONFIG,
      useFactory: msalInterceptorConfigFactory
    },
    MsalService,
    MsalGuard,
    MsalBroadcastService
  ],
  bootstrap: [AppComponent, MsalRedirectComponent]
})
export class AppModule { }
