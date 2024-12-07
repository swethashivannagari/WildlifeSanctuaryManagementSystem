import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { IncidentService } from '../../services/incident.service';
import { Incident } from '../../../models/incident.model';
import { error } from 'console';
import { CommonModule } from '@angular/common';
import { ReportService } from '../../services/report.service';

@Component({
  selector: 'app-incident-form',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule,FormsModule],
  templateUrl: './incident-form.component.html',
  styleUrl: './incident-form.component.scss'
})
export class IncidentFormComponent {
  
  incidentForm: FormGroup;
  incidentId!: number;
  isEditMode: boolean = false;
  currentRole: string | null = "";
  statusmessage: string = "";

  constructor(private fb: FormBuilder, private router: Router, private route: ActivatedRoute,
    private incidentService: IncidentService,private reportService:ReportService) {
    this.incidentForm = this.fb.group({
      sanctuaryId: ['', [Validators.required]],
      date: ['', [Validators.required]],
      description: ['', [Validators.required, Validators.maxLength(500)]],
      severity: ['', [Validators.required]],
     
    });
  }

  ngOnInit() {
    this.currentRole = localStorage.getItem('userRole');
    if (this.currentRole !== "Ranger") {
      alert("Access Denied");
      this.router.navigate(['/']);
    }

    this.incidentId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.incidentId) {
      this.isEditMode = true;
      this.loadIncidentForm(this.incidentId);
    }
  }

  loadIncidentForm(incidentId: Number): void {
    this.incidentService.getIncidentById(incidentId).subscribe(data => {
      console.log('Loaded Incident Data:', data);
      this.incidentForm.patchValue({
        sanctuaryId: data.sanctuaryId,
        date: data.date,
        description: data.description,
        severity: data.severity,
        
      }

      )
    })

  }

  onSubmitIncident(): void {
    const role=localStorage.getItem("userRole");
    if (this.incidentForm.valid) {
      const incident = new Incident(this.incidentForm.value.sanctuaryId,
        this.incidentForm.value.date,
        this.incidentForm.value.description,
        this.incidentForm.value.severity,
        
      )


      if (this.isEditMode) {
        incident.incidentId = this.incidentId;
        
        this.incidentService.updateIncident(incident).subscribe(
          response => {
            alert('sanctuary updated successfully!');
            this.reportService.addObservation(`incident ${incident.incidentId} updated`);
          this.reportService.updateActionReport("update");
            
            if (role === "Admin") {
              this.router.navigate(['/main/incidents']);
            } else {
              this.router.navigate(['/ranger']);
            }
          },
          (error) => {
            console.log('Error updating sanctuary', error);
            alert('Failed to update sanctuary');
          });
      }
      else{
        
        this.incidentService.addIncident(incident).subscribe(
          response=>{
            alert("incident added successfully!!");
            this.reportService.addObservation(`incident ${incident.incidentId} updated`);
            this.reportService.updateActionReport("add");
              
            this.router.navigate(['/ranger']);
          },
          error=>{
            alert("error occured Try Again!!");
            
          }
        )
      }
    }
  }
}
