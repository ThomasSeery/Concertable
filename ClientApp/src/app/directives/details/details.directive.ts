import { Directive, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { NavItem } from '../../models/nav-item';
import { Venue } from '../../models/venue';
import { Artist } from '../../models/artist';
import { Event } from '../../models/event';
import { BlobStorageService } from '../../services/blob-storage/blob-storage.service';
import { Preference } from '../../models/preference';
import { User } from '../../models/user';
import { Coordinates } from '../../models/coordinates';
import { ToastService } from '../../services/toast/toast.service';

@Directive({
  selector: '[appDetails]',
  standalone: false,
})
export abstract class DetailsDirective<T extends Venue | Artist | Event | User | Preference> implements OnInit {
  @Input() item?: T;
  @Input() editMode?: boolean;
  @Output() itemChange = new EventEmitter<T>()

  constructor(
    protected authService: AuthService,
    protected route: ActivatedRoute,
    protected router: Router,
    protected toastService: ToastService
  ) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.setDetails(data);
    })
  }

  abstract setDetails(data: any): void;

  onChangeDetected() {
    console.log("entity",this.item);
    this.itemChange.emit(this.item);
  }
}
