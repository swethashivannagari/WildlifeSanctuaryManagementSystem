import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { NotificationService } from '../../../services/notification.service';


@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  
  @Output() sidebarToggle = new EventEmitter<void>();
  isProfileMenuOpen=false;
  userId:number|null=null;
  isNotificationMenuOpen = false;
  notifications: any[] = [];


  constructor(private notificationService: NotificationService,private authService:AuthService) {}

  ngAfterViewInit(): void {
    this.userId = localStorage.getItem('userId') ? parseInt(localStorage.getItem('userId')!, 10) : null;
        if (this.userId) {
          this.loadNotifications();
        }
  }

  toggleSidebar() {
    this.sidebarToggle.emit();
  }
  toggleProfileMenu(): void {
    this.isProfileMenuOpen = !this.isProfileMenuOpen;
    
  }
  logout(){
    this.authService.logout();
    sessionStorage.removeItem('recentObservations');
    sessionStorage.removeItem('actionsReport');
  }
  toggleNotificationMenu() {
    this.isNotificationMenuOpen = !this.isNotificationMenuOpen;
    if (this.isNotificationMenuOpen && this.userId) {
      this.loadNotifications(); 
    }
  }

  loadNotifications() {
    console.log(this.userId,"userid");
    if(this.userId!==null){
      
    this.notificationService.getNotifications(this.userId).subscribe((data) => {
      this.notifications = data;
    });
  }
  }

 

  deleteNotification(notificationId: number) {
    this.notificationService.deleteNotification(notificationId).subscribe(() => {
      this.notifications = this.notifications.filter(n => n.id !== notificationId);
      this.loadNotifications();
    });
    this.notifications = this.notifications.filter(n => n.id !== notificationId);
  }
}

 



