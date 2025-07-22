import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { 
  PeliculaSalaCineDTO, 
  PeliculaSalaCineCreateDTO, 
  PeliculaSalaDetalleDTO 
} from '../models/peliculaSalaCineDTO'; // Asegúrate que la ruta al DTO es correcta

@Injectable({
  providedIn: 'root'
})
export class PeliculaSalaCineService {

  private apiURL = 'http://localhost:5041/api/asignaciones';

  constructor(private http: HttpClient) {}

  getPeliculasSalaCine(): Observable<PeliculaSalaCineDTO[]> {
    return this.http.get<PeliculaSalaCineDTO[]>(this.apiURL);
  }

  createAsignacion(asignacion: PeliculaSalaCineCreateDTO): Observable<PeliculaSalaCineDTO> {
    const url = `${this.apiURL}/pelicula/${asignacion.idPelicula}/sala/${asignacion.idSalaCine}`;
    return this.http.post<PeliculaSalaCineDTO>(url, asignacion);
  }

  updateAsignacion(asignacion: PeliculaSalaCineDTO): Observable<void> {
    const url = `${this.apiURL}/${asignacion.idPeliculaSala}`;
    return this.http.put<void>(url, asignacion);
  }

  deleteAsignacion(idAsignacion: number): Observable<void> {
    const url = `${this.apiURL}/${idAsignacion}`;
    return this.http.delete<void>(url);
  }

  getAsignacionesPorFecha(fecha: string): Observable<PeliculaSalaDetalleDTO[]> {
    const url = `${this.apiURL}/por-fecha/${fecha}`;
    return this.http.get<PeliculaSalaDetalleDTO[]>(url);
  }
}