import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Coordinates } from '../../../models/coordinates';

@Component({
  selector: 'app-google-maps',
  standalone: false,
  
  templateUrl: './google-maps.component.html',
  styleUrl: './google-maps.component.scss'
})
export class GoogleMapsComponent {
  @Input() editMode?: boolean = false;
  @Input() lat?: number;
  @Input() lng?: number;
  @Output() coordinatesChange = new EventEmitter<Coordinates>();

  defaultCenter = { lat: 51.5074, lng: -0.1278 };

  get center(): google.maps.LatLngLiteral {
    if (this.lat !== undefined && this.lng !== undefined)
      return { lat: this.lat, lng: this.lng };
    return this.defaultCenter;
  }

  onLocationChange(location: google.maps.LatLngLiteral) {
    this.lat = location.lat;
    this.lng = location.lng;
    this.coordinatesChange.emit({
      latitude: this.lat,
      longitude: this.lng
    });
  }
  
}
