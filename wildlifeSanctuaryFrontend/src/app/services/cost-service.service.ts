import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CostService {
  private apiUrl=`${environment.apiUrl}/Cost`;
  constructor(private http:HttpClient) { }

  getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('authToken');
    return new HttpHeaders().set('Authorization', `Bearer ${token}`);
  }

  addCost(costData: any): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.post(`${this.apiUrl}`, costData,{headers});
  }

  // Get cost by ID
  getCostById(costId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${costId}`);
  }

  // Update an existing cost record
  updateCost(costData: any): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.put(`${this.apiUrl}/${costData.costId}`, costData,{headers});
  }

  // Delete a cost record by ID
  deleteCost(costId: number): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.delete(`${this.apiUrl}/${costId}`,{headers});
  }

  // Get all cost records
  getAllCosts(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}`);
  }

  //get expense by sanctuary
  getExpensesBySanctuary(sanctuary:string):Observable<any[]>{
    return this.http.get<any[]>(`${this.apiUrl}/${sanctuary}/expenses`);
  }
}
