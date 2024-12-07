import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class EnvironmentalService {
  private apiUrl = `${environment.apiUrl}/EnvironmentalData`;

  constructor(private http: HttpClient) {}

  getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('authToken');
    return new HttpHeaders().set('Authorization', `Bearer ${token}`);
  }
  // Add new environmental data
  addEnvironmentalData(environmentalData: any): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.post(`${this.apiUrl}`, environmentalData,{headers});
  }

  // Get environmental data by ID
  getEnvironmentalDataById(id: number): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.get(`${this.apiUrl}/${id}`,{headers});
  }

  // Update existing environmental data
  updateEnvironmentalData(id: number, environmentalData: any): Observable<any> {
    const headers = this.getAuthHeaders();

    return this.http.put(`${this.apiUrl}/${id}`, environmentalData,{headers});
  }

  // Delete environmental data by ID
  deleteEnvironmentalData(id: number): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.delete(`${this.apiUrl}/${id}`,{headers});
  }

  // Get all environmental data records
  getAllEnvironmentalData(): Observable<any[]> {
    const headers = this.getAuthHeaders();
    return this.http.get<any[]>(`${this.apiUrl}`,{headers});
  }
// Fetch the number of assessments conducted per sanctuary
getAssessmentsBySanctuary(): Observable<{ sanctuary: string; count: number }[]> {
  return this.http.get<{ sanctuary: string; count: number }[]>(`${this.apiUrl}/by-sanctuary`);
}

// Fetch the distribution of assessments by impact type
getAssessmentsByImpactType(): Observable<{ impactType: string; count: number }[]> {
  return this.http.get<{ impactType: string; count: number }[]>(`${this.apiUrl}/by-impact-type`);
}
}
