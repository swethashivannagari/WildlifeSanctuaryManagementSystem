import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../../models/user.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl=`${environment.apiUrl}/Users`;
  constructor(private httpClient:HttpClient) { }

  addUser(user:User):Observable<any>{
    const token=localStorage.getItem('authToken');
    const headers=new HttpHeaders().set('Authorization',`Bearer ${token}`);
    return this.httpClient.post(`${this.apiUrl}/register`,user,{headers});
  }

  getUsers(): Observable<any[]> {
    const token=localStorage.getItem('authToken');
    const headers=new HttpHeaders().set('Authorization',`Bearer ${token}`);
    return this.httpClient.get<any[]>(this.apiUrl,{headers});
  }

  updateUser(userId: string, user: any): Observable<any> {
    const token=localStorage.getItem('authToken');
    const headers=new HttpHeaders().set('Authorization',`Bearer ${token}`);
    return this.httpClient.put<any>(`${this.apiUrl}/${userId}`, user,{headers});
  }

  // Delete a user
  deleteUser(userId: string): Observable<any> {
    const token=localStorage.getItem('authToken');
    const headers=new HttpHeaders().set('Authorization',`Bearer ${token}`);
    return this.httpClient.delete<any>(`${this.apiUrl}/${userId}`,{headers});
  }

  getUsersByRole(role: string): Observable<any[]> {
    return this.httpClient.get<any[]>(`${this.apiUrl}/GetUsersByRole?role=${role}`);
  }
}
