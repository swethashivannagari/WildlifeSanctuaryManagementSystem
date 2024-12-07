import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { WildlifeService } from '../../services/wildlife.service';

import { WildlifeData } from '../../../models/wildlife.model'
import { CommonModule } from '@angular/common';
import { ReportService } from '../../services/report.service';

@Component({
  selector: 'app-wildlife',
  standalone: true,
  imports: [FormsModule, CommonModule, ReactiveFormsModule],
  templateUrl: './wildlife.component.html',
  styleUrl: './wildlife.component.scss'
})
export class WildlifeComponent {
  wildlifeDataForm: FormGroup;
  isEditMode: boolean = false; // Set to true if editing
  statusMessage: string = '';
  wildlifeDataId!:number;

  constructor(private fb: FormBuilder, private router: Router,
    private route: ActivatedRoute,
    private wildlifeDataService: WildlifeService,private reportService:ReportService) {
    this.wildlifeDataForm = this.fb.group({
      sanctuaryId: ['', Validators.required],
      species: ['', [Validators.required, Validators.maxLength(100)]],
      observationDate: ['', Validators.required],
      behavioralReport: ['', Validators.maxLength(500)],
      populationEstimate: [''],
     
    });
  }
  ngOnInit(): void {
    this.wildlifeDataId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.wildlifeDataId) {
      this.isEditMode = true;
      this.loadWildlifeData(this.wildlifeDataId);
    }
  }

  loadWildlifeData(id: number): void {
    this.wildlifeDataService.getWildlifeDataById(id).subscribe((data: WildlifeData) => {
      if (data) {
        this.wildlifeDataForm.patchValue({
          sanctuaryId: data.sanctuaryId,
          species: data.species,
          observationDate: data.observationDate,
          behavioralReport: data.behavioralReport,
          populationEstimate: data.populationEstimate,
         
        });
      }
    });
  }
  onSubmitWildlifeData(): void {
    const role=localStorage.getItem("userRole");
    if (this.wildlifeDataForm.valid) {
      const wildlifeData: WildlifeData = {
        sanctuaryId: this.wildlifeDataForm.value.sanctuaryId,
        species: this.wildlifeDataForm.value.species,
        observationDate: this.wildlifeDataForm.value.observationDate,
        behavioralReport: this.wildlifeDataForm.value.behavioralReport,
        populationEstimate: this.wildlifeDataForm.value.populationEstimate,
        
       
      };
      const userId = localStorage.getItem("userId");
      wildlifeData.biologistId = userId ? parseInt(userId, 10) : undefined;
      if (this.isEditMode) {
        wildlifeData.dataId = this.wildlifeDataId;
       
        this.wildlifeDataService.updateWildlifeData(this.wildlifeDataId, wildlifeData).subscribe(
          () => {
            alert('Wildlife data updated successfully!');
            this.reportService.addObservation(`Wildlife Record${wildlifeData.dataId} updated`);
            this.reportService.updateActionReport("update");
            if (role === 'Admin') {
              this.router.navigate(['main/wildlife']);
            } else {
              this.router.navigate(['/biologist']);
            }
          },
          (error) => {
            console.error('Error updating wildlife data', error);
            alert('Failed to update wildlife data.');
          }
        );
      } else {
        const userId = localStorage.getItem('userId');
        wildlifeData.biologistId = userId ? parseInt(userId, 10) : 0;
        this.wildlifeDataService.createWildlifeData(wildlifeData).subscribe(
          () => {
            console.log(wildlifeData);
            alert('Wildlife data added successfully!');
            this.reportService.addObservation(`Wildlife Record${wildlifeData.dataId} updated`);
            this.reportService.updateActionReport("update");
            if (role === 'Admin') {
              this.router.navigate(['main/wildlife']);
            } else {
              this.router.navigate(['/biologist']);
            }
          },
          (error) => {
            console.error('Error adding wildlife data', error);
            alert('Failed to add wildlife data.');
          }
        );
      }
    }
}
}
