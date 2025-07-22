import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SalaCineCreateDTO, SalaCineDTO, SalaCineUpdateDTO } from '../models/salaCineDTO';
import { DashBoardDTO } from '../models/dashBoardDTO';
import { VerificarDisponibilidadRequestDTO, VerificarDisponibilidadResponseDTO } from '../models/verificarDisponibilidadRequestDTO';

@Injectable({
  providedIn: 'root'
})
export class SalasService {
  private apiURL = 'http://localhost:5041/api/salas-cine'

  constructor(private http: HttpClient){}

  getSalas():Observable<SalaCineDTO[]>{
    return this.http.get<SalaCineDTO[]>(this.apiURL);
  }

  getSala(id: number): Observable<SalaCineDTO> {
    const url = `${this.apiURL}/${id}`;
    return this.http.get<SalaCineDTO>(url);
  }

  createSala(sala: SalaCineCreateDTO): Observable<SalaCineDTO> {
    return this.http.post<SalaCineDTO>(this.apiURL, sala);
  }

  updateSala(sala: SalaCineUpdateDTO): Observable<void> {
    const url = `${this.apiURL}/${sala.id_Sala}`;
    return this.http.put<void>(url, sala);
  }


  deleteSala(id: number): Observable<void> {
    const url = `${this.apiURL}/${id}`;
    return this.http.delete<void>(url);
  }

  verificarDisponibilidad(nombreSala: string): Observable<VerificarDisponibilidadResponseDTO> {
    const url = `${this.apiURL}/verificar-disponibilidad`;
    const request: VerificarDisponibilidadRequestDTO = { nombreSala: nombreSala };
    return this.http.post<VerificarDisponibilidadResponseDTO>(url, request);
  }

  getDashBoard(): Observable<DashBoardDTO> {  // Quitamos el [] para que retorne un solo objeto
    const searchUrl = `${this.apiURL}/dashboard`; 
    return this.http.get<DashBoardDTO>(searchUrl);
  }


}
