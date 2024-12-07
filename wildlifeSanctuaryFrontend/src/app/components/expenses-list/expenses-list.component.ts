import { Component } from '@angular/core';
import { CostService } from '../../services/cost-service.service';
import { AdminDashboardService, DashboardCounts } from '../../services/admin-dashboard.service';
import { error } from 'console';
import { CommonModule, DatePipe } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-expenses-list',
  standalone: true,
  imports: [DatePipe,CommonModule],
  templateUrl: './expenses-list.component.html',
  styleUrl: './expenses-list.component.scss'
})
export class ExpensesListComponent {
  sanctuaries: { [sanctuaryName: string]: number } = {};
  selectedExpenses: any[] = [];
  selectedSanctuary: any = null;
  Object = Object;

  constructor(private costService:CostService,private dashboardService:AdminDashboardService,private router:Router) {}

  ngOnInit(): void {
    this.loadCostData();
  }

  
  loadCostData(): void {
    this.dashboardService
    .getTotalExpenses().subscribe(
      response => {
        this.sanctuaries=response;
        console.log(this.sanctuaries);
      },
      error => {
        console.error('Error loading cost data:', error);
        alert("Something went wrong, try again!");
      }
    );
  }

  viewExpenses(sanctuary: string) {
    this.selectedSanctuary=sanctuary;
    this.costService.getExpensesBySanctuary(sanctuary).subscribe((data) => {
      this.selectedExpenses = data;
      console.log(data);
      
    }),
    (error: any)=>{
      console.error('Error loading cost data:', error);
        alert("Something went wrong, try again!");
    
    }
  }
  closeModal(): void {
    this.selectedExpenses = [];
    this.selectedSanctuary = null;
  }

  addExpense() {
    this.router.navigate(['/expense/add'])
  }

  
  deleteExpense(expenseId: number,sanctuary:string): void {
    if (confirm("Are you sure you want to delete this incident?")) {
      this.costService.deleteCost(expenseId).subscribe(() => {
        
        this.viewExpenses(sanctuary);
      }, error => {
        console.error('Error deleting incident:', error);
      });
    }
  }

  getColor(percentage: number): string {
    if (percentage < 25) {
      return 'green'; 
    } else if (percentage < 60) {
      return 'yellow'; 
    } else if (percentage < 80) {
      return 'orange'; 
    } else {
      return 'red'; 
    }
  }

  getCappedWidth(value: number): number {
    const percentage = (value / 100000) * 100;
    return Math.min(percentage, 100); 
  }

 
}
