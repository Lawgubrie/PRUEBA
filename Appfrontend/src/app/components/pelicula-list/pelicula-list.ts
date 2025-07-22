import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatCardModule } from '@angular/material/card';
import { PeliculaDTO } from '../../models/peliculaDTO';
import { PeliculaSalaDetalleDTO } from '../../models/peliculaSalaCineDTO';
import { PeliculasService } from '../../services/peliculas-service';
import { PeliculaSalaCineService } from '../../services/pelicula-sala-cine-service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-pelicula-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatProgressSpinnerModule,
    MatCardModule
  ],
  templateUrl: './pelicula-list.html',
  styleUrls: ['./pelicula-list.css']
})
export class PeliculaList implements OnInit {

  public terminoBusqueda: string = '';
  public fechaFiltro: string = '';

  public peliculasEncontradas: PeliculaDTO[] = [];
  public proyeccionesPorFecha: PeliculaSalaDetalleDTO[] = [];

  public isLoading: boolean = false;
  public mensajeError: string | null = null;
  public vistaActiva: 'busqueda' | 'fecha' | 'inicial' = 'inicial';

  constructor(
    private peliculasService: PeliculasService,
    private peliculaSalaCineService: PeliculaSalaCineService
  ) { }

  ngOnInit(): void {
    this.cargarPeliculasIniciales();
  }

  cargarPeliculasIniciales() {
    this.limpiarEstado();
    this.isLoading = true;
    this.vistaActiva = 'inicial';

    this.peliculasService.getPeliculas().subscribe({
      next: (peliculas) => {
        this.peliculasEncontradas = peliculas;
        this.isLoading = false;
      },
      error: (err) => this.handleError(err, 'Error al cargar las películas.')
    });
  }

  buscarPorNombre() {
    if (!this.terminoBusqueda.trim()) {
      this.cargarPeliculasIniciales(); 
      return;
    }

    this.limpiarEstado();
    this.isLoading = true;
    this.vistaActiva = 'busqueda';

    this.peliculasService.buscarPeliculaPorNombre(this.terminoBusqueda).subscribe({
      next: (resultados) => {
        this.peliculasEncontradas = resultados;
        this.isLoading = false;
        if (resultados.length === 0) {
            this.mensajeError = `No se encontraron películas con el nombre "${this.terminoBusqueda}".`
        }
      },
      error: (err) => this.handleError(err, `No se encontraron películas con el nombre "${this.terminoBusqueda}".`)
    });
  }

  filtrarPorFecha() {
    if (!this.fechaFiltro) {
        return; 
    }

    this.limpiarEstado();
    this.isLoading = true;
    this.vistaActiva = 'fecha';

    this.peliculaSalaCineService.getAsignacionesPorFecha(this.fechaFiltro).subscribe({
      next: (resultados) => {
        this.proyeccionesPorFecha = resultados;
        this.isLoading = false;
        if (resultados.length === 0) {
            this.mensajeError = `No se encontraron proyecciones para la fecha ${this.fechaFiltro}.`
        }
      },
      error: (err) => this.handleError(err, `No se encontraron proyecciones para la fecha ${this.fechaFiltro}.`)
    });
  }



  private limpiarEstado() {

    this.mensajeError = null;
    this.peliculasEncontradas = [];
    this.proyeccionesPorFecha = [];
    

    if (this.vistaActiva === 'fecha') {
        this.terminoBusqueda = '';
    } else if (this.vistaActiva === 'busqueda') {
        this.fechaFiltro = '';
    }
  }

  private handleError(error: HttpErrorResponse, mensaje404: string) {
    this.isLoading = false;
    if (error.status === 404) {
      this.mensajeError = mensaje404;
    } else {
      this.mensajeError = 'Ocurrió un error inesperado. Por favor, inténtalo de nuevo más tarde.';
    }
    console.error(error);
  }
}