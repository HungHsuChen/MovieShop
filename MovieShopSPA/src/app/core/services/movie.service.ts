import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MovieCard } from 'src/app/shared/models/movieCard';
import { MovieDetail } from 'src/app/shared/models/movieDetail';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

export class MovieService {

  constructor(private http:HttpClient) { }

  // Home Component will call this method
  // Array of movie card model
  // return observables, RxJS
  getTopGrossingMovies(): Observable<MovieCard[]>{

    // we need to make a call to the API 
    // HttpClient class comes from HttpClientModule in angular

    return this.http.get<MovieCard[]>(`${environment.apiBaseUrl}movies/toprevenue`);

  }

  getMovieDetails(id: number): Observable<MovieDetail>{
    // call the api to get movie details, create the model based on json data and return the model

    return this.http.get<MovieDetail>(`${environment.apiBaseUrl}movies/details/${id}`)
  }
}

// Dependency Injection
// var movies = dbContext.Movies.Where(x => x.revenue>10000).ToList();
// Youtube channels => ABC => posts videos
// get notification when new video is posted =>
// two types, finite observable and infinite observable (stream of data) until cancel
// Http call => 