export class Incident {
    incidentId?: number;  
    sanctuaryId: number;  
    date: string;         
    description: string; 
    severity: string; 
    resolutionStatus: string='Unresolved';
  
  
  sanctuaryName?: string;    
    
  
    constructor(
      sanctuaryId: number,
      date: string,
      description: string,
      severity: string,
       
      
      
    ) {
      this.sanctuaryId = sanctuaryId;
      this.date = date;
      this.description = description;
      this.severity = severity;
     
    }
  }
  