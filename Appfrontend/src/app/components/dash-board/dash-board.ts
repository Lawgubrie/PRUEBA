import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SalasService } from '../../services/salas-service';
import { DashBoardDTO } from '../../models/dashBoardDTO';
import { PeliculasService } from '../../services/peliculas-service';
import { PeliculaDTO } from '../../models/peliculaDTO';

@Component({
  selector: 'app-dash-board',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dash-board.html',
  styleUrl: './dash-board.css'
})
export class DashBoard implements OnInit {

  public dashboardData: DashBoardDTO | null = null;
  public isLoading: boolean = true;
  public errorMessage: string | null = null;

  constructor(
    private peliculaS: PeliculasService, private salas:SalasService
  ){}

  ngOnInit(): void {
    this.obtenerDash();
  }

  obtenerPeliculas(){
    this.peliculaS.getPeliculas().subscribe((data:PeliculaDTO[])=>console.log(data))
  }

  obtenerDash() {
  this.isLoading = true;
  this.errorMessage = null;
  
  this.salas.getDashBoard().subscribe({
    next: (data: DashBoardDTO) => {
      this.dashboardData = data;
      this.isLoading = false;
    },
    error: (err) => {
      this.errorMessage = 'Error al cargar los datos del dashboard';
      this.isLoading = false;
      console.error('Error en dashboard:', err);
    }
  });
}

}