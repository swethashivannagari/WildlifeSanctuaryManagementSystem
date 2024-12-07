export class Report {
    reportId?: number;
    type: string; 
    generatedDate?: string; 
    data: string; 
    userId: number; 
   // showDetails?: boolean; 
  
    constructor(
      type: string,
      data: string,
      userId: number,
      reportId?: number
    ) {
      this.type = type;
      
      this.data = data;
      this.userId = userId;
  
      if (reportId) {
        this.reportId = reportId;
      }
     // this.showDetails=false;
    }
  }
  