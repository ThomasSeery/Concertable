import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { ListingApplication } from '../../models/listing-application';
import { ValidationResponse } from '../../models/validation-response';

@Injectable({
  providedIn: 'root'
})
export class ListingApplicationService {

  private apiUrl = `${environment.apiUrl}/listingapplication`;
  
  constructor(private http: HttpClient) { }

  applyForListing(id: number) : Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${id}`, {});
  }

  getAllForListingId(id : number) : Observable<ListingApplication[]> {
    return this.http.get<ListingApplication[]>(`${this.apiUrl}/all/${id}`);
  }

  getById(id: number): Observable<ListingApplication> {
    return this.http.get<ListingApplication>(`${this.apiUrl}/${id}`);
  }

  canApplyForListing(listingId: number): Observable<boolean> {
    return this.http.get<boolean>(`${this.apiUrl}/can-apply/${listingId}`);
  }

  canAcceptApplication(listingApplicationId: number): Observable<boolean> {
    return this.http.get<boolean>(`${this.apiUrl}/can-accept/${listingApplicationId}`);
  }
}
