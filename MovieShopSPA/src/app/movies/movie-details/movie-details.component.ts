import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MovieService } from 'src/app/core/services/movie.service';
import { MovieDetail } from 'src/app/shared/models/movieDetail';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent implements OnInit {

  id:number = 0;
  movie!: MovieDetail;
  constructor(private route:ActivatedRoute, private movieService: MovieService) { }

  ngOnInit(): void {
    // get the movie id from the current URL and call Movie Service and show the movie details

    this.route.paramMap.subscribe(
      p=> {
        this.id = Number(p.get('id'));
        console.log(p)
        console.log('MovieId:' + this.id);

        this.movieService.getMovieDetails(this.id).subscribe(
          m=> {
            this.movie = m;
            console.log(this.movie);
          }
        );
      }
    )
  }

}
