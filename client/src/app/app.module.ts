import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { ItemModule } from './item/item.module';
import { MaterialModule } from './material/material.module';
import { RecommendationModule } from './recommendation/recommendation.module';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';

import { MainNavComponent } from './main-nav/main-nav.component';
import { LayoutModule } from '@angular/cdk/layout';

import { AppRoutingModule } from './app-routing.module';
import { UserModule } from './user/user.module';
import { httpInterceptorProviders } from './interceptors/';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [	
    AppComponent,
    PageNotFoundComponent,
    MainNavComponent,
   ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialModule,
    ItemModule,
    RecommendationModule,
    LayoutModule,    
    UserModule,
    AppRoutingModule,
  ],
  providers: [httpInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
