import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  private baseUrl=`${environment.apiUrl}/Projects`;
  constructor(private httpClient:HttpClient) { }
  getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('authToken');
    return new HttpHeaders().set('Authorization', `Bearer ${token}`);
  }

  getProjects(): Observable<any> {
    
    return this.httpClient.get(`${this.baseUrl}`);
  }

  // Get a specific project by ID
  getProjectById(projectId: number): Observable<any> {
    return this.httpClient.get(`${this.baseUrl}/${projectId}`);
  }

  createProject(projectData: any): Observable<any> {
    console.log(projectData);
    const token = localStorage.getItem('authToken');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.httpClient.post(`${this.baseUrl}`, projectData,{headers});
  }

  // Update an existing project
  updateProject(projectId: number, projectData: any): Observable<any> {
    const headers=this.getAuthHeaders();
    return this.httpClient.put(`${this.baseUrl}/${projectId}`,projectData, {headers});
  }

  // Delete a project
  deleteProject(projectId: number): Observable<any> {
    const headers=this.getAuthHeaders();
    return this.httpClient.delete(`${this.baseUrl}/${projectId}`,{headers});
  }

}
