

import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EnvironmentalService } from '../../services/environmental.service';
import { ActivatedRoute, Router } from '@angular/router';

import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ReportService } from '../../services/report.service';

@Component({
  selector: 'app-environmental-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],  // Added necessary imports
  templateUrl: './environmental-form.component.html',
  styleUrls: ['./environmental-form.component.scss']
})
export class EnvironmentalDataComponent {
  environmentalForm!: FormGroup;
  isEditMode = false;
  assessmentId!: number;

  constructor(
    private fb: FormBuilder,
    private environmentalDataService: EnvironmentalService,
    private router: Router,
    private route: ActivatedRoute,
    private reportService:ReportService
  ) {}

  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    // Retrieve the assessmentId from the route
    this.assessmentId = Number(this.route.snapshot.paramMap.get('id'));

    // Initialize the form
    this.environmentalForm = this.fb.group({
      sanctuaryId: ['', [Validators.required]],
      impactType: ['', [Validators.required, Validators.maxLength(50)]],
      date: ['', Validators.required],
      recommendations: ['', [Validators.maxLength(500)]],
      
    });

    // If the assessmentId exists, load the environmental data for editing
    if (this.assessmentId) {
      this.isEditMode = true;
      this.loadEnvironmentalData(this.assessmentId);
    }
  }

  // Load the environmental data for editing
  loadEnvironmentalData(id: number): void {
    this.environmentalDataService.getEnvironmentalDataById(id).subscribe(
      (environmentalData) => {
        this.environmentalForm.patchValue(environmentalData);
      },
      (error) => {
        console.error('Error loading environmental data', error);
        alert('Failed to load environmental data.');
      }
    );
  }

  // Handle form submission
  onEnvironmentalFormSubmit(): void {
    const role=localStorage.getItem("userRole");
    
    
   
    if (this.environmentalForm.invalid) {
      alert('Please fill out all required fields.');
      return;
    }
    const userId=localStorage.getItem('userId');
    const environmentalData = this.environmentalForm.value;
    environmentalData.conductedBy=userId ? parseInt(userId, 10) : 0;
    if (this.isEditMode) {
      
     
      this.environmentalDataService.updateEnvironmentalData(this.assessmentId, environmentalData).subscribe(
        () => {
          alert('Environmental data updated successfully!');
          this.reportService.addObservation(`Data ${environmentalData.impactType} updated`);
          this.reportService.updateActionReport("update");
          if (role === "Admin") {
            this.router.navigate(['/main/assessments']);
          } else {
            this.router.navigate(['/conservationist']);
          }
        },
        (error) => {
          console.error('Error updating environmental data', error);
          alert('Failed to update environmental data.');
        }
      );
    } else {
      environmentalData.conductedBy=localStorage.getItem("userId");
      this.environmentalDataService.addEnvironmentalData(environmentalData).subscribe(
        () => {
          alert('Environmental data added successfully!');
          this.reportService.addObservation(`Data ${environmentalData.impactType} updated`);
          this.reportService.updateActionReport("add");
          if (role === "Admin") {
            this.router.navigate(['/main/assessments']);
          } else {
            this.router.navigate(['/conservationist']);
          }
        },
        (error) => {
          console.error('Error adding environmental data', error);
          alert('Failed to add environmental data.');
        }
      );
    }
  }

  // Delete the environmental data
  deleteEnvironmentalData(assessmentId: number): void {
    this.environmentalDataService.deleteEnvironmentalData(assessmentId).subscribe(
      () => {
        alert('Environmental data deleted successfully!');
        this.router.navigate(['/environmental-data']); // Navigate back to the list
      },
      (error) => {
        console.error('Error deleting environmental data', error);
        alert('Failed to delete environmental data.');
      }
    );
  }

  // Reset the form for new environmental data entry
  resetForm(): void {
    this.isEditMode = false;
    this.assessmentId = 0; // Clear assessmentId
    this.environmentalForm.reset();
  }
}

