import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Sanctuary } from '../../../models/sanctuary.model';
import { SanctuaryService } from '../../services/sanctuary.service';
import { response } from 'express';
import { error } from 'console';

@Component({
  selector: 'app-add-sanctuary',
  standalone: true,
  imports: [ReactiveFormsModule,FormsModule,CommonModule],
  templateUrl: './add-sanctuary.component.html',
  styleUrl: './add-sanctuary.component.scss'
})
export class AddSanctuaryComponent {
sanctuaryForm:FormGroup;
currentUserRole:string|null="";
isEditMode=false;
sanctuaryId!:number;


constructor(private fb:FormBuilder,
  private router:Router,
  private route:ActivatedRoute,
  private sanctuaryService:SanctuaryService){
 
  this.sanctuaryForm=this.fb.group({
    sanctuaryName:['',[Validators.required,Validators.maxLength(100)]],
    location:['',[Validators.required]],
    totalArea:[null,[Validators.required,Validators.min(0.1)]],
    habitatType:['',[Validators.required]],
    protectedSpecies: [''], 
    status:[''],
    
  })
}

ngOnInit():void{
  this.currentUserRole=localStorage.getItem('userRole');
   

  //check if edit mode 
  this.sanctuaryId=Number(this.route.snapshot.paramMap.get('id'));
  if(this.sanctuaryId){
    this.isEditMode=true;
    this.loadSanctuary(this.sanctuaryId);
  }
  
}

//Fetch sanctuary data for editing
loadSanctuary(id:number):void{
  this.sanctuaryService.getSanctuaryById(id).subscribe((sanctuary:Sanctuary)=>{
    if(sanctuary){
      this.sanctuaryForm.patchValue({
        sanctuaryName: sanctuary.name,
        location: sanctuary.location,
        totalArea: sanctuary.totalArea,
        habitatType: sanctuary.habitatType,
        protectedSpecies: sanctuary.protectedSpecies,
        status: sanctuary.status,
      });
      }
  });
}

addSanctuary():void{
  
  if(this.sanctuaryForm.valid){
   const sanctuary=new Sanctuary(
    this.sanctuaryForm.value.sanctuaryName,this.sanctuaryForm.value.location,
    this.sanctuaryForm.value.totalArea,
    this.sanctuaryForm.value.habitatType,
    this.sanctuaryForm.value.protectedSpecies,
    this.sanctuaryForm.value.status,
  
   );
   sanctuary.managerId = parseInt(localStorage.getItem('userId') || '0', 10);

   if(this.isEditMode){
    sanctuary.sanctuaryId=this.sanctuaryId;
    console.log(sanctuary);
    this.sanctuaryService.updateSanctuary(this.sanctuaryId,sanctuary).subscribe(
      response=>{
        alert('sanctuary updated successfully!');
        this.router.navigate(['main/sanctuaries']);
      },
      (error) => {
        if (error.status === 409) {
          alert(error.error.message || 'Sanctuary already exists.');
        } else {
          console.error('Error adding sanctuary', error);
          alert('Failed to add sanctuary');
        }
      }
    )
   }
   else{

   this.sanctuaryService.addSanctuary(sanctuary).subscribe(
    response=>{
      alert('Sanctuary added successfully!');
      this.router.navigate(['main/sanctuaries']);
    },
    error=>{
      console.log("Error adding sucessfully",error);
      alert('Failed to add sanctuary');
    }
   );
  }
   
  }
}

}
