import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Report } from '../../models/report.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  private observationsKey = 'recentObservations'; 
  private actionsReportKey = 'actionsReport'; 
  private apiUrl = `${environment.apiUrl}/Reports`;

  constructor(private http: HttpClient) {}

  getObservations(): any[] {
    const storedData = sessionStorage.getItem(this.observationsKey);
    return storedData ? JSON.parse(storedData) : []; 
  }

  saveObservations(observations: any[]): void {
    sessionStorage.setItem(this.observationsKey, JSON.stringify(observations)); 
  }

  getActionReport(): any {
    const report = sessionStorage.getItem(this.actionsReportKey);
    return report ? JSON.parse(report) : { add: 0, update: 0, delete: 0 };
  }

  updateActionReport(action: string): void {
    const report = this.getActionReport();
    if (report.hasOwnProperty(action)) {
      report[action]++;
      sessionStorage.setItem(this.actionsReportKey, JSON.stringify(report)); 
    }
  }

 
    addObservation(newObservation: any): void {
      let observations = this.getObservations(); 
    
      if (!observations) {
        observations = []; 
      }
    
      observations.push(newObservation); 
      this.saveObservations(observations); 
      this.updateActionReport('add'); 
    
  }

  generateReport(): any {
    const observations = this.getObservations();
    const actions = this.getActionReport();
   
    return {
      observations,
      actions
    };
  }
  clearStorage():any{
    sessionStorage.removeItem(this.observationsKey);
    sessionStorage.removeItem(this.actionsReportKey);

  }

  getReports(): Observable<Report[]> {
    return this.http.get<Report[]>(this.apiUrl);
  }

  getReportById(reportId: number): Observable<Report> {
    return this.http.get<Report>(`${this.apiUrl}/${reportId}`);
  }

  createReport(report: Report): Observable<Report> {
    return this.http.post<Report>(this.apiUrl, report);
  }

  updateReport(reportId: number, report: Report): Observable<Report> {
    return this.http.put<Report>(`${this.apiUrl}/${reportId}`, report);
  }

  deleteReport(reportId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${reportId}`);
  }
}
