import { Component } from '@angular/core';
import { AnalyticsService } from '../../services/analytics.service';
import { CommonModule, DatePipe } from '@angular/common';
import { EnvironmentalService } from '../../services/environmental.service';
import { AdminDashboardService, DashboardCounts } from '../../services/admin-dashboard.service';
import { Router } from '@angular/router';
import { EnvironmentalData } from '../../../models/Environmental.model';


@Component({
  selector: 'app-conservationist',
  standalone: true,
  imports: [CommonModule, DatePipe],
  templateUrl: './conservationist.component.html',
  styleUrl: './conservationist.component.scss'
})
export class ConservationistComponent {
  totalAssessments = 50;
  dashboardCounts: DashboardCounts = {
    animalCount: 0,
    sanctuaryCount: 0,
    incidentCount: 0,
    userCount: 0,
    
  };
  userRole :string|null=""
  showDashboard :boolean=true;
  labels: string[] = [];
  data: number[] = [];
  emptyMessage: string = "No data available to display."; 
  chartEmpty:boolean=false;

  recentAssessments:EnvironmentalData[] = [
   ];



  constructor(private environmentalService: EnvironmentalService,
    private dashboardService: AdminDashboardService,
     private analyticsService: AnalyticsService,
    private router:Router) { }

  ngOnInit(): void {
    this.fetchAssessmentsByImpactType();
    this.fetchAssessmentsBySanctuary();
    this.loadAssesmentData();
    this.checkAccess();
  }

  checkAccess(): void {
    this.userRole = localStorage.getItem("userRole"); 

    if (this.userRole === 'Admin'||this.userRole==='Manager') {
      this.showDashboard = false;
  }
  console.log("Bio",this.showDashboard);
  }

  loadAssesmentData(){
    this.environmentalService.getAllEnvironmentalData().subscribe(
      (data)=>{
        this.recentAssessments=data;
        console.log("assessments:",data);
        this.totalAssessments=data.length;
      },
      (error)=>{
        console.error('Error fetching records', error);
      }
    )
  }

  fetchAssessmentsBySanctuary(): void {
    this.environmentalService.getAssessmentsBySanctuary().subscribe(
      (response) => {
        console.log("res", response);
        this.labels = response.map((item: any) => {
         
          return item.sanctuary; 
        });
    
        this.data = response.map((item: any) => item.count);
        this.chartEmpty = this.labels.length === 0 || this.data.length === 0;
  
        if (this.chartEmpty) {
          console.log(this.emptyMessage);
        } else {
          // Create chart if data is available
          const ctx = document.getElementById('barChart') as HTMLCanvasElement;
          this.analyticsService.createBarChart(ctx, this.labels, this.data, "Total Incidents by Sanctuary");
        }
      },
      (error) => {
        console.error('Error fetching assessments by sanctuary:', error);
      });
    if (this.labels.length === 0 || this.data.length === 0) {
      this.chartEmpty = true;
      console.log(this.emptyMessage);
      this.labels = ["Sanctuary A", "Sanctuary B", "Sanctuary C"];
      this.data = [10, 20, 5];
      
    }
   

  }

  fetchAssessmentsByImpactType(): void {
    this.environmentalService.getAssessmentsByImpactType().subscribe(
      (response) => {
        console.log("res",response);
        this.labels = response.map((item: any) => item.impactType);
        this.data = response.map((item: any) => item.count);
        this.chartEmpty = false;
        if (this.labels.length === 0 || this.data.length === 0) {
          this.chartEmpty = true;
          console.log(this.emptyMessage);
          this.labels = ["Biodiversity Loss", "Habitat Degradation", "Climate Impact"];
          this.data = [10, 20, 5];
    
        }
        const ctx = document.getElementById('areaChart') as HTMLCanvasElement;
            this.analyticsService.createAreaChart(ctx, this.labels, this.data, "");

      },
      (error) => {
        console.error('Error fetching assessments by sanctuary:', error);
      }

    );
  }

  loadDashboardCounts(): void {
    this.dashboardService.getDashboardCounts().subscribe(
      data => {
        this.dashboardCounts = data;
        console.log('Dashboard Counts:', this.dashboardCounts);
      },
      error => {
        console.error('Error fetching dashboard counts:', error);
      }
    );
  }

  addAssessment(){
    this.router.navigate(['/assessment/add']);
  }

  editAssessment(id:number){
    this.router.navigate([`/assessment/edit/${id}`])
  }

  deleteAssessment(id:number){
    if (confirm("Are you sure you want to delete this assessment?")) {
      this.environmentalService.deleteEnvironmentalData(id).subscribe(() => {
        // Reload incidents after deletion
        this.loadAssesmentData();
      }, error => {
        console.error('Error deleting assessment:', error);
      });
    }
  }
}
