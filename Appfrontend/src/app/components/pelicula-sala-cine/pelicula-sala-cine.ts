import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; 
import { PeliculaSalaCineService } from '../../services/pelicula-sala-cine-service';
import { PeliculaSalaCineCreateDTO, PeliculaSalaCineDTO } from '../../models/peliculaSalaCineDTO';


interface FormularioAsignacion extends PeliculaSalaCineCreateDTO {
  idPeliculaSala?: number; 
}

@Component({
  selector: 'app-pelicula-sala-cine',
  standalone: true,
  imports: [CommonModule, FormsModule], 
  templateUrl: './pelicula-sala-cine.html',
  styleUrls: ['./pelicula-sala-cine.css']
})
export class PeliculaSalaCine implements OnInit {

  public asignaciones: PeliculaSalaCineDTO[] = [];
  public isLoading = true;
  public error: string | null = null;

  public esModoEdicion = false;
  public formularioAsignacion: FormularioAsignacion = this.inicializarFormulario();

  constructor(private asignacionService: PeliculaSalaCineService) {}

  ngOnInit(): void {
    this.cargarAsignaciones();
  }

  inicializarFormulario(): FormularioAsignacion {
    return {
      idPelicula: 0,
      idSalaCine: 0,
      fechaPublicacion: '',
      fechaFin: ''
    };
  }

  cargarAsignaciones(): void {
    this.isLoading = true;
    this.error = null;
    this.asignacionService.getPeliculasSalaCine().subscribe({
      next: (data) => {
        this.asignaciones = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.error = 'Error al cargar las asignaciones.';
        console.error(err);
        this.isLoading = false;
      }
    });
  }

  seleccionarParaEditar(asignacion: PeliculaSalaCineDTO): void {
    this.esModoEdicion = true;
    this.formularioAsignacion = {
      idPeliculaSala: asignacion.idPeliculaSala,
      idPelicula: asignacion.idPelicula,
      idSalaCine: asignacion.idSalaCine,
      fechaPublicacion: asignacion.fechaPublicacion.split('T')[0],
      fechaFin: asignacion.fechaFin.split('T')[0]
    };
  }

  guardarCambios(): void {
    if (this.esModoEdicion) {
      this.actualizarAsignacion();
    } else {
      this.crearAsignacion();
    }
  }
  
  private crearAsignacion(): void {
    const nuevaAsignacion: PeliculaSalaCineCreateDTO = {
      idPelicula: this.formularioAsignacion.idPelicula,
      idSalaCine: this.formularioAsignacion.idSalaCine,
      fechaPublicacion: this.formularioAsignacion.fechaPublicacion,
      fechaFin: this.formularioAsignacion.fechaFin
    };

    this.asignacionService.createAsignacion(nuevaAsignacion).subscribe({
      next: () => {
        console.log('Asignación creada con éxito');
        this.cargarAsignaciones();
        this.cancelarEdicion(); 
      },
      error: (err) => {
        this.error = 'Error al crear la asignación.';
        console.error(err);
      }
    });
  }

  private actualizarAsignacion(): void {
    if (!this.formularioAsignacion.idPeliculaSala) return;

    const asignacionActualizada: PeliculaSalaCineDTO = {
      idPeliculaSala: this.formularioAsignacion.idPeliculaSala,
      idPelicula: this.formularioAsignacion.idPelicula,
      idSalaCine: this.formularioAsignacion.idSalaCine,
      fechaPublicacion: this.formularioAsignacion.fechaPublicacion,
      fechaFin: this.formularioAsignacion.fechaFin
    };

    this.asignacionService.updateAsignacion(asignacionActualizada).subscribe({
      next: () => {
        console.log('Asignación actualizada con éxito');
        this.cargarAsignaciones();
        this.cancelarEdicion(); 
      },
      error: (err) => {
        this.error = 'Error al actualizar la asignación.';
        console.error(err);
      }
    });
  }

  cancelarEdicion(): void {
    this.esModoEdicion = false;
    this.formularioAsignacion = this.inicializarFormulario();
  }

  eliminarAsignacion(id: number): void {
    if (confirm(`¿Estás seguro de que deseas eliminar la asignación con ID ${id}?`)) {
      this.asignacionService.deleteAsignacion(id).subscribe({
        next: () => {
          console.log(`Asignación con ID ${id} eliminada.`);
          this.cargarAsignaciones();
        },
        error: (err) => {
          this.error = 'No se pudo eliminar la asignación.';
          console.error(err);
        }
      });
    }
  }
}