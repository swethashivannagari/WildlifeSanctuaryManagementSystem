import { Component } from '@angular/core';
import { ReportService } from '../../services/report.service';
import { Report } from '../../../models/report.model';
import { CommonModule, DatePipe } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-report-list',
  standalone: true,
  imports: [CommonModule,DatePipe],
  templateUrl: './report-list.component.html',
  styleUrl: './report-list.component.scss'
})
export class ReportListComponent {
  reports: any[] = [];

  constructor(private reportService: ReportService,private router:Router) {}

  ngOnInit(): void {
    this.fetchReports();
  }

  fetchReports(): void {
   
      this.reportService.getReports().subscribe(
        (data) => {
          console.log(data);
          this.reports = data.map(
            (report:any) => ({...report,showDetails:false } )
          );
          console.log("reports:",this.reports)
        },
        (error) => {
          console.error('Error fetching reports:', error);
        }
      );
    
  }

  addReport():void{
    this.router.navigate(['/report/add'])
  }

  editReport(id:number):void{
    this.router.navigate([`/report/edit/${id}`])
  }

  toggleDetails(report: any): void {
    report.showDetails= !report.showDetails;
  }

  deleteReport(reportId: number|any): void {
    if (confirm('Are you sure you want to delete this project?')) {
      this.reportService.deleteReport(reportId).subscribe(
        () => {
          this.reports= this.reports.filter(report => report.reportId !== reportId); // Remove deleted project from the list
        },
        (error) => {
          console.error('Error deleting report:', error);
        }
      );
    }
  }
}
