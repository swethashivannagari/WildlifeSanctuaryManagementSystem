export class Sanctuary {
  sanctuaryId?: number;  
  name: string;          
    location: string;       
    totalArea: number;      
    habitatType: string;    
    protectedSpecies: string; 
    status:string;
    managerId?:number;
           

    constructor(    
      name: string,
      location: string,
      totalArea: number,
      habitatType: string,
      protectedSpecies: string,
      status:string,
           
    ) {  
      this.name = name;
      this.location = location;
      this.totalArea = totalArea;
      this.habitatType = habitatType;
      this.protectedSpecies = protectedSpecies;
      this.status=status
    }
  }
  