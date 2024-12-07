export class Animal {
    animalId: number;
    species: string;
    age: number;
    gender: string;
    healthStatus: string;
    currentLocation: string;
    lastCheckupDate: string; // ISO date format
    sanctuaryId: number;
    sanctuaryName?: string;
  
    constructor(
      animalId: number = 0,
      species: string = '',
      age: number = 0,
      gender: string = '',
      healthStatus: string = '',
      currentLocation: string = '',
      lastCheckupDate: string = '',
      sanctuaryId: number = 0,
      sanctuaryName?: string
    ) {
      this.animalId = animalId;
      this.species = species;
      this.age = age;
      this.gender = gender;
      this.healthStatus = healthStatus;
      this.currentLocation = currentLocation;
      this.lastCheckupDate = lastCheckupDate;
      this.sanctuaryId = sanctuaryId;
      this.sanctuaryName = sanctuaryName;
    }
  }
  