import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MedicalRecord } from '../../models/medicalRecord.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MedicalRecordService {

  private apiUrl = `${environment.apiUrl}/MedicalRecord`;
  
  constructor(private httpClient: HttpClient) { }

  // Function to get Authorization header
  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('authToken');
    return new HttpHeaders().set('Authorization', `Bearer ${token}`);
  }

  // Get all medical records
  getMedicalRecords(): Observable<any[]> {
    const headers = this.getAuthHeaders();
    return this.httpClient.get<any[]>(this.apiUrl,{headers});
  }

  // Get medical record by ID
  getMedicalRecordById(id: number): Observable<MedicalRecord> {
    return this.httpClient.get<MedicalRecord>(`${this.apiUrl}/${id}`);
  }

  // Add new medical record
  addMedicalRecord(record: MedicalRecord): Observable<any> {
    const headers = this.getAuthHeaders(); // Add the Authorization header
    return this.httpClient.post(this.apiUrl, record, { headers });
  }

  // Update an existing medical record
  updateMedicalRecord(id: number, record: MedicalRecord): Observable<any> {
    const headers = this.getAuthHeaders(); // Add the Authorization header
    return this.httpClient.put<void>(`${this.apiUrl}/${id}`, record, { headers });
  }

  // Delete a medical record by ID
  deleteMedicalRecord(id: number): Observable<any> {
    const headers = this.getAuthHeaders(); // Add the Authorization header
    return this.httpClient.delete<void>(`${this.apiUrl}/${id}`, { headers });
  }

  // Get Animal Count by Criteria
  getAnimalCountByCriteria(criteria: string): Observable<{ [key: string]: number }> {
    return this.httpClient.get<{ [key: string]: number }>(`http://localhost:5209/api/Animal/count?criteria=${criteria}`);
  }

  // Get Animals by Health Status
  getAnimalsByHealthStatus(healthStatus: string): Observable<string[]> {
    return this.httpClient.get<string[]>(`http://localhost:5209/api/Animal/health/${healthStatus}`);
  }

  // Get Medical Records by Vet (with Authorization)
  getMedicalRecordsByVet(): Observable<any[]> {
    const headers = this.getAuthHeaders(); // Add the Authorization header
    return this.httpClient.get<any[]>(`${this.apiUrl}/vet`, { headers });
  }

  // Get Vet Schedule (with Authorization)
  getVetSchedule(): Observable<any[]> {
    const headers = this.getAuthHeaders(); // Add the Authorization header
    return this.httpClient.get<any[]>(`${this.apiUrl}/schedule`, { headers });
  }

  // Get record count (with Authorization)
  getRecordCount(): Observable<number> {
    const headers = this.getAuthHeaders(); // Add the Authorization header
    return this.httpClient.get<number>(`${this.apiUrl}/records/count`, { headers });
  }
}
