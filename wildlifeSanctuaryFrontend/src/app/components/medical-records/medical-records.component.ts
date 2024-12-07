import { Component } from '@angular/core';
import { MedicalRecordService } from '../../services/medical-record.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-medical-records',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './medical-records.component.html',
  styleUrl: './medical-records.component.scss'
})
export class MedicalRecordsComponent {
  medicalRecords:any = [];
  role:string|null="";
  constructor(private medicalRecordService: MedicalRecordService,private router:Router) { }
  ngOnInit(): void {
    this.loadRecords();
    this.role=localStorage.getItem('userRole')
   
  }
  loadRecords(){
    this.medicalRecordService.getMedicalRecords().subscribe(
      (data) => {
        this.medicalRecords = data; 
        console.log(data);
      },
      (error) => {
        console.error('Error fetching medical records', error);
      }
    );
  }

  
editRecord(incidentId: number): void {
  this.router.navigate([`/MedicalRecord/edit/${incidentId}`]);
}

 deleteRecord(recordId: number): void {
  if (confirm("Are you sure you want to delete this incident?")) {
    this.medicalRecordService.deleteMedicalRecord(recordId).subscribe(() => {
      
      this.loadRecords();
    }, error => {
      console.error('Error deleting incident:', error);
    });
  }

}
}
