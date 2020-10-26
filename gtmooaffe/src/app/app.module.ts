import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { MsalModule, MsalInterceptor } from '@azure/msal-angular';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './components/home/home.component';
import { SecuredComponent } from './components/secured/secured.component';

export const protectedRM: [string, string[]][] =
  [
    ['https://gtmooaf01.azurewebsites.net/api/',
      ['https://gtmooaf01.azurewebsites.net/user_impersonation']]
  ];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SecuredComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    HttpClientModule,
    MsalModule.forRoot({
      auth: {
        authority: 'https://login.microsoftonline.com/rickvdboschoutlook.onmicrosoft.com',
        clientId: '8b09257d-f609-47ad-b8ef-226b87b04ee1'
      }
    }, {
      protectedResourceMap: protectedRM
    })
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: MsalInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
