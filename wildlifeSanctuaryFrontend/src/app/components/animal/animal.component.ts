import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AnimalService } from '../../services/animal.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Animal } from '../../../models/animal.model';
import { CommonModule } from '@angular/common';
import { ReportService } from '../../services/report.service';

@Component({
  selector: 'app-animal',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, FormsModule],
  templateUrl: './animal.component.html',
  styleUrl: './animal.component.scss'
})
export class AnimalComponent {
  animalForm!: FormGroup;
  isEditMode = false;
  animalId!: number;

  constructor(
    private fb: FormBuilder,
    private animalService: AnimalService,
    private router: Router,
    private route: ActivatedRoute,
    private reportService: ReportService
  ) { }

  ngOnInit(): void {

    this.animalId = Number(this.route.snapshot.paramMap.get('id'));

    this.animalForm = this.fb.group({
      species: ['', [Validators.required, Validators.maxLength(100)]],
      age: [null, [Validators.required, Validators.min(0)]],
      gender: ['', [Validators.required]],
      healthStatus: ['', [Validators.required]],
      currentLocation: ['', [Validators.required, Validators.maxLength(200)]],
      lastCheckupDate: ['', Validators.required],
      sanctuaryId: [null, [Validators.required]],
    });


    if (this.animalId) {
      this.isEditMode = true;
      this.loadAnimal(this.animalId);
    }
  }
  // Load animal data for editing
  loadAnimal(id: number): void {
    this.animalService.getAnimalById(id).subscribe(
      (animal: Animal) => {
        this.animalForm.patchValue(animal);
      },
      (error) => {
        console.error('Error loading animal', error);
        alert('Failed to load animal data.');
      }
    );
  }

  onAnimalFormSubmit(): void {
    const role = localStorage.getItem('userRole');
    if (this.animalForm.invalid) {
      alert('Please fill out all required fields.');
      return;
    }

    const animal: Animal = this.animalForm.value;

    if (this.isEditMode) {
      this.animalService.updateAnimal(this.animalId, animal).subscribe(
        () => {
          alert('Animal updated successfully!');
          this.reportService.addObservation(`Animal id ${this.animalId} updated`);
          this.reportService.updateActionReport("update");
          console.log(role);

          if (role === 'Admin') {
            this.router.navigate(['main/animals']);
          } else {
            this.router.navigate(['/animals']);
          }
        },
        (error) => {
          console.error('Error updating animal', error);
          alert('Failed to update animal.');
        }
      );
    } else {
      this.animalService.addAnimal(animal).subscribe(
        () => {
          alert('Animal added successfully!');
          this.reportService.addObservation(`Animal  ${animal.species} added`);
          this.reportService.updateActionReport("add");
          if (role === "Admin") {
            this.router.navigate(['/main/animals']);
          } else {
            this.router.navigate(['/animals']);
          }
        },
        (error) => {
          console.error('Error adding animal', error);
          alert('Failed to add animal.');
        }
      );
    }

  }

}
