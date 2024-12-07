export class ProjectManagement {
    projectId?: number; 
    sanctuaryId: number;
    activityType: string;
    startDate: string; 
    endDate: string; 
    rangerId: number;
    status: string;
  
    constructor(
      sanctuaryId: number,
      activityType: string,
      startDate: string,
      assignedRanger: number,
      status: string,
      endDate: string
      
      
    ) {
      this.sanctuaryId = sanctuaryId;
      this.activityType = activityType;
      this.startDate = startDate;
      this.endDate = endDate;
      this.rangerId = assignedRanger;
      this.status = status;
  
      
      
    }
  }
  