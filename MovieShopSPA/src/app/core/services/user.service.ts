import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Favorite } from 'src/app/shared/models/favorite';
import { Purchase } from 'src/app/shared/models/purchase';
import { UserDetail } from 'src/app/shared/models/userDetail';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  // get user details by id
  getUserDetail(id: number): Observable<UserDetail>{

    return this.http.get<UserDetail>(`${environment.apiBaseUrl}Account/${id}`);
  }

  // get movies purchased by user
  getUserPurchases(id: number): Observable<Purchase[]>{

    return this.http.get<Purchase[]>(`${environment.apiBaseUrl}User/${id}/purchases`);
  }

  // get favorites by user 
  getUserFavorties(id: number): Observable<Favorite[]>{

    return this.http.get<Favorite[]>(`${environment.apiBaseUrl}User/${id}/favorite`);
  }
}
