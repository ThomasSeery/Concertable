import { Component } from '@angular/core';
import { ReviewDirective } from '../../directives/review/review.directive';
import { Observable } from 'rxjs';
import { Pagination } from '../../models/pagination';
import { Review } from '../../models/review';
import { ReviewSummary } from '../../models/review-summary';

@Component({
  selector: 'app-event-reviews',
  standalone: false,
  templateUrl: '../../shared/templates/reviews/reviews.template.html',
  styleUrl: '../../shared/templates/reviews/reviews.template.scss'
})
export class EventReviewsComponent extends ReviewDirective {
  override onAddReview(): void {
    throw new Error('Method not implemented.');
  }
  getSummary(id: number): Observable<ReviewSummary> {
    return this.reviewService.getSummaryByEventId(id);
  }

  get(id: number): Observable<Pagination<Review>> {
    return this.reviewService.getByEventId(id, this.pageParams);
  }

  canReview(id: number): Observable<boolean> {
      return this.reviewService.canUserReviewEvent(id);
  }
}
