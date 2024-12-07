import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Animal } from '../../models/animal.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AnimalService {

  private apiUrl = `${environment.apiUrl}/Animal`;

  constructor(private httpClient: HttpClient) {}

  // Get all animals
  getAnimals(): Observable<Animal[]> {
    return this.httpClient.get<Animal[]>(this.apiUrl);
  }

  // Get animal by ID
  getAnimalById(id: number): Observable<Animal> {
    return this.httpClient.get<Animal>(`${this.apiUrl}/${id}`);
  }

  // Add a new animal
  addAnimal(animal: Animal): Observable<any> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.httpClient.post(this.apiUrl, animal, { headers });
  }

  // Update an existing animal
  updateAnimal(id: number, animal: Animal): Observable<void> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.httpClient.put<void>(`${this.apiUrl}/${id}`, animal, { headers });
  }

  // Delete an animal
  deleteAnimal(id: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.apiUrl}/${id}`);
  }


}
