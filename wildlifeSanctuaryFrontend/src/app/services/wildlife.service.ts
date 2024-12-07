import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { WildlifeData } from '../../models/wildlife.model';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WildlifeService {
  private apiUrl =`${environment.apiUrl}/wildlife`;

  constructor(private httpClient: HttpClient) { }

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('authToken');
    return new HttpHeaders().set('Authorization', `Bearer ${token}`);
  }

  getAllWildlifeData(): Observable<WildlifeData[]> {
    const headers = this.getAuthHeaders(); 
    return this.httpClient.get<WildlifeData[]>(this.apiUrl,{headers});
  }

  // Get Wildlife Data by ID
  getWildlifeDataById(dataId: number): Observable<WildlifeData> {
    return this.httpClient.get<WildlifeData>(`${this.apiUrl}/${dataId}`);
  }

  // Create new Wildlife Data
  createWildlifeData(wildlifeData: WildlifeData): Observable<WildlifeData> {
    const headers = this.getAuthHeaders();  // Add Authorization header
    return this.httpClient.post<WildlifeData>(this.apiUrl, wildlifeData, { headers });
  }

  // Update Wildlife Data
  updateWildlifeData(dataId: number, wildlifeData: WildlifeData): Observable<WildlifeData> {
    const headers = this.getAuthHeaders();  // Add Authorization header
    return this.httpClient.put<WildlifeData>(`${this.apiUrl}/${dataId}`, wildlifeData, { headers });
  }

  // Delete Wildlife Data
  deleteWildlifeData(dataId: number): Observable<void> {
    const headers = this.getAuthHeaders();  // Add Authorization header
    return this.httpClient.delete<void>(`${this.apiUrl}/${dataId}`, { headers });
  }

  // Get Population Trends
  getPopulationTrends(): Observable<any[]> {
    const headers = this.getAuthHeaders();
    return this.httpClient.get<any[]>(`${this.apiUrl}/population-trends`,{headers});
  }

  // Get Top Recent Observations
  getTopRecentObservations(): Observable<any[]> {
    const headers = this.getAuthHeaders(); 
    return this.httpClient.get<any[]>(`${this.apiUrl}/top-recent-observations`,{headers});
  }
}
