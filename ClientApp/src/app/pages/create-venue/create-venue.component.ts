import { Component } from '@angular/core';
import { Venue } from '../../models/venue';
import { VenueService } from '../../services/venue/venue.service';
import { VenueToastService } from '../../services/toast/venue/venue-toast.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateItemDirective } from '../../directives/create-item/create-item.directive';
import { Observable } from 'rxjs';
import { ListingService } from '../../services/listing/listing.service';
import { Listing } from '../../models/listing';

@Component({
  selector: 'app-create-venue',
  standalone: false,
  
  templateUrl: './create-venue.component.html',
  styleUrl: './create-venue.component.scss'
})
export class CreateVenueComponent extends CreateItemDirective<Venue> {
  newListings: Listing[] = [];
  image?: File

  constructor(
    private venueService: VenueService, 
    private venueToastService: VenueToastService,
    private listingService: ListingService,
    router: Router,
    route: ActivatedRoute
  ) {
    super(router, route);
  }

  get venue(): Venue | undefined {
        return this.item;
      }
    
  set venue(value: Venue | undefined) {
    this.item = value;
  }

  createDefaultItem(): void {
      this.venue = {
        id: 0, 
        type: 'venue', 
        name: "",
        about: "",
        latitude: 0,
        longitude: 0,
        imageUrl: "", 
        county: "",
        town: "",
        email: '',
        approved: false,
        rating: 0
      }
  }

  create(venue: Venue): Observable<Venue> {
    if (!this.image) 
      throw new Error("Image is required to create a venue");
    return this.venueService.create(venue, this.image);
  }

  showCreated(venue: Venue): void {
    this.venueToastService.showCreated(venue.name); 
  }

  addListing(listing: Listing) {
    this.newListings.push(listing);
  }

  updateImage(image: File) {
    this.image = image;
  }

  override createChanges(): void {
      super.createChanges()
      this.listingService.createMultiple(this.newListings).subscribe(l => this.newListings = []);
  }
  
}
