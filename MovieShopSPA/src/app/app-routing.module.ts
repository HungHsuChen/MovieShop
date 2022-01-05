import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';
import { AuthGuard } from './core/guards/auth.guard';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {path:"", component: HomeComponent},
  {path:"account/login", component: LoginComponent},
  {path:"account/register", component: RegisterComponent},

  // lazy load the movies module only when user goes to localhost:4200/movies path
  {path:'movies',loadChildren:() => import("./movies/movies.module").then(mod=>mod.MoviesModule)},
  {path: 'user', loadChildren:() => import("./user/user.module").then(mod=>mod.UserModule), canLoad:[AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
