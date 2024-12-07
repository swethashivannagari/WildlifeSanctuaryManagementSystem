import { Component } from '@angular/core';
import { UserService } from '../../services/user.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-users-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './users-list.component.html',
  styleUrl: './users-list.component.scss'
})
export class UsersListComponent {

  users:any[]=[]
  constructor(private userService: UserService,private router:Router) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  // Fetch users from backend
  loadUsers(): void {
    this.userService.getUsers().subscribe({
      next: (data) => {
        this.users = data; 
      },
      error: (err) => {
        console.error('Error fetching users:', err);
      },
    });
  }

  createUser(){
    this.router.navigate([`/user/add`]);
  }
  

  // Delete a user
  deleteUser(userId: any): void {
    const confirmed = window.confirm('Are you sure you want to delete this user?');

    if (confirmed) {
      this.userService.deleteUser(userId).subscribe({
        next: () => {
          console.log('User deleted successfully');
          // Reload users after deletion
          this.loadUsers();
        },
        error: (err) => {
          console.error('Error deleting user:', err);
        },
      });
    }
  }

}
