import { Directive, OnInit } from '@angular/core';
import { Header } from '../models/header';
import { Observable } from 'rxjs';

@Directive()
export abstract class HeaderCarouselDirective<T extends Header> implements OnInit {
  headers: T[] = [];

  abstract getByAmount(amount: number): Observable<T[]>

  loadHeaders(amount: number) {
    return this.getByAmount(amount).subscribe(h => this.headers = h);
  }

  ngOnInit(): void {
    if (!this.headers || this.headers.length === 0)
      this.loadHeaders(15)
  }
}
