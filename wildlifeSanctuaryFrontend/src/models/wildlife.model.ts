export class WildlifeData {
    dataId?:number
    sanctuaryId: number;  
    species: string;          
      observationDate: string;       
      populationEstimate: number;      
      behavioralReport: string;    
      biologistId?:number
             
  
      constructor(  
        sanctuaryId:number , 
        species: string,          
        observationDate: string,      
        populationEstimate: number,      
        behavioralReport: string,    
       
             
      ) {  
        this.sanctuaryId=sanctuaryId;
        this.species=species;          
        this.observationDate= observationDate;       
        this.populationEstimate= populationEstimate;      
        this.behavioralReport= behavioralReport;    
       
      }
    }
    