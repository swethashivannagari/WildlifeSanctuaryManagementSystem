import { Component } from '@angular/core';
import { Incident } from '../../../models/incident.model';
import { IncidentService } from '../../services/incident.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-incident',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './incident.component.html',
  styleUrls: ['./incident.component.scss']
})
export class IncidentComponent {
  incidents: Incident[] = [];
  selectedSeverity: string = '';
  selectedResolutionStatus: string = '';
  role: string | null = "";

  constructor(private incidentService: IncidentService, private router: Router) {}

  ngOnInit(): void {
    this.role = localStorage.getItem("userRole");
    this.loadIncidents();
  }

  loadIncidents(): void {
    if(this.role===	'Admin'||this.role==="Manager"){
      this.incidentService.getAllIncidents().subscribe(
        (data) => {this.incidents = data},
        (error) => console.error('Error fetching incidents:', error)
      );
  
    }
    else
    {this.incidentService.getFilteredIncidents(this.selectedSeverity, this.selectedResolutionStatus).subscribe(
      (data) => this.incidents = data,
      (error) => console.error('Error fetching incidents:', error)
    );
  }

  }

  filterIncidents(): void {
    this.loadIncidents();
  }

  editIncident(incidentId: number): void {
    this.router.navigate([`/incident/edit/${incidentId}`]);
  }

   

  addIncident(): void {
    this.router.navigate([`/incident/add`]);
  }

  deleteIncident(incidentId: number): void {
    if (confirm("Are you sure you want to delete this incident?")) {
      this.incidentService.deleteIncident(incidentId).subscribe(() => this.loadIncidents());
    }
  }

  updateResolutionStatus(incident: Incident): void {
    this.incidentService.updateIncident(incident).subscribe(
      () =>{ this.loadIncidents()
        alert("status Updated")
      },
      (error) => console.error('Error updating incident status:', error)
    );
  }
}
