import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { CostService } from '../../services/cost-service.service';
import { CostManagement } from '../../../models/costManagement.model';
import { CommonModule } from '@angular/common';
import { ReportService } from '../../services/report.service';

@Component({
  selector: 'app-cost-form',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './cost-form.component.html',
  styleUrl: './cost-form.component.scss'
})
export class CostFormComponent {
  costManagementForm: FormGroup;
  costId!: number;
  isEditMode: boolean = false;
  currentRole: string | null = "";
  statusmessage: string = "";

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private costService: CostService,
    private reportService:ReportService
  ) {

    this.costManagementForm = this.fb.group({
      sanctuaryId: ['', [Validators.required]], 
      expenseType: ['', [Validators.required]], 
      amount: ['', [Validators.required]],      
      responsiblePersonId: ['', [Validators.required]] 
    });
  }

  ngOnInit() {
    this.currentRole = localStorage.getItem('userRole');

    this.costId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.costId) {
      this.isEditMode = true;
      this.loadCostForm(this.costId);
    }
  }

  loadCostForm(costId: number): void {
    this.costService.getCostById(costId).subscribe(data => {
      console.log('Loaded Cost Data:', data);
      this.costManagementForm.patchValue({
        sanctuaryId: data.sanctuaryId,
        expenseType: data.expenseType,    
        amount: data.amount,            
        responsiblePersonId: data.responsiblePersonId  
      });
    });
  }

  onSubmitCost(): void {
    if (this.costManagementForm.valid) {
      const costData = new CostManagement(
        this.costManagementForm.value.sanctuaryId,
        this.costManagementForm.value.expenseType,    
        this.costManagementForm.value.amount,         
       
        this.costManagementForm.value.responsiblePersonId
      );

      if (this.isEditMode) {
        costData.costId = this.costId;
        this.costService.updateCost(costData).subscribe(
          response => {
            alert('Cost updated successfully!');
            this.reportService.addObservation(`Expense ${costData.expenseType} updated`);
            this.reportService.updateActionReport("update");
            this.router.navigate(['/main/expenses']);
          },
          (error) => {
            console.log('Error updating cost', error);
            alert('Failed to update cost');
          }
        );
      } else {
        this.costService.addCost(costData).subscribe(
          response => {
            alert('Cost added successfully!');
            this.reportService.addObservation(`Expense ${costData.expenseType} added`);
            this.reportService.updateActionReport("add");
            this.router.navigate(['/main/expenses']);
          },
          (error) => {
            console.log('Error occurred. Try Again!', error);
            alert('Error occurred. Try Again!');
          }
        );
      }
    }
  }
}
