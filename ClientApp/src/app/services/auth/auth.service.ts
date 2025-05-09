import { HttpClient, HttpContext, HttpParams } from '@angular/common/http';
import { computed, Injectable, signal } from '@angular/core';
import { LoginCredentials } from '../../models/login-credentials';
import { BehaviorSubject, distinctUntilChanged, map, Observable, tap } from 'rxjs';
import { LoginResponse } from '../../models/login-response';
import { environment } from '../../../environments/environment';
import { User } from '../../models/user';
import { RegisterCredentials } from '../../models/register-credentials';
import { Router } from '@angular/router';
import { Role } from '../../models/role';
import { SignalRService } from '../signalr/signalr.service';
import { ResetPasswordRequest } from '../../models/reset-password-request';
import { ToastService } from '../toast/toast.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = `${environment.apiUrl}/auth`;
  
  private currentUserSubject = new BehaviorSubject<User | undefined>(undefined); 
  currentUser$ = this.currentUserSubject.asObservable(); // Every subscriber will recieve the current user whenever it changes

  isAuthenticated$ = this.currentUserSubject.pipe(
    map(user => user !== undefined),
    distinctUntilChanged()
  );
  
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

  get user() {
    return this.currentUserSubject.value;
  }

  constructor(private http: HttpClient, private router: Router, private signalRService: SignalRService, private toastService: ToastService) { }

  isRole = (role: Role): boolean => {
    const user = this.currentUserSubject.getValue();
    return !!user && user.role === role;
  };
  
  isNotRole = (role: Role): boolean => {
    const user = this.currentUserSubject.getValue();
    return !!user && user.role !== role;
  };

  // Returns true if user is unauthenticated or a customer
  isCustomerOrUnauthenticated = (): boolean => {
    const user = this.currentUserSubject.getValue();
    return !user || user.role === 'Customer';  
  };

  isAuthenticated(): boolean {
    return this.currentUserSubject.getValue() !== undefined;
  }

  login(credentials: LoginCredentials, returnUrl?: string): Observable<User> {
    let params = new HttpParams().append('useCookies', true);

    return this.http.post<User>(`${this.apiUrl}/login`, credentials, { params }).pipe(
      tap((user) => {
        this.currentUserSubject.next(user);
        this.currentUserLoaded = true;
        this.router.navigateByUrl(returnUrl ?? user.baseUrl);
        this.signalRService.createHubConnections();
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
        this.signalRService.stopHubConnections();
      })
    );
  }

  register(credentials: RegisterCredentials) : Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/register`, credentials).pipe(
      tap(() => {
        this.toastService.showInfo("Please check your email for verification", "Account created")
        this.router.navigateByUrl('/login');
      })
    );
  }

  getCurrentUser(context?: HttpContext) : Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/current-user`, { context }).pipe(
      tap(user => {
        this.currentUserSubject.next(user);
      })
    )
  }

  navigateByRole(role: Role) : void {
    if (role === 'VenueManager')
      this.router.navigateByUrl('/venue');
  }

  requestEmailChange(newEmail: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/request-email-change`, { newEmail }).pipe(
      tap(() => this.toastService.showSuccess('Confirmation email sent to your email'))
    );
  }  

  forgotPassword(email: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/forgot-password`, { email }).pipe(
      tap(() => this.toastService.showSuccess('Reset email sent to your email address'))
    );
  }

  resetPassword(request: ResetPasswordRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}/reset-password`, request).pipe(
      tap(() => this.toastService.showSuccess('Password reset successful!'))
    );
  }
  
}
