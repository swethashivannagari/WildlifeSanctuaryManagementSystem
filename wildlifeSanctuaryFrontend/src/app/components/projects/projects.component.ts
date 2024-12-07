import { Component } from '@angular/core';
import { ProjectService } from '../../services/project.service';
import { ProjectManagement } from '../../../models/project.model';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-projects',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './projects.component.html',
  styleUrl: './projects.component.scss'
})
export class ProjectsComponent {

  projects: ProjectManagement[] = [];

  constructor(private projectService: ProjectService,private router:Router) {}

  ngOnInit(): void {
    this.loadProjects();
  }

  addProject():void{
    this.router.navigate(['/project/add'])
  }

  editProject(id?:number):void{
    this.router.navigate([`/project/edit/${id}`])
  }

  loadProjects(): void {
    this.projectService.getProjects().subscribe(
      (data) => {
        console.log(data);
        this.projects = data;
      },
      (error) => {
        console.error('Error fetching projects:', error);
      }
    );
  }

  // Delete a project
  deleteProject(projectId: number|any): void {
    if (confirm('Are you sure you want to delete this project?')) {
      this.projectService.deleteProject(projectId).subscribe(
        () => {
          this.projects = this.projects.filter(project => project.projectId !== projectId); // Remove deleted project from the list
        },
        (error) => {
          console.error('Error deleting project:', error);
        }
      );
    }
  }
}
