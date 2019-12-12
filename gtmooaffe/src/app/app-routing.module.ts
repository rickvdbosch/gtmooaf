import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MsalGuard } from '@azure/msal-angular';
import { HomeComponent } from './components/home/home.component';
import { SecuredComponent } from './components/secured/secured.component';


const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'secured',
    component: SecuredComponent,
    canActivate: [MsalGuard]
  }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
