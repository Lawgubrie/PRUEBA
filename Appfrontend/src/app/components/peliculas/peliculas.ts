import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule, MatDatepicker } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

import { PeliculaCreateDTO, PeliculaDTO, PeliculaUpdateDTO } from '../../models/peliculaDTO';
import { PeliculasService } from '../../services/peliculas-service';

@Component({
  selector: 'app-crud-peliculas',
  standalone: true,
  imports: [
    CommonModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule,
    MatButtonModule, MatTableModule, MatPaginatorModule, MatIconModule, MatSelectModule,
    MatDatepickerModule, MatNativeDateModule
  ],
  templateUrl: './peliculas.html',
  styleUrls: ['./peliculas.css']
})
export class Peliculas implements OnInit, AfterViewInit {
  title: string = "Gestión de Películas";
  form!: FormGroup;
  isEditMode: boolean = false;
  currentId!: number;
  dataSource = new MatTableDataSource<PeliculaDTO>();
  
  // Columnas que se mostrarán en la tabla
  displayedColumns: string[] = ['imagen', 'nombre', 'duracion', 'activo', 'actions'];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild('picker1') picker1!: MatDatepicker<Date>;
  @ViewChild('picker2') picker2!: MatDatepicker<Date>;

  constructor(
    private peliculasService: PeliculasService,
    private fb: FormBuilder,
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.loadPeliculas();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }

  initializeForm() {
    this.form = this.fb.group({
      nombre: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
      duracion: ['', [Validators.required, Validators.min(1), Validators.max(999), Validators.pattern("^[0-9]*$")]],
      imagen: ['', [Validators.pattern(/^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$/i)]], // Valida que sea una URL
      fechaPublicacion: ['', Validators.required],
      fechaFin: ['', Validators.required]
    });
  }

  loadPeliculas() {
    this.peliculasService.getPeliculas().subscribe({
      next: (data) => this.dataSource.data = data,
      error: (err) => console.error("Error al cargar películas:", err)
    });
  }

  onSubmit() {
    if (this.form.invalid) return;

    if (this.isEditMode) {
      const peliculaUpdateData: PeliculaUpdateDTO = {
        id_Pelicula: this.currentId,
        nombre: this.form.value.nombre,
        duracion: Number(this.form.value.duracion),
        imagen: this.form.value.imagen
      };
      this.peliculasService.updatePelicula(peliculaUpdateData).subscribe({
        next: () => {
          alert("Película actualizada exitosamente");
          this.loadPeliculas();
          this.clearForm();
        },
        error: (err) => console.error('Error al actualizar:', err)
      });
    } else {
      const peliculaCreateData: PeliculaCreateDTO = {
        nombre: this.form.value.nombre,
        duracion: Number(this.form.value.duracion),
        imagen: this.form.value.imagen
      };
      this.peliculasService.createPelicula(peliculaCreateData).subscribe({
        next: () => {
          alert("Película creada exitosamente");
          this.loadPeliculas();
          this.clearForm();
        },
        error: (err) => console.error('Error al crear:', err)
      });
    }
  }

  clearForm() {
    this.form.reset();
    this.isEditMode = false;
    this.currentId = 0;
  }
  
  editar(pelicula: PeliculaDTO) {
    this.isEditMode = true;
    this.currentId = pelicula.id_Pelicula;
    this.form.patchValue({
      nombre: pelicula.nombre,
      duracion: pelicula.duracion,
      imagen: pelicula.imagen
    });
    window.scrollTo(0, 0);
  }
  
  toggleActivo(pelicula: PeliculaDTO) {
    const accion = pelicula.activo ? 'Desactivar' : 'Activar';
    if (window.confirm(`¿Estás seguro de ${accion.toLowerCase()} la película "${pelicula.nombre}"?`)) {
      const peliculaUpdateData: PeliculaUpdateDTO & { activo: boolean } = {
          ...pelicula,
          activo: !pelicula.activo
      };
      this.peliculasService.updatePelicula(peliculaUpdateData).subscribe({
          next: () => {
              alert(`Película ${accion.toLowerCase()}da exitosamente.`);
              this.loadPeliculas();
          },
          error: (err) => console.error(`Error al ${accion.toLowerCase()}:`, err)
      });
    }
  }
  
  eliminar(pelicula: PeliculaDTO) {
    const mensaje = `¿Estás seguro de ELIMINAR PERMANENTEMENTE la película "${pelicula.nombre}"? Esta acción no se puede deshacer.`;
    if (window.confirm(mensaje)) {
      this.peliculasService.deletePelicula(pelicula.id_Pelicula).subscribe({
        next: () => {
          alert("Película eliminada permanentemente.");
          this.loadPeliculas();
        },
        error: (err) => console.error('Error al eliminar permanentemente:', err)
      });
    }
  }
}