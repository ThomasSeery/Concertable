import { Component, Input } from '@angular/core';
import { NavItem } from '../../models/nav-item';
import { VenueService } from '../../services/venue/venue.service';
import { AuthService } from '../../services/auth/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { EventService } from '../../services/event/event.service';
import { Event } from '../../models/event';
import { EventStateService } from '../../services/event-state/event-state.service';
import { BlobStorageService } from '../../services/blob-storage/blob-storage.service';
import { ExtendedDetailsDirective } from '../../directives/extended-details/extended-details.directive';
import { ToastService } from '../../services/toast/toast.service';

@Component({
  selector: 'app-event-details',
  standalone: false,
  
  templateUrl: './event-details.component.html',
  styleUrl: './event-details.component.scss'
})
export class EventDetailsComponent extends ExtendedDetailsDirective<Event> {
  @Input('event') declare entity?: Event;

  override navItems: NavItem[] = [
    { name: 'Info', fragment: 'info' },
    { name: 'Location', fragment: 'location' },
    { name: 'Artist', fragment: 'artist' },
    { name: 'Videos', fragment: 'videos' },
    { name: 'Reviews', fragment: 'reviews' }
  ];

  constructor(
    private eventService: EventService,
    private eventStateService: EventStateService,
    authService: AuthService,
    blobStorageService: BlobStorageService,
    route: ActivatedRoute,
    router: Router,
    toastService: ToastService) 
  { 
    super(blobStorageService, authService, route, router, toastService)
  }

  get event(): Event | undefined {
    return this.entity;
  }

  set event(value: Event | undefined) {
    this.entity = value;
  }
  

  override ngOnInit(): void {
      super.ngOnInit();
  }

  setDetails(data: any): void {
      this.event = data['event'];
  }

  onBuyClick() {
    this.router.navigate(['event/checkout', this.event?.id])
  }
}
