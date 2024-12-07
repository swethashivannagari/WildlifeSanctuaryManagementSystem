export class CostManagement {
    costId?: number;
    sanctuaryId: number;
    expenseType: string;
    amount: number;
    date?: Date;
    responsiblePersonId: number;
    
  
    constructor(
     
      sanctuaryId: number,
      expenseType: string,
      amount: number,
     
      responsiblePersonId: number,
     
    ) {
     
      this.sanctuaryId = sanctuaryId;
      this.expenseType = expenseType;
      this.amount = amount;
      
      this.responsiblePersonId = responsiblePersonId;
     
    }
  }
  