import { CommonModule } from '@angular/common';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, NavigationEnd, RouterLink } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from '../../services/auth.service';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule,RouterLink],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'] // Corrected "styleUrl" to "styleUrls"
})
export class NavbarComponent implements OnInit, OnDestroy {
  userRole: string | null = '';
  userId:number|null=null;
  isProfileMenuOpen: boolean = false;
  showNavbar: boolean = true;
  isNotificationMenuOpen:boolean=false;
  private routerSubscription!: Subscription;
  notifications: any[] = [];
  showNotification:boolean=false;


  constructor(private notificationService:NotificationService, private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.checkAccess();

    
    this.routerSubscription = this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.checkAccess();
      }
      this.userId = localStorage.getItem('userId') ? parseInt(localStorage.getItem('userId')!, 10) : null;
        if (this.userId) {
          this.loadNotifications();
        }
    });
  }

  

  ngOnDestroy(): void {
    // Clean up subscription to prevent memory leaks
    if (this.routerSubscription) {
      this.routerSubscription.unsubscribe();
    }
  }

  toggleMenu(): void {
    const menu = document.getElementById('mobile-menu');
    if (menu) {
      menu.classList.toggle('hidden');
    }
  }

  checkAccess(): void {
    this.userRole = localStorage.getItem('userRole');
    this.showNavbar = !(this.userRole === 'Admin' || this.userRole === 'Manager' || this.userRole === null);
    this.showNotification=this.userRole==='Ranger';
  }

  toggleProfileMenu(): void {
    this.isProfileMenuOpen = !this.isProfileMenuOpen;
    console.log('Profile Menu Open:', this.isProfileMenuOpen);
  }

  logout(): void {
    this.authService.logout();
    sessionStorage.removeItem('recentObservations');
    sessionStorage.removeItem('actionsReport');
    this.router.navigate(['/']);
  }

  ngAfterViewInit(): void {
    this.userId = localStorage.getItem('userId') ? parseInt(localStorage.getItem('userId')!, 10) : null;
        if (this.userId) {
          this.loadNotifications();
        }
  }

  
  toggleNotificationMenu() {
    this.isNotificationMenuOpen = !this.isNotificationMenuOpen;
    if (this.isNotificationMenuOpen && this.userId) {
      this.loadNotifications(); 
    }
  }

  loadNotifications() {
    
    if(this.userId!==null){
      
    this.notificationService.getNotifications(this.userId).subscribe((data) => {
      this.notifications = data;
    });
  }
  }

 

  deleteNotification(notificationId: number) {
    this.notificationService.deleteNotification(notificationId).subscribe(() => {
      this.notifications = this.notifications.filter(n => n.id !== notificationId);
    });
  }

}
