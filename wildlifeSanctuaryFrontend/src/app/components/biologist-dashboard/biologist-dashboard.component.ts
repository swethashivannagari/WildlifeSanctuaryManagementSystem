import { Component } from '@angular/core';
import { WildlifeService } from '../../services/wildlife.service';
import { CommonModule, DatePipe } from '@angular/common';
import { WildlifeData } from '../../../models/wildlife.model';
import { Router } from '@angular/router';
import { AnalyticsService } from '../../services/analytics.service';
import { MedicalRecordService } from '../../services/medical-record.service';
import { error } from 'console';

@Component({
  selector: 'app-biologist-dashboard',
  standalone: true,
  imports: [CommonModule,DatePipe],
  templateUrl: './biologist-dashboard.component.html',
  styleUrl: './biologist-dashboard.component.scss'
})
export class BiologistDashboardComponent {
  labels: string[] = [];
  data: number[] = [];
  observationCount=120;
  recentRecords:WildlifeData[]= [];
  ChartEmpty: boolean = false;
  criticalAlerts:string[]=[];
  recentObservations:any=[];
  emptyMessage: string = "No data available to display."; 
  userRole :string|null="";
  showDashboard:boolean=true;

  alerts= [
    { species: 'Elephant', status: 'Critical' },
    { species: 'Tiger', status: 'Sick' },
  ];
  constructor(private wildlifeService: WildlifeService,
    private router:Router,private analyticsService:AnalyticsService,
    private medicalRecordService:MedicalRecordService) {}

  ngOnInit() {
    this.fetchDashboardData();
    this. loadPopulationTrends();
    this.loadAlerts()
    this.fetchRecentReports();
    this.checkAccess();
  }

  fetchRecentReports(){
    this.wildlifeService.getTopRecentObservations().subscribe(
      (data)=>{
        this.recentObservations=data;
        console.log("recent:",data);
      },
      (error)=>{
        
        console.error('Error fetching records', error);
      }
    )
  }

  fetchDashboardData(): void {
    this.wildlifeService.getAllWildlifeData().subscribe(
      (data) => {
        this.recentRecords = data; 
        console.log("records",this.recentRecords);
      },
      (error) => {
        console.error('Error fetching records', error);
      }
    );
  }

  
    loadAlerts():void{
      this.medicalRecordService.getAnimalsByHealthStatus('Critical').subscribe(
        (animals) => {
          this.criticalAlerts = animals; 
        },
        (error) => {
          console.error('Error fetching critical alerts', error);
        }
        
      );
      if(!this.criticalAlerts.length){
        console.log("alerts",this.alerts);
      }
  }

  loadPopulationTrends():void{
    this.wildlifeService.getPopulationTrends().subscribe(
      (response) => {
      this.labels = response.map((item: any) => item.species);
      console.log("comlabels",this.labels)
      this.data = response.map((item: any) => item.populationEstimate);
      console.log('comPopulation Trends Data:', { labels: this.labels, data: this.data });
      this.ChartEmpty = false;
      const ctx = document.getElementById('populationChart') as HTMLCanvasElement;
      console.log(this.data,"chart")
      this.analyticsService.createBarChart(ctx, this.labels, this.data, "Population Trends");
         
      },
      error=>{
        
        console.log("error occured",error);
        alert("Something went wrong try again!!");
      });
      if (this.labels.length === 0 || this.data.length === 0) {
        this.ChartEmpty = true;
        console.log(this.emptyMessage);
        this.labels=["elephants","bears","tigers"];
        this.data=[10,20,5];
      } 
       
  }

  checkAccess(): void {
    this.userRole = localStorage.getItem("userRole"); 

    
    if (this.userRole === 'Admin'||this.userRole==='Manager') {
      this.showDashboard = false;
  }
 
  }

  addObservation() {
    this.router.navigate(['/wildlife/add']);
  }

  editObservation(id:number){
    this.router.navigate([`/wildlife/edit/${id}`])
  }

  // Delete incident method
 deleteObservation(Id: number): void {
  if (confirm("Are you sure you want to delete this observation?")) {
    this.wildlifeService.deleteWildlifeData(Id).subscribe(() => {
      // Reload incidents after deletion
      this.fetchRecentReports();
    }, error => {
      console.error('Error deleting incident:', error);
    });
  }


}

}
