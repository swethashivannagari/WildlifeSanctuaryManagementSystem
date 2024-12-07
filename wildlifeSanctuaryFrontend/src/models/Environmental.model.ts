export class EnvironmentalData {
    assessmentId!: number; 
    sanctuaryId!: number;  
    impactType!: string;  
    date!: Date;  
    recommendations?: string;  
    conductedBy!: number;  

    constructor(
        sanctuaryId: number, 
        impactType: string,  
        date: Date,  
        recommendations: string,  
        
       
      ) {
       
        this.sanctuaryId = sanctuaryId;
        this.impactType=impactType;
        this.date=date;
        this.recommendations=recommendations;
       
       
      }
  
  }
  
  
  
  