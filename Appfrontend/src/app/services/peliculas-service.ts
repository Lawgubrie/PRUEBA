import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { PeliculaCreateDTO, PeliculaDTO, PeliculaUpdateDTO } from '../models/peliculaDTO';

@Injectable({
  providedIn: 'root'
})
export class PeliculasService {
  private apiURL = 'http://localhost:5041/api/peliculas'

  constructor(private http: HttpClient) {}

  getPeliculas(): Observable<PeliculaDTO[]> {
    return this.http.get<PeliculaDTO[]>(this.apiURL);
  }

  createPelicula(pelicula: PeliculaCreateDTO): Observable<PeliculaDTO> {
    return this.http.post<PeliculaDTO>(this.apiURL, pelicula);
  }

  updatePelicula(pelicula: PeliculaUpdateDTO): Observable<void> {
    const url = `${this.apiURL}/${pelicula.id_Pelicula}`;
    return this.http.put<void>(url, pelicula);
  }

  deletePelicula(id: number): Observable<void> {
    const url = `${this.apiURL}/${id}`;
    return this.http.delete<void>(url);
  }

  buscarPeliculaPorNombre(nombre: string): Observable<PeliculaDTO[]> {
    const url = `${this.apiURL}/buscar/${nombre}`;
    return this.http.get<PeliculaDTO[]>(url);
  }
}
