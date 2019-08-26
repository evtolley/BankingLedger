import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AccountService } from './swagger-proxy/services';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { AuthGuard } from './auth-guard.service';
import { AccountModule } from './account/account.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    AccountModule
  ],
  providers: [
    HttpClient,
    AccountService,
    AuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
