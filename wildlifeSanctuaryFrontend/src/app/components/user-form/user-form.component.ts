import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { User } from '../../../models/user.model';
import { UserService } from '../../services/user.service';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-user-form',
  standalone: true,
  imports: [CommonModule,FormsModule,ReactiveFormsModule],
  templateUrl: './user-form.component.html',
  styleUrl: './user-form.component.scss'
})
export class UserFormComponent {
  userForm: FormGroup;
  roles: string[] = ['Admin', 'Manager', 'Ranger', 'Biologist', 'Veterinarian', 'Conservationist']; 
  currentUserRole: string|null = '';

constructor(private formBuiler:FormBuilder,private authService:AuthService,private router:Router,private userService:UserService){
  this.userForm=this.formBuiler.group({
    username:['',[Validators.required,Validators.maxLength(50)]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    role: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
  })
}

ngOnInit():void{
  const token=localStorage.getItem('authToken');
  
  this.currentUserRole=localStorage.getItem('userRole');
  console.log(this.currentUserRole);
  // Restrict role options based on logged-in user's role
  if (this.currentUserRole === 'Admin') {
    this.roles = ['Admin', 'Manager', 'Ranger', 'Biologist', 'Veterinarian', 'Conservationist'];
  } else if (this.currentUserRole === 'Manager') {
    this.roles = ['Ranger', 'Biologist', 'Veterinarian', 'Conservationist'];
  } else {
    alert('Access denied');
    this.router.navigate(['main/dashboard']);
  }
 
}
addUser():void{
  
  if(this.userForm.valid){
    const user=new User(
      this.userForm.value.username,
      this.userForm.value.password,
      this.userForm.value.role,
      this.userForm.value.email
    );

    console.log(user);
    this.userService.addUser(user).subscribe(
      response=>{
        alert('User added Successfully!');
        this.router.navigate(['user/add']);
      },
      error=>{
        console.log('Error adding User',error);
        alert('Failed to add user');
      }
    );
  }
}

}

