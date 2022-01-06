import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from './core/services/authentication.service';
import { User } from './shared/models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'MovieShopSPA';

  isLoggedIn: boolean = false;
  user!: User;

  constructor(private authService: AuthenticationService, private router: Router) {
    
  }

  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    
    this.authService.populateUserInfo();

    this.authService.isLoggedIn.subscribe(resp => this.isLoggedIn = resp);
    this.authService.currentUser.subscribe(resp => this.user = resp);
  }

  logout(){
    this.authService.logout();
    this.router.navigateByUrl('/')
  }
}
