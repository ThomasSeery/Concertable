import { HttpClient, HttpParams } from '@angular/common/http';
import { computed, Injectable, signal } from '@angular/core';
import { LoginCredentials } from '../../models/login-credentials';
import { BehaviorSubject, map, Observable, tap } from 'rxjs';
import { LoginResponse } from '../../models/login-response';
import { environment } from '../../../environments/environment';
import { User } from '../../models/user';
import { RegisterCredentials } from '../../models/register-credentials';
import { Router } from '@angular/router';
import { Role } from '../../models/role';
import { PaymentHubService } from '../payment-hub/payment-hub.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = `${environment.apiUrl}/auth`;
  
  private currentUserSubject = new BehaviorSubject<User | undefined>(undefined); 
  currentUser$ = this.currentUserSubject.asObservable(); // Every subscription will recieve the current user whenever it changes

  /* 
  * Observable responses cant distinguish the undefined between user not being authenticated
  * and data from the server not being retrievex yet, so use this boolean to distinguish
  */
  private _currentUserLoaded = false;

  get currentUserLoaded(): boolean {
    return this._currentUserLoaded;
  }

  private set currentUserLoaded(value: boolean) {
    this._currentUserLoaded = value;
  }

  constructor(private http: HttpClient, private router: Router, private paymentHubService: PaymentHubService) { }

  isRole = (role: Role) => this.currentUserSubject.getValue()?.role === role;
  isNotRole = (role: Role) => this.currentUserSubject.getValue()?.role !== role;

  isAuthenticated(): boolean {
    return this.currentUserSubject.getValue() !== null;
  }

  login(credentials: LoginCredentials): Observable<any> {
    let params = new HttpParams().append('useCookies', true);
    return this.http.post<any>(`${environment.apiUrl}/login`, credentials, { params }).pipe(
      tap(() => {
        this.getCurrentUser().subscribe();
        this.router.navigateByUrl('/')
        this.paymentHubService.createHubConnection();
      })
    );
  }

  logout() : Observable<void> {
    console.log(`${this.apiUrl}/logout`);
    return this.http.post<void>(`${this.apiUrl}/logout`, {}).pipe(
      tap(() => {
        this.currentUserSubject.next(undefined);
        this.currentUserLoaded = false;
        this.router.navigateByUrl('/');
        this.paymentHubService.stopHubConnection();
      })
    );
  }

  register(credentials: RegisterCredentials) : Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/register`, credentials).pipe(
      tap(() => {
        this.getCurrentUser().subscribe();
        this.router.navigateByUrl('/login');
      })
    );
  }

  getCurrentUser() : Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/current-user`).pipe(
      tap(user => {
        this.currentUserSubject.next(user);
        this.currentUserLoaded = true;
      })
    )
  }

  navigateByRole(role: Role) : void {
    if (role === 'VenueManager')
      this.router.navigateByUrl('/venue');
  }
}
