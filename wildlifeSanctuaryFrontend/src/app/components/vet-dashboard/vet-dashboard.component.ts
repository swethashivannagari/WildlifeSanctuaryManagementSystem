import { Component } from '@angular/core';
import { AnalyticsService } from '../../services/analytics.service';
import { MedicalRecordService } from '../../services/medical-record.service';
import { ActivatedRoute, Router } from '@angular/router';
import { response } from 'express';
import { MedicalRecordsComponent } from '../medical-records/medical-records.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-vet-dashboard',
  standalone: true,
  imports: [MedicalRecordsComponent,CommonModule],
  templateUrl: './vet-dashboard.component.html',
  styleUrl: './vet-dashboard.component.scss'
})
export class VetDashboardComponent {
  labels: string[] = [];
  data: number[] = [];
  criticalAlerts:string[]=[];
  sickAlerts:string[]=[];
  schedules:any=[];
  recordCount:number=0;
  ChartEmpty: boolean = false;
  emptyMessage: string = "No data available to display.";
  
  constructor(private analyticsService:AnalyticsService,private medicalRecordService:MedicalRecordService,
    private route:ActivatedRoute,private router:Router
  ){}

  ngOnInit():void{
    this.loadSeverityData();
    this.loadAlerts();
    this.loadSchedules();
    this.loadRecordCount();
  }

  addRecord(): void {
    this.router.navigate([`/MedicalRecord/add`]);
  }

  loadRecordCount():void{
    this.medicalRecordService.getRecordCount().subscribe(
      (data) => {
        console.log(data);
        this.recordCount=data; 
      },
      (error) => {
        console.error('Error fetching count', error);
      }
    );
  }

  loadAlerts():void{
    this.medicalRecordService.getAnimalsByHealthStatus('Critical').subscribe(
      (animals) => {
        this.criticalAlerts = animals; // Store animals as critical alerts
      },
      (error) => {
        console.error('Error fetching critical alerts', error);
      }
    );

    this.medicalRecordService.getAnimalsByHealthStatus("sick").subscribe(
      (animals) => {
        console.log(animals);
        this.sickAlerts = animals; 
      },
      (error) => {
        console.error('Error fetching critical alerts', error);
      }
    );
  }

  loadSchedules():void{
    this.medicalRecordService.getVetSchedule().subscribe(
      (schedules) => {
        console.log(schedules);
        this.schedules = schedules; 
      },
      (error) => {
        console.error('Error fetching critical alerts', error);
      }
    );
  }

  loadSeverityData():void{
    this.medicalRecordService.getAnimalCountByCriteria("health").subscribe(
      (response) => {
        this.labels = Object.keys(response); 
        this.data = Object.values(response); 
        console.log('Severity Count Data:', { labels: this.labels, data: this.data });
        if (this.labels.length === 0 || this.data.length === 0) {
          this.ChartEmpty = true;
          console.log(this.emptyMessage);
        } else {
          this.ChartEmpty = false;
          const ctx = document.getElementById('donutChart') as HTMLCanvasElement;
          this.analyticsService.createDonutChart(ctx, this.labels, this.data, "Health Severity Status");
        }
      },
      error=>{
        console.log("error occured",error);
        alert("Something went wrong try again!!");
      });

      this.medicalRecordService.getAnimalCountByCriteria("age").subscribe(
        (response) => {
          this.labels = Object.keys(response); 
          this.data = Object.values(response); 
          console.log('Severity Count Data:', { labels: this.labels, data: this.data });
          if (this.labels.length === 0 || this.data.length === 0) {
            this.ChartEmpty = true;
            console.log(this.emptyMessage);
          } else {
            this.ChartEmpty = false;
            const ctx = document.getElementById('barChart') as HTMLCanvasElement;
            this.analyticsService.createBarChart(ctx, this.labels, this.data, "Animals Age Status");
          }
        },
        error=>{
          console.log("error occured",error);
          alert("Something went wrong try again!!");
        });
    
  }
}
