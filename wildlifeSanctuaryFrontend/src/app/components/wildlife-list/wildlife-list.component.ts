import { WildlifeService } from '../../services/wildlife.service';
import { WildlifeData } from '../../../models/wildlife.model';
import { CommonModule, DatePipe } from '@angular/common';
import { Router } from '@angular/router';
import { Component } from '@angular/core';

@Component({
  selector: 'app-wildlife-list',
  standalone: true,
  imports: [CommonModule,DatePipe],
  templateUrl: './wildlife-list.component.html',
  styleUrl: './wildlife-list.component.scss'
})
export class WildlifeListComponent {
  recentRecords:WildlifeData[]= [];
  constructor(private wildlifeService: WildlifeService,
    private router:Router) {}

  ngOnInit() {
    this.fetchDashboardData();
    
  }

  fetchDashboardData(): void {
    this.wildlifeService.getAllWildlifeData().subscribe(
      (data) => {
        this.recentRecords = data; 
        console.log(this.recentRecords);
      },
      (error) => {
        console.error('Error fetching records', error);
      }
    );
  }

  editObservation(id:number){
    this.router.navigate([`/wildlife/edit/${id}`])
  }

  // Delete incident method
 deleteObservation(Id: number): void {
  if (confirm("Are you sure you want to delete this observation?")) {
    this.wildlifeService.deleteWildlifeData(Id).subscribe(() => {
      // Reload incidents after deletion
      this.fetchDashboardData();
    }, error => {
      console.error('Error deleting incident:', error);
    });
  }

 }
}
