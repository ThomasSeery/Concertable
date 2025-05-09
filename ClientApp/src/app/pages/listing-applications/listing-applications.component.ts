import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ListingApplicationService } from '../../services/listing-application/listing-application.service';
import { ListingApplication } from '../../models/listing-application';
import { Artist } from '../../models/artist';
import { EventService } from '../../services/event/event.service';
import { EventToastService } from '../../services/toast/event/event-toast.service';

@Component({
  selector: 'app-listing-applications',
  standalone: false,
  templateUrl: './listing-applications.component.html',
  styleUrl: './listing-applications.component.scss'
})
export class ListingApplicationsComponent implements OnInit {
  applications: ListingApplication[] = [];
  titleStyle: { [key: string]: string; } = { 'max-width': '1000px' };
  title?: string;

  constructor(
    private route: ActivatedRoute, 
    private router: Router) { }

  get artists(): Artist[] {
    return this.applications.map(app => app.artist);
  }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.applications = data['applications'];
      this.title = `You have ${this.applications.length} application(s)`
    })
  }

  acceptApplication(application: ListingApplication) {
    this.router.navigate(['application/checkout', application?.id]);
  }
}


