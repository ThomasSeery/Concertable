import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SearchParams } from '../../models/search-params';
import { HeaderType } from '../../models/header-type';

@Component({
  selector: 'app-search',
  standalone: false,
  
  templateUrl: './search.component.html',
  styleUrl: './search.component.scss'
})
export class SearchComponent {
  @Input() searchParams!: SearchParams;

  @Input() headerType?: HeaderType;
  @Output() search : EventEmitter<void>  = new EventEmitter<void>();
  @Output() searchParamsChange = new EventEmitter<SearchParams>();

  onDateChange(date: Date) {
    this.searchParams.date = date;
  }

  onLocationChange({ lat, lng }: google.maps.LatLngLiteral) {
    this.searchParams.latitude = lat;
    this.searchParams.longitude = lng;
  }
  

  onSearch() {
    if(this.headerType) {
      this.searchParamsChange.emit(this.searchParams);
      this.search.emit();
    }
  }
}
