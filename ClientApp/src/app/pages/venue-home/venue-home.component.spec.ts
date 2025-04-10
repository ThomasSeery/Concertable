import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VenueHomeComponent } from './venue-home.component';

describe('VenueHomeComponent', () => {
  let component: VenueHomeComponent;
  let fixture: ComponentFixture<VenueHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [VenueHomeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VenueHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
