import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Sanctuary } from '../../models/sanctuary.model';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SanctuaryService {
  private apiUrl = `${environment.apiUrl}/Sanctuary`;

  constructor(private httpClient: HttpClient) { }

  getSanctuaries(): Observable<any[]> {
    return this.httpClient.get<any[]>(this.apiUrl);
  }

  getSanctuaryById(id: number): Observable<Sanctuary> {
    return this.httpClient.get<Sanctuary>(`${this.apiUrl}/${id}`);
  }

  addSanctuary(sanctuary: Sanctuary): Observable<any> {
    const token = localStorage.getItem('authToken');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.httpClient.post(this.apiUrl, sanctuary, { headers });
  }

  updateSanctuary(id: number, sanctuary: Sanctuary): Observable<void> {
    const token = localStorage.getItem('authToken');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.httpClient.put<void>(`${this.apiUrl}/${id}`, sanctuary, { headers });
  }

  deleteSanctuary(id: number): Observable<void> {
    const token = localStorage.getItem('authToken');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.httpClient.delete<void>(`${this.apiUrl}/${id}`, { headers });
  }
}
