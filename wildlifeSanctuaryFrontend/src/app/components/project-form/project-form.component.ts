import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ProjectManagement } from '../../../models/project.model';
import { ProjectService } from '../../services/project.service';
import { ReportService } from '../../services/report.service';

@Component({
  selector: 'app-project-form',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './project-form.component.html',
  styleUrls: ['./project-form.component.scss'],
})
export class ProjectFormComponent {
  projectForm!: FormGroup;
  isEditMode = false;
  projectId!: number;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private projectService: ProjectService,
    private route: ActivatedRoute,
    private reportService:ReportService
  ) {}

  ngOnInit(): void {
    // Initialize form
    this.projectForm = this.fb.group({
      sanctuaryId: ['', [Validators.required]],
      activityType: ['', [Validators.required, Validators.maxLength(100)]],
      startDate: ['', [Validators.required]],
      endDate: ['', [Validators.required]],
      rangerId: ['', [Validators.required]],
      status: ['', [Validators.required]],
    });

    // Get projectId from route params to identify if it's edit mode
    this.route.paramMap.subscribe((params) => {
      this.projectId = +params.get('id')!; // Ensure to convert to number
      if (this.projectId) {
        this.isEditMode = true;
        this.loadProject(this.projectId); // Load project data for editing
      }
    });
  }

  loadProject(id: number): void {
    // Fetch project data by id for editing
    this.projectService.getProjectById(id).subscribe(
      (project: ProjectManagement) => {
        // Patch form with existing project data
        this.projectForm.patchValue({
          sanctuaryId: project.sanctuaryId,
          activityType: project.activityType,
          startDate: project.startDate,
          endDate: project.endDate,
          rangerId: project.rangerId,
          status: project.status,
        });
      },
      (error) => {
        console.error('Error loading project data:', error);
        alert('Failed to load project data.');
      }
    );
  }

  onProjectSubmit(): void {
    if (this.projectForm.invalid) {
      alert('Please fill out all required fields.');
      return;
    }

    
    const project: ProjectManagement = {
      ...this.projectForm.value,
      startDate: this.formatDateToYMD(this.projectForm.value.startDate),
      endDate: this.formatDateToYMD(this.projectForm.value.endDate),
    };
    if (this.isEditMode) {
      project.projectId=this.projectId;
      this.projectService.updateProject(this.projectId, project).subscribe(
        () => {
          alert('Project updated successfully!');
          this.reportService.addObservation(`project ${project.activityType} updated`);
          this.reportService.updateActionReport("update");
            
          this.router.navigate(['main/projects']); // Redirect after success
        },
        (error) => {
          console.error('Error updating project', error);
          alert('Failed to update project.');
        }
      );
    } else {
      // If adding new project, call create API
      this.projectService.createProject(project).subscribe(
        () => {
          alert('Project created successfully!');
          this.reportService.addObservation(`project ${project.activityType} updated`);
          this.reportService.updateActionReport("add");
            
            this.router.navigate(['main/projects']);
          
        },
        (error) => {
          console.error('Error creating project', error);
          alert('Failed to create project.');
        }
      );
    }
  }
  formatDateToYMD(date: string | Date): string {
    const parsedDate = new Date(date);
    const year = parsedDate.getFullYear();
    const month = String(parsedDate.getMonth() + 1).padStart(2, '0'); // Months are 0-based
    const day = String(parsedDate.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  }
}
