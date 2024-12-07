import { Component } from '@angular/core';
import { AnimalService } from '../../services/animal.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-animals-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './animals-list.component.html',
  styleUrl: './animals-list.component.scss'
})
export class AnimalsListComponent {
  animals: any[] = [];
  expandedAnimal: number | null = null;

  constructor(private animalService: AnimalService,private router:Router) {}

  ngOnInit(): void {
    this.loadAnimals();
  }

  loadAnimals(): void {
    this.animalService.getAnimals().subscribe({
      next: (data) => (this.animals = data),
      error: (error) => console.error('Error fetching animals', error),
    });
  }

  toggleDetails(animal: any): void {
    this.expandedAnimal = this.expandedAnimal === animal ? null : animal;
  }

  addAnimal(){
    this.router.navigate([`/animal/add`]);
  }
  editAnimal(animalId: any): void {
   
    this.router.navigate([`/animal/edit/${animalId}`]);
  }

  deleteAnimal(animalId: any): void {
  
    const confirmed = window.confirm('Are you sure you want to delete this animal?');
    
    if (confirmed) {
      console.log('Delete animal:', animalId);
      this.animalService.deleteAnimal(animalId).subscribe({
        next: (response) => {
          this.animals = this.animals.filter(a => a.animalId !== animalId);
        },
        error: (err) => {
          console.error('Error deleting animal:', err);
        }
      });
    } else {
      console.log('Animal deletion canceled');
    }
  }
  
}
