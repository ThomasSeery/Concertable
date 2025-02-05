import { Component, Input } from '@angular/core';
import { VenueHeader } from '../../models/venue-header';

@Component({
  selector: 'app-venue-headers',
  standalone: false,
  
  templateUrl: './venue-headers.component.html',
  styleUrl: './venue-headers.component.scss'
})
export class VenueHeadersComponent {
  @Input() headers: VenueHeader[] = [];
}
