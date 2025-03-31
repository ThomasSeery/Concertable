import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { HeaderService } from '../../services/header/header.service';
import { Header } from '../../models/header';
import { Pagination } from '../../models/pagination';
import { HeaderType } from '../../models/header-type';
import { SearchParams } from '../../models/search-params';
import { FindDirective } from '../../directives/find/find.directive';
import { SearchParamsSerializerServiceService } from '../../services/search-params-serializer/search-params-serializer-service.service';
import { EventHeader } from '../../models/event-header';
import { EventService } from '../../services/event/event.service';

@Component({
  selector: 'app-find',
  templateUrl: './find.component.html',
  standalone: false,
  styleUrl: './find.component.scss'
})
export class FindComponent extends FindDirective implements OnInit {
triggerNewSubscription() {
  const newEvent: EventHeader = {
    id: 999,
    name: '🔥 Injected Event',
    imageUrl: 'https://via.placeholder.com/200x200?text=🔥+Injected',
    county: 'Test County',
    town: 'Test Town',
    latitude: 51.5,
    longitude: -0.12,
    rating: 5,
    startDate: new Date(),
    endDate: new Date(new Date().getTime() + 2 * 60 * 60 * 1000),
  };

  this.eventService.addFakeEvent(newEvent);
  console.log('✅ Injected event into ReplaySubject');
}
  coordinates?: google.maps.LatLngLiteral;
  paginatedHeaders?: Pagination<Header>;
  headers: Header[] = [];

  constructor(
    searchParamsSerializerService: SearchParamsSerializerServiceService,
    router: Router,
    route: ActivatedRoute,
    private headerService: HeaderService,
    private eventService: EventService
  ) {
    super(searchParamsSerializerService, router, route);
  }

  ngOnInit(): void {
    this.searchParamsSerializerService.defaultHeaderType = this.headerType;
    this.route.queryParams.subscribe((params: Params) => {
      this.searchParams = this.searchParamsSerializerService.deserialize(params);
      console.log("xxx",this.searchParams)
      this.fetchHeaders(params);
    });
  }

  override changeHeaderType(headerType?: HeaderType) {
    super.changeHeaderType(headerType);
    this.headers = [];
  }

  handleSearch() {
    if(this.searchParams?.headerType)
      if (!this.searchParams.latitude || !this.searchParams.longitude) {
        this.searchParams.radiusKm = undefined;
      }

      const params = this.searchParamsSerializerService.serialize(this.searchParams);
      
      this.router.navigate([], {
        relativeTo: this.route,
        queryParams: params,
      });
    }


  fetchHeaders(params: Params) {
    if (Object.keys(params).length > 0) {
      this.headerService.get(params).subscribe((p) => {
        this.headers = p.data;
        this.paginatedHeaders = p;
      });
    } else {
      this.headers = [];
    }
  }

  onPageChange(event: { pageIndex: number; pageSize: number }) {
    this.searchParams.pageNumber = event.pageIndex;
    this.searchParams.pageSize = event.pageSize;
    this.handleSearch();
  }
}
