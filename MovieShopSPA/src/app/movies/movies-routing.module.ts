import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CastDetailsComponent } from './cast-details/cast-details.component';
import { MovieDetailsComponent } from './movie-details/movie-details.component';
import { MoviesComponent } from './movies.component';

const routes: Routes = [
  {
    path:'', component: MoviesComponent,
    children:[
      // http://localhost:4200/movies/details/3
      {path:'details/:id', component:MovieDetailsComponent},

      // http://localhost:4200/movies/cast/3
      {path:'cast/:id', component:CastDetailsComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MoviesRoutingModule { }
