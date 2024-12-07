import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from '../app/services/auth.service'; 
@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    const requiredRoles = route.data['roles'] as Array<string>;
    
    if (!this.authService.isLoggedIn()) {
      // If the user is not logged in, redirect to the login page
      this.router.navigate(['/login']);
      return false;
    }

    const userRoles = this.authService.getUserRole(); // Assume this returns a list of roles the user has
    const hasRequiredRole = requiredRoles.some(role => userRoles.includes(role));
   

    if (!hasRequiredRole) {
      // If the user doesn't have required roles, redirect to a forbidden page or similar
      this.router.navigate(['/forbidden']);
      return false;
    }

    return true;  // Allow access
  }
}
