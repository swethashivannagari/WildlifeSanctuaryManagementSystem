import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IncidentService } from '../../services/incident.service';  
import { IncidentComponent } from '../incident/incident.component';
import { AnalyticsService } from '../../services/analytics.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-ranger-dashboard',
  standalone: true,
  imports: [IncidentComponent,CommonModule],
  templateUrl: './ranger-dashboard.component.html',
  styleUrls: ['./ranger-dashboard.component.scss']
})
export class RangerDashboardComponent implements OnInit {
  @ViewChild('severityBarChart') canvasRef: ElementRef | undefined;
  constructor(private analyticService:AnalyticsService, private incidentService: IncidentService,private router:Router) { }

 
  ChartEmpty: boolean = false;
emptyMessage: string = "No data available to display.";
  labels: string[] = [];
  data: number[] = [];
  username:string|null="";
  incidentsCount: number = 0;
  sanctuariesCount: number = 0;
  filteredIncidents: any[] = [];
  severity: string = 'high';
  resolutionStatus: string = 'unResolved';


  ngOnInit(): void {
    this.getIncidentData();
    this.username=localStorage.getItem('username');
    this.loadFilteredIncidents();
    this.loadIncidentsCount();
    this.loadSanctuariesCount();
  }
  

  getIncidentData(): void {

    //severity
    this.incidentService.getSeverityCount().subscribe(
      (response) => {
        this.labels = Object.keys(response); 
        this.data = Object.values(response); 
        console.log('Severity Count Data:', { labels: this.labels, data: this.data });
        if (this.labels.length === 0 || this.data.length === 0) {
          this.ChartEmpty = true;
          console.log(this.emptyMessage);
        } else {
          this.ChartEmpty = false;
          const ctx = document.getElementById('severityBarChart') as HTMLCanvasElement;
          this.analyticService.createBarChart(ctx, this.labels, this.data, "Incident Status");
        }
      },
      error=>{
        console.log("error occured",error);
        alert("Something went wrong try again!!");
      }
    );

    //status
    this.incidentService.getTaskStatusCount().subscribe(
      (response)=>{
        this.labels = Object.keys(response); 
        this.data = Object.values(response); 
        console.log('Severity Count Data:', { labels: this.labels, data: this.data });
        const ctx = document.getElementById('statusPieChart') as HTMLCanvasElement;
        this.analyticService.createPieChart(ctx,this.labels,this.data,"Status ");
      },
      (error)=>{
        console.log("error occured",error);
        alert("Something went wrong try again!!");
      }
    )
    
    
    
  }

  
  
 
  loadFilteredIncidents(): void {
    this.incidentService.getFilteredIncidents(this.severity, this.resolutionStatus)
      .subscribe(
        (data) => {
          this.filteredIncidents = data;
        },
        (error) => {
          console.error('Error fetching filtered incidents', error);
        }
      );
  }

  loadIncidentsCount(): void {
    const userId = 1;  // Use the actual user ID
    this.incidentService.getIncidentsCount(userId)
      .subscribe(
        (count) => {
          this.incidentsCount = count;
        },
        (error) => {
          console.error('Error fetching incidents count', error);
        }
      );
  }

  loadSanctuariesCount(): void {
    const userId = 1;  // Use the actual user ID
    this.incidentService.getUniqueSanctuariesCount(userId)
      .subscribe(
        (count) => {
          this.sanctuariesCount = count;
        },
        (error) => {
          console.error('Error fetching sanctuaries count', error);
        }
      );
  }

  
}
