import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Event } from '../../models/event';
import { EventHeader } from '../../models/event-header';
import { Pagination } from '../../models/pagination';
import { ListingApplicationPurchase } from '../../models/listing-application-purchase';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private apiUrl = `${environment.apiUrl}/event`;
  
  constructor(private http: HttpClient) { }

  getUpComingByVenueId(id: number) : Observable<Event[]> {
    return this.http.get<Event[]>(`${this.apiUrl}/upcoming/venue/${id}`);
  }

  getUpComingByArtistId(id: number) : Observable<Event[]> {
    return this.http.get<Event[]>(`${this.apiUrl}/upcoming/artist/${id}`);
  }

  getDetailsById(id: number): Observable<Event> {
    return this.http.get<Event>(`${this.apiUrl}/${id}`);
  }

  getDetailsByApplicationId(applicationId: number): Observable<Event> {
    return this.http.get<Event>(`${this.apiUrl}/application/${applicationId}`);
  }

  getHeaders(params: HttpParams): Observable<Pagination<EventHeader>> {
    return this.http.get<Pagination<EventHeader>>(`${this.apiUrl}/headers`, { params });
  }

  book(paymentMethodId: string, applicationId: number) : Observable<ListingApplicationPurchase> {
    return this.http.post<ListingApplicationPurchase>(`${this.apiUrl}/book`, { paymentMethodId, applicationId });
  }
}
