import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface Event {
  eventType?: string;
  eventDetail?: string;
  eventDate?: Date;
}

export interface DashboardCounts {
  animalCount: number;
  sanctuaryCount: number;
  incidentCount: number;
  userCount: number;

  
}

@Injectable({
  providedIn: 'root'
})
export class AdminDashboardService {
  private baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  // Fetch checkup percentage dictionary
  getCheckupPercent(): Observable<{ [key: string]: number }> {
    return this.http.get<{ [key: string]: number }>(`${this.baseUrl}/Animal/checkup-percent`);
  }

  // Fetch total expenses dictionary
  getTotalExpenses(): Observable<{ [key: string]: number }> {
    return this.http.get<{ [key: string]: number }>(`${this.baseUrl}/Cost/total-expenses`);
  }

  // Fetch upcoming events
  getUpcomingEvents(): Observable<Event[]> {
    return this.http.get<Event[]>(`${this.baseUrl}/Events/upcoming`);
  }

  // Fetch dashboard counts
  getDashboardCounts(): Observable<DashboardCounts> {
    return this.http.get<DashboardCounts>(`${this.baseUrl}/Events/counts`);
  }

  //fetch incidents
  getTotalIncidents(): Observable<{ [key: string]: number }> {
    return this.http.get<{ [key: string]: number }>(`${this.baseUrl}/Incident/totalIncidentsBySanctuary`);
  }
}
