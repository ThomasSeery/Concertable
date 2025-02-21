import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { VenueHeader } from '../../models/venue-header';
import { forkJoin, Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ArtistHeader } from '../../models/artist-header';
import { EventHeader } from '../../models/event-header';
import { SearchParams } from '../../models/search-params';
import { VenueService } from '../venue/venue.service';
import { ArtistService } from '../artist/artist.service';
import { EventService } from '../event/event.service';
import { Pagination } from '../../models/pagination';

@Injectable({
  providedIn: 'root'
})
export class HeaderService {

  apiUrl = `${environment.apiUrl}/header`

  constructor(
    private http: HttpClient,
    private venueService: VenueService,
    private artistService: ArtistService,
    private eventService: EventService
  ) { }

  setParams(searchParams: SearchParams): HttpParams {
    let params = new HttpParams();
    console.log("after",searchParams)
    Object.entries(searchParams)
        .filter(([_, value]) => value !== undefined) // ✅ Ignore undefined values
        .forEach(([key, value]) => {
            if (key === "date") {
                params = params.set(key, value.toISOString().slice(0, 16) + "Z"); // ✅ Convert only when needed
            } else if (key === "genreIds") { // ✅ Check if value is an array
                params = params.set(key, value);
            } else {
                params = params.set(key, String(value)); // ✅ Convert everything else to a string
            }
        });
    return params;
}


  getVenueHeaders(searchParams: SearchParams): Observable<Pagination<VenueHeader>> {
    const { date, ...filteredParams } = searchParams; 
    const params = new HttpParams({ fromObject: filteredParams as any })
    return this.venueService.getHeaders(params);
  }

  getArtistHeaders(searchParams: SearchParams): Observable<Pagination<ArtistHeader>> {
    const { date, ...filteredParams } = searchParams; 
    const params = new HttpParams({ fromObject: filteredParams as any })
    return this.artistService.getHeaders(params);
  }

  getEventHeaders(searchParams: SearchParams): Observable<Pagination<EventHeader>> {
    const params = this.setParams(searchParams);
    console.log("xxx",searchParams);
    console.log(params);
    return this.eventService.getHeaders(params);
  }
}
