import { AfterViewInit, Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { ListingService } from '../../services/listing/listing.service';
import { Venue } from '../../models/venue';
import { Observable, of } from 'rxjs';
import { Listing } from '../../models/listing';
import { AuthService } from '../../services/auth/auth.service';
import { ListingApplicationService } from '../../services/listing-application/listing-application.service';
import { Genre } from '../../models/genre';
import { GenreService } from '../../services/genre/genre.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ListingToastServiceService } from '../../services/toast/listing/listing-toast-service.service';
import { VenueService } from '../../services/venue/venue.service';

@Component({
  selector: 'app-listings',
  standalone: false,
  templateUrl: './listings.component.html',
  styleUrl: './listings.component.scss'
})
export class ListingsComponent  implements OnInit, OnChanges {

  @Input() venueId?: number
  @Input() editMode?: boolean
  @Output() listingCreate = new EventEmitter<Listing>

  listings : Listing[] = [];
  newListing: Listing = {
    startDate: new Date(),
    endDate: new Date(),
    pay: 0,
    genres: []
  };
  genres: Genre[] = [];
  newGenre: Genre = {
    id: 0,
    name: ''
  };
  addNew: boolean = false;
  isOwner$?: Observable<boolean>


  constructor(
    private listingService: ListingService, 
    private applicationService: ListingApplicationService, 
    private venueService: VenueService,
    private genreService: GenreService,
    protected authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private listingToastService: ListingToastServiceService) { }

  ngOnInit(): void {
    this.getListings();
    this.getGenres();
    if(this.venueId)
      this.isOwner$ = this.venueService.isOwner(this.venueId);

  }

  ngOnChanges(changes: SimpleChanges): void {
    if(changes['venue'])
      this.getListings();
  }

  getGenres() {
    console.log("?")
    this.genreService.getAll().subscribe(g => this.genres = g);
    console.log(this.genres);
  }

  getListings() {
    console.log("getActiveListings");
    if(this.venueId)
      this.listingService.getActiveByVenueId(this.venueId).subscribe((listings) => {this.listings = listings; console.log(this.listings)})
  }

  onAdd() {
    this.addNew = true;
    this.createDummyListing();
  }

  onAddGenre() {
    if (this.newGenre.name) {
      if (!this.newListing.genres.includes(this.newGenre)) {
        this.newListing.genres.push(this.newGenre);
        this.newGenre = { id: 0, name: '' };
      }
    }
  }

  onStartDateChange(date: Date) {
    this.newListing.startDate.setFullYear(date.getFullYear());
    this.newListing.startDate.setMonth(date.getMonth());
    this.newListing.startDate.setDate(date.getDate());
  }

  onStartTimeChange(time: Date) {
    this.newListing.startDate.setHours(time.getHours());
    this.newListing.startDate.setMinutes(time.getMinutes());
    this.newListing.startDate.setSeconds(0);
    this.newListing.startDate.setMilliseconds(0);
    console.log(this.newListing)
  }

  onEndTimeChange(time: Date) {
    this.newListing.endDate.setHours(time.getHours());
    this.newListing.endDate.setMinutes(time.getMinutes());
    this.newListing.endDate.setSeconds(0);
    this.newListing.endDate.setMilliseconds(0);
  
    console.log(this.newListing);
  }
  
  isEndTimeBeforeStartTime(start: Date, end: Date): boolean {
    const startHours = start.getHours();
    const startMinutes = start.getMinutes();
    const endHours = end.getHours();
    const endMinutes = end.getMinutes();
  
    return (
      endHours < startHours ||
      (endHours === startHours && endMinutes < startMinutes)
    );
  }
  
  

  onSaveItem() {
    // If end time is earlier than start time, assume it's the next day
    if (this.isEndTimeBeforeStartTime(this.newListing.startDate, this.newListing.endDate)) 
      this.newListing.endDate.setDate(this.newListing.startDate.getDate() + 1);
    else 
      this.newListing.endDate.setDate(this.newListing.startDate.getDate());
    
    console.log(this.newListing)
    this.listings?.push(this.newListing);
    this.addNew = false;
    this.listingCreate.emit(this.newListing);
    this.createDummyListing();
  }

  createListing(listing: Listing) {
    this.listingService.create(listing).subscribe();
  }

  onDelete(listing: Listing) {

  }

  onApply(listing: Listing) {
    if(listing.id)
      this.applicationService.applyForListing(listing.id).subscribe(() => {
        this.listingToastService.showApplied('');
    });
  }

  onViewApplications(listing: Listing) {
    console.log(listing);
    this.router.navigate(['venue/my/applications', listing.id]);
  }
  

  //?
  private createDummyListing() {
    this.newListing = {
      startDate: new Date(),
      endDate: new Date(),
      pay: 0,
      genres: []
    }
  }
  
}
