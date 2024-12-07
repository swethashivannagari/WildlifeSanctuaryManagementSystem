import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Incident } from '../../models/incident.model';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class IncidentService {
  private apiUrl=`${environment.apiUrl}/Incident`;
  constructor(private httpClient:HttpClient) { }

  toIncidentDto(incident: Incident): any {
    return {
      INCIDENTID: incident.incidentId ?? 0, // Use default value if not set
      SANCTUARYID: incident.sanctuaryId,
      DATE: new Date(incident.date).toISOString(), // Convert date to ISO string
      DESCRIPTION: incident.description,
      SEVERITY: incident.severity,
      RESOLUTIONSTATUS: incident.resolutionStatus,
      REPORTEDBYID: 0, // Set dynamically in the backend
      SANCTUARYNAME: incident.sanctuaryName ?? '',
    };
  }

  getAllIncidents():Observable<any[]>{
    return this.httpClient.get<Incident[]>(this.apiUrl);
  }
  

  //get incidents of ranger
  getUserIncidents():Observable<any[]>{
    const token=localStorage.getItem('authToken');
    const headers=new HttpHeaders().set('Authorization',`Bearer ${token}`);
   
    return this.httpClient.get<Incident[]>(`${this.apiUrl}/User`,{headers});
  }

  //get incident by id
  getIncidentById(id:Number):Observable<Incident>{
    return this.httpClient.get<Incident>(`${this.apiUrl}/${id}`)
  }

  
  //add incident
  addIncident(incident:Incident):Observable<any>{
    const incidentDto = this.toIncidentDto(incident);

    console.log(incident);
    incident.date = new Date(incident.date).toISOString();

    // Replace the raw date with the formatted date
    //formData.date = formattedDate;
    const token=localStorage.getItem('authToken');
    const headers=new HttpHeaders().set('Authorization',`Bearer ${token}`);
    return this.httpClient.post(this.apiUrl,incidentDto,{headers});
  }

  //update incident
  updateIncident(incident:Incident):Observable<any>{
    const token=localStorage.getItem('authToken');
    const headers=new HttpHeaders().set('Authorization',`Bearer ${token}`);
    return this.httpClient.put(`${this.apiUrl}/${incident.incidentId}`,incident,{headers});
  }

   // Delete an incident by ID
   deleteIncident(incidentId: number): Observable<void> {
    const token = localStorage.getItem('authToken');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.httpClient.delete<void>(`${this.apiUrl}/${incidentId}`, { headers });
  }

  getSeverityCount(): Observable<{ [key: string]: number }> {
    const token = localStorage.getItem('authToken');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
  
    return this.httpClient.get<{ [key: string]: number }>(`${this.apiUrl}/SeverityCount`, { headers });
  }
  

  //count based on status
  getTaskStatusCount(): Observable<{ [key: string]: number }> {
    const token = localStorage.getItem('authToken');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.httpClient.get<{ [key: string]: number }>(`${this.apiUrl}/StatusCount`,{headers});
  }

   // Get filtered incidents based on severity and resolution status
   getFilteredIncidents(severity: string, resolutionStatus: string): Observable<any> {
    const url = `${this.apiUrl}/FilterUserIncidents?severity=${severity}&resolutionStatus=${resolutionStatus}`;
    const token = localStorage.getItem('authToken');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
   
    return this.httpClient.get<any>(url,{headers});
  }

  // Get count of incidents for a specific user
  getIncidentsCount(userId: number): Observable<number> {
    const url = `${this.apiUrl}/Incidents/count`;
    const token = localStorage.getItem('authToken');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
   
    return this.httpClient.get<number>(url,{headers});
  }

  // Get count of unique sanctuaries associated with a specific user
  getUniqueSanctuariesCount(userId: number): Observable<number> {
    const url = `${this.apiUrl}/sanctuaries/count`;
    const token = localStorage.getItem('authToken');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
   
    return this.httpClient.get<number>(url,{headers});
  }

  
  


}
