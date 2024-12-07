import { Component, HostListener } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './navbar/navbar.component';
import { SidebarComponent } from './sidebar/sidebar.component';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [RouterOutlet,NavbarComponent,SidebarComponent],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.scss'
})
export class AdminComponent {

  isSidebarCollapsed = false;
  screenSize = 'lg';

  toggleSidebar() {
    this.isSidebarCollapsed = !this.isSidebarCollapsed;
  }
  @HostListener('window:resize', [])
  detectScreenSize() {
    const width = window.innerWidth;
    if (width < 768) this.screenSize = 'sm';
    else if (width < 1024) this.screenSize = 'md';
    else this.screenSize = 'lg';
  }

}
