import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl=`${environment.apiUrl}/Users/login`;
  constructor(private http:HttpClient,private router:Router) { }

  login(email:string,password:string):Observable<any>{
    const body={Email:email,Password:password};
    
    return this.http.post<any>(this.apiUrl,body);
  }

  storeToken(token:string ):void{
    console.log(token);
    localStorage.setItem('authToken',token);
    if(token){
      const role=this.getRoleFromToken(token);
      localStorage.setItem("userRole",role);
      const username = this.getUsernameFromToken(token);
      localStorage.setItem("username",username);
      const userId=this.getUserIdFromToken(token);
      localStorage.setItem("userId",userId);
    }
  }

  getRoleFromToken(token:string):string{
    try{
      const decodedToken:any=jwtDecode(token);
      
      for(let key in decodedToken){
        if(key.includes('role')){
          return decodedToken[key];
        }
      }
      return "";
    }
    catch (error) {
      console.error('Error decoding token:', error);
      return "";
    }
  }

  private getUsernameFromToken(token: string): string {
    try {
     
      const decodedToken: any = jwtDecode(token);
      console.log(decodedToken);
      return decodedToken.sub || ''; // Adjust if the claim is named differently
    } catch (error) {
      console.error('Error decoding token:', error);
      return '';
    }
  }

  private getUserIdFromToken(token: string): string {
    try {
     
      const decodedToken: any = jwtDecode(token);
      console.log(decodedToken);
      return decodedToken.userId || ''; // Adjust if the claim is named differently
    } catch (error) {
      console.error('Error decoding token:', error);
      return '';
    }
  }

  isLoggedIn(): boolean {
    const token = localStorage.getItem("authToken");
    return !!token;
  }
  logout(): void {
    localStorage.removeItem("authToken");
    localStorage.removeItem("userRole");
    localStorage.removeItem("username");
    console.log('User logged out.');
    this.router.navigate(['/login'])
  }
  private isTokenExpired(token: string): boolean {
    try {
      const decodedToken: any = jwtDecode(token);
      const currentTime = Math.floor(Date.now() / 1000); // Current time in seconds
      return decodedToken.exp < currentTime;
    } catch (error) {
      console.error('Error checking token expiration:', error);
      return true;
    }
  }
  getUserRole(): string {
    return localStorage.getItem("userRole") || '';
  }

  getUsername(): string {
    return localStorage.getItem("username") || '';
  }
}

