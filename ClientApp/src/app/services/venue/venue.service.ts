import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Venue } from '../../models/venue';
import { environment } from '../../../environments/environment';
import { VenueHeader } from '../../models/venue-header';
import { Pagination } from '../../models/pagination';

@Injectable({
  providedIn: 'root'
})
export class VenueService {

  constructor(private http: HttpClient) { }

  private apiUrl = `${environment.apiUrl}/venue`

  private currentUserVenueSubject = new BehaviorSubject<Venue | undefined>(undefined);
  currentUserVenue$ = this.currentUserVenueSubject.asObservable();

  getDetailsById(id: number) {
    return this.http.get<Venue>(`${this.apiUrl}/${id}`);
  }

  getHeaders(params: HttpParams) : Observable<Pagination<VenueHeader>> {
    return this.http.get<Pagination<VenueHeader>>(`${this.apiUrl}/headers`, { params }); 
  }

  getDetailsForCurrentUser() : Observable<Venue> {
    return this.http.get<Venue>(`${this.apiUrl}/user`).pipe(
      tap(venue => this.currentUserVenueSubject.next(venue))
    );
  }

  create(venue: Venue) : Observable<Venue> {
    return this.http.post<Venue>(`${this.apiUrl}`, venue);
  }

  update(venue: Venue) : Observable<Venue> {
    return this.http.put<Venue>(`${this.apiUrl}/${venue.id}`, venue)
  }
}
