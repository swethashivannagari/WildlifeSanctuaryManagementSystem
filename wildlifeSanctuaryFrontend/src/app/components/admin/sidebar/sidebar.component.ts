import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { routes } from '../../../app.routes';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterLinkActive,RouterLink,CommonModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss'
})
export class SidebarComponent {
  @Output() sidebarToggle = new EventEmitter<void>();
  isCollapsed = false; 
  role:string|null="";
  showUsers:boolean=true;
  toggleCollapse(): void {
    this.isCollapsed = !this.isCollapsed;
  }
  ngAfterViewInit(): void {
    this.checkRole();
  }
  checkRole(){
    this.role=localStorage.getItem("userRole");
    if(this.role=="Manager"){
      this.showUsers=false;
    }
    console.log("role:",this.role);
  }
  menuItems = [
    { label: 'Dashboard', icon: 'fas fa-tachometer-alt', route: '/main/dashboard' },
    { label: 'Sanctuaries', icon: 'fas fa-leaf', route: '/main/sanctuaries' },
    { label: 'Animals', icon: 'fas fa-paw', route: '/main/animals' },
    { label: 'Projects', icon: 'fas fa-folder', route: '/main/projects' },
    { label: 'Incidents', icon: 'fas fa-exclamation-circle', route: '/main/incidents' },
    {label:'Reports',icon:'fas fa-book',route:'/main/reports'},
    {label:'Expenses',icon:'fas fa-money',route:'/main/expenses'},
    {label:'WildLifeData',icon:'fas fa-feather',route:'/main/wildlife'},
    {label:'Assessments',icon:'fas fa-microscope',route:'/main/assessments'},
  ];

  toggleSidebar(): void {
    this.sidebarToggle.emit();
  }
}
