import { Component, Input, Output } from '@angular/core';
import { Event } from '../../models/event';
import { DetailsHeroDirective } from '../../directives/details-hero.directive';

@Component({
  selector: 'app-event-details-hero',
  standalone: false,
  templateUrl: '../../shared/components/details-hero/details-hero.component.html',
  styleUrl: '../../shared/components/details-hero/details-hero.component.scss'
})
export class EventDetailsHeroComponent extends DetailsHeroDirective<Event> {
  @Input() override locationEditable?: boolean = false;

  get event() : Event | undefined {
    return this.item;
  }

  @Input() set event(event: Event) {
      this.item = event;
  }

  @Output() get eventChange() {
    return this.itemChange;
  }

  get county(): string | undefined {
    return this.event?.venue.county;
  }

  set county(value: string) {
    if(this.event?.venue)
      this.event.venue.county = value;
  }

  get town(): string | undefined {
    return this.event?.venue.town;
  }

  set town(value: string) {
    if(this.event?.venue)
      this.event.venue.town = value;
  }

  get email(): string | undefined {
    return this.event?.venue.email;
  }


  get imageUrl(): string | undefined {
    return this.event?.artist.imageUrl;
  }

  get latitude() : number | undefined {
    return this.event?.venue.latitude
  }

  set latitude(latitude: number) {
    if(this.event?.venue)
      this.event.venue.latitude = latitude;
  }

  get longitude() : number | undefined {
    return this.event?.venue.longitude
  }

  set longitude(longitude: number) {
    if(this.event?.venue)
      this.event.venue.longitude = longitude;
  }

  set imageUrl(value: string) {
    if(this.event?.artist)
      this.event.artist.imageUrl = value;
  }
}
