import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReportService } from '../../services/report.service';
import { ActivatedRoute, Router } from '@angular/router';

import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { Report } from '../../../models/report.model';


@Component({
  selector: 'app-report',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],  // Added necessary imports
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss']
})
export class ReportComponent {
  reportForm!: FormGroup;
  isEditMode = false;
  reportId!: number;

  constructor(
    private fb: FormBuilder,
    private reportService: ReportService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    // Retrieve the reportId from the route
    this.reportId = Number(this.route.snapshot.paramMap.get('id'));

    // Initialize the form
    this.reportForm = this.fb.group({
      type: ['', [Validators.required, Validators.maxLength(50)]],
      generatedDate: ['', Validators.required],
      data: ['', [Validators.required, Validators.maxLength(5000)]]
    });

    // If the reportId exists, load the report for editing
    if (this.reportId) {
      this.isEditMode = true;
      this.loadReport(this.reportId);
    }
  }

  // Load the report data for editing
  loadReport(id: number): void {
    this.reportService.getReportById(id).subscribe(
      (report: Report) => {
        this.reportForm.patchValue(report);
      },
      (error) => {
        console.error('Error loading report', error);
        alert('Failed to load report data.');
      }
    );
  }

  // Handle form submission
  onReportFormSubmit(): void {
    if (this.reportForm.invalid) {
      alert('Please fill out all required fields.');
      return;
    }

    const report: Report= this.reportForm.value;
    const userId = localStorage.getItem('userId');
    report.userId = userId ? parseInt(userId, 10) : 0;
    

    if (this.isEditMode) {
      report.reportId=this.reportId;
      this.reportService.updateReport(this.reportId, report).subscribe(
        () => {
          alert('Report updated successfully!');
          
        },
        (error) => {
          console.error('Error updating report', error);
          alert('Failed to update report.');
        }
      );
    } else {
      this.reportService.createReport(report).subscribe(
        () => {
          alert('Report added successfully!');
          this.router.navigate(['/main/reports']); // Corrected route
        },
        (error) => {
          console.error('Error adding report', error);
          alert('Failed to add report.');
        }
      );
    }
  }

  // Delete the report
  deleteReport(reportId: number): void {
    this.reportService.deleteReport(reportId).subscribe(
      () => {
        alert('Report deleted successfully!');
        this.router.navigate(['/reports']); // Navigate back to report list
      },
      (error) => {
        console.error('Error deleting report', error);
        alert('Failed to delete report.');
      }
    );
  }

  // Reset the form for new report
  resetForm(): void {
    this.isEditMode = false;
    this.reportId = 0; // Clear reportId
    this.reportForm.reset();
  }
}
