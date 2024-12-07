import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MedicalRecord } from '../../../models/medicalRecord.model';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MedicalRecordService } from '../../services/medical-record.service';
import { ReportService } from '../../services/report.service';

@Component({
  selector: 'app-vet-form',
  standalone: true,
  imports: [FormsModule,CommonModule,ReactiveFormsModule],
  templateUrl: './vet-form.component.html',
  styleUrl: './vet-form.component.scss'
})
export class VetFormComponent {

  medicalRecordForm: FormGroup;
  isEditMode: boolean = false;
  medicalRecordId!:number;

  constructor(private fb: FormBuilder,
    private router:Router,
    private route:ActivatedRoute,private medicalRecordService:MedicalRecordService,
    private reportService:ReportService) {
    this.medicalRecordForm = this.fb.group({
      animalId: [null, [Validators.required]],
      date: [null, [Validators.required]],
      diagnosis: [null, [Validators.required, Validators.maxLength(200)]],
      treatment: [null, [Validators.required, Validators.maxLength(500)]],
      
      nextCheckup: [null]
    });
  }
  ngOnInit(): void {
    this.medicalRecordId=Number(this.route.snapshot.paramMap.get('id'));
    if (this.medicalRecordId) {
      this.isEditMode=true;
      this.loadRecordData(this.medicalRecordId);
    }
  }

  loadRecordData(id:number): void {
    this.medicalRecordService.getMedicalRecordById(id).subscribe((record:MedicalRecord)=>{
      if(record){
        console.log(record.animalId);
        
        this.medicalRecordForm.patchValue({
         
            animalId: record.animalId,
            
            date: record.date,
            diagnosis: record.diagnosis,
            treatment: record.treatment,
            
            nextCheckup:record.nextCheckup,
            
        })
        console.log(this.medicalRecordForm);
      }
    }); 
  }

  onSubmitMedicalRecord():void{
    if(this.medicalRecordForm.valid){
      const medicalRecord=new MedicalRecord(
        this.medicalRecordForm.value.animalId,
        this.medicalRecordForm.value.date,
        this.medicalRecordForm.value.diagnosis,
        this.medicalRecordForm.value.treatment,
        
        this.medicalRecordForm.value.nextCheckup,
      );

      if(this.isEditMode){
        medicalRecord.recordId=this.medicalRecordId;
        //update from service
        this.medicalRecordService.updateMedicalRecord(this.medicalRecordId,medicalRecord).subscribe(
          (response)=>{
            alert('Record updated successfully!');
            this.reportService.addObservation(`Medical Record ${medicalRecord.recordId} updated`);
            this.reportService.updateActionReport("update");
            const role=localStorage.getItem("userRole");
            if(role==="Admin"||role==="Manager")
            this.router.navigate(['main/medicalRecords']);
            else{
              this.router.navigate(['/biologist']); 
            }
          },
          (error) => {
             
              console.error('Error adding sanctuary', error);
              alert('Failed to add sanctuary');
            
          }
        );
      }
      else{
        const userId = localStorage.getItem('userId');
        medicalRecord.vetId = userId ? parseInt(userId, 10) : 0;
          this.medicalRecordService.addMedicalRecord(medicalRecord).subscribe(
            (response)=>{
              alert('Record added Successfully');
              this.reportService.addObservation(`Medical Record ${medicalRecord.recordId} updated`);
              this.reportService.updateActionReport("add");
              this.router.navigate(['/biologist']);
            },
            error=>{
              console.log("Error adding sucessfully",error);
              alert('Failed to add record');
            }
          );
      }
    }
  }

}
