import { Component, OnInit } from '@angular/core';
import { MovieService } from '../core/services/movie.service';  
import { MovieCard } from '../shared/models/movieCard';
import { MovieDetail } from '../shared/models/movieDetail';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  // data that need to expose to the template/view
  movieCards!: MovieCard[];
  movieDetail!: MovieDetail;

  constructor(private movieService:MovieService) { }

  ngOnInit(): void {

    console.log('inside Home Component Init Method')
    // one of the most important life cycle hooks method in angular
    // we use this method to make any API calls and initiallize 
    this.movieService.getTopGrossingMovies()
    .subscribe( m =>{

      this.movieCards = m;
      console.log('inside the subscribtion');
      console.log(this.movieCards);

    });

    // this.movieService.getMovieDetails(1)
    // .subscribe(d => {
    //   this.movieDetail = d;
    //   console.log(this.movieDetail);
    // });


    // call 

  }
  // get the json data of the top revenue movies and send the data to the view
  // Models
}
