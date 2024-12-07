export class MedicalRecord {
    recordId?: number;
    animalId: number;
    date: string; 
    diagnosis: string;
    treatment: string;
    vetId?: number;
    nextCheckup?: string; 
    constructor(
        AnimalId: number,
        Date: string,
        Diagnosis: string,
        Treatment: string,
        
        NextCheckup:string 
      ) {
        this.animalId = AnimalId;
        this.date = Date;
        this.diagnosis = Diagnosis;
        this.treatment = Treatment;
       
        this.nextCheckup=NextCheckup;
      }

   
  }
  