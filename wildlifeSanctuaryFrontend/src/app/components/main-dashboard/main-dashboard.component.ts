import { Component } from '@angular/core';
// import { MainSideBarComponent } from '../main-side-bar/main-side-bar.component';
// import { MainContentComponent } from '../main-content/main-content.component';
import { CommonModule } from '@angular/common';
import { AdminDashboardService, Event, DashboardCounts } from '../../services/admin-dashboard.service';
import { Router, RouterLink } from '@angular/router';
import { AnalyticsService } from '../../services/analytics.service';

@Component({
  selector: 'app-main-dashboard',
  standalone: true,
  imports: [CommonModule,RouterLink],
  templateUrl: './main-dashboard.component.html',
  styleUrls: ['./main-dashboard.component.scss']
})
export class MainDashboardComponent {
  labels: string[] = [];
  costChartEmpty: boolean = false;
  incidentChartEmpty:boolean=false;
  emptyMessage: string = "No data available to display.";

  data: number[] = [];
  checkupPercent: { [key: string]: number } = {};
  totalExpenses: { [key: string]: number } = {};
  upcomingEvents: Event[] = [ {
    eventType: 'Animal Rescue',
    eventDetail: 'Rescuing a stranded dolphin at the coast',
    eventDate: new Date('2024-12-10')
  },
  {
    eventType: 'Sanctuary Visit',
    eventDetail: 'Guided tour of the sanctuary for school students',
    eventDate: new Date('2024-12-15')
  }];
  dashboardCounts: DashboardCounts = {
    animalCount: 0,
    sanctuaryCount: 0,
    incidentCount: 0,
    userCount: 0
  };

  tasks = [
    { title: 'Review Incident Reports', status: 'Pending' },
    { title: 'Update Sanctuary Data', status: 'In Progress' },
    { title: 'Schedule Health Checkups', status: 'Completed' }
  ];

  isSidebarCollapsed = false;
  isProfileMenuOpen = false;

  constructor(
    private analyticsService: AnalyticsService,
    private dashboardService: AdminDashboardService
  ) {}

  ngOnInit(): void {
    this.initializeDashboardData();
  }

  initializeDashboardData(): void {
    this.loadCheckupPercent();
    this.loadTotalExpenses();
    this.loadUpcomingEvents();
    this.loadDashboardCounts();
    this.loadIncidentData();
    this.loadCostData();
  }

  loadCheckupPercent(): void {
    this.dashboardService.getCheckupPercent().subscribe(
      data => {
        this.checkupPercent = data;
        console.log('Checkup Percent:', this.checkupPercent);
      },
      error => {
        console.error('Error fetching checkup percent:', error);
      }
    );
  }

  loadTotalExpenses(): void {
    this.dashboardService.getTotalExpenses().subscribe(
      data => {
        this.totalExpenses = data;
        console.log('Total Expenses:', this.totalExpenses);
      },
      error => {
        console.error('Error fetching total expenses:', error);
      }
    );
  }

  loadUpcomingEvents(): void {
    this.dashboardService.getUpcomingEvents().subscribe(
      data => {
        this.upcomingEvents = data.length ? data : this.upcomingEvents;
        console.log('Upcoming Events:', this.upcomingEvents);
      },
      error => {
        console.error('Error fetching upcoming events:', error);
      }
    );
  }

  loadDashboardCounts(): void {
    this.dashboardService.getDashboardCounts().subscribe(
      data => {
        this.dashboardCounts = data;
        console.log('Dashboard Counts:', this.dashboardCounts);
      },
      error => {
        console.error('Error fetching dashboard counts:', error);
      }
    );
  }

  loadIncidentData(): void {
    this.dashboardService.getTotalIncidents().subscribe(
      response => {
        this.labels = Object.keys(response);
        this.data = Object.values(response);
        this.incidentChartEmpty = this.labels.length === 0 || this.data.length === 0;

        if (this.incidentChartEmpty) {
          console.log(this.emptyMessage);
        } else {
          const ctx = document.getElementById('barChart') as HTMLCanvasElement;
          this.analyticsService.createBarChart(ctx, this.labels, this.data, "Total Incidents by Sanctuary");
        }
      },
      error => {
        console.error('Error loading incident data:', error);
        alert("Something went wrong, try again!");
      }
    );
  }

  loadCostData(): void {
    this.dashboardService.getTotalExpenses().subscribe(
      response => {
        this.labels = Object.keys(response);
        this.data = Object.values(response);
        this.costChartEmpty = this.labels.length === 0 || this.data.length === 0;

        if (this.costChartEmpty) {
          console.log(this.emptyMessage);
        } else {
          const ctx = document.getElementById('costChart') as HTMLCanvasElement;
          this.analyticsService.createBarChart(ctx, this.labels, this.data, "Total Expenses by Sanctuary");
        }
      },
      error => {
        console.error('Error loading cost data:', error);
        alert("Something went wrong, try again!");
      }
    );
  }

  toggleSidebar(): void {
    this.isSidebarCollapsed = !this.isSidebarCollapsed;
    console.log('Sidebar Collapsed:', this.isSidebarCollapsed);
  }

  toggleProfileMenu(): void {
    this.isProfileMenuOpen = !this.isProfileMenuOpen;
    console.log('Profile Menu Open:', this.isProfileMenuOpen);
  }

  logout(): void {
    // Implement logout logic here
    console.log('User logged out');
  }
}
