import { Component } from '@angular/core';
import { SanctuaryService } from '../../services/sanctuary.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sanctuary-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './sanctuary-list.component.html',
  styleUrl: './sanctuary-list.component.scss'
})
export class SanctuaryListComponent {
sanctuaries:any[]=[];
constructor(private sanctuaryService:SanctuaryService,private router:Router){}

ngOnInit():void{
  this.loadSanctuaries();
}

loadSanctuaries(){
  this.sanctuaryService.getSanctuaries().subscribe((data)=>{
    this.sanctuaries=data;
  })
}
addSanctuary(){
  this.router.navigate(['sanctuary/add']);
}

editSanctuary(id:number){
  this.router.navigate([`sanctuary/edit/${id}`])
}
deleteSanctuary(sanctuaruId:number){
  const confirmed = window.confirm('Are you sure you want to delete this user?');

    if (confirmed) {
      this.sanctuaryService.deleteSanctuary(sanctuaruId).subscribe({
        next: () => {
          console.log('User deleted successfully');
          this.loadSanctuaries();
        },
        error: (err) => {
          console.error('Error deleting user:', err);
        },
      });
    }
  }
getRandomBackground() {
  return Math.random() < 0.5 ? 'bg-green-900' : 'bg-green-600'; 
}
}
