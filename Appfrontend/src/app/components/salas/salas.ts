import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { SalaCineCreateDTO, SalaCineDTO, SalaCineUpdateDTO } from '../../models/salaCineDTO';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { SalasService } from '../../services/salas-service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-crud-salas',
  standalone: true, 
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSelectModule,
    MatIconModule
  ],
  templateUrl: './salas.html', 
  styleUrls: ['./salas.css'] 
})
export class CrudSalas implements OnInit, AfterViewInit {
  title: string = "Gestión de Salas de Cine";
  form!: FormGroup;
  isEditMode: boolean = false;
  currentId!: number;
  dataSource = new MatTableDataSource<SalaCineDTO>();
  displayedColumns: string[] = ['nombre', 'tipo', 'activo', 'actions'];

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private salasService: SalasService,
    private fb: FormBuilder,
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.loadSalas();
  }
  
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }

  initializeForm() {
    this.form = this.fb.group({
      nombre: ['', [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(50),
        Validators.pattern(/^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]+$/)
      ]],
      tipo: ['', [Validators.required]]
    });
  }

  loadSalas() {
    this.salasService.getSalas().subscribe({
      next: (data) => {
        this.dataSource.data = data;
      },
      error: (err) => console.error("Error al cargar las salas:", err)
    });
  }

  onSubmit() {
    if (this.form.invalid) {
      return;
    }

    if (this.isEditMode) {
      const salaUpdateData: SalaCineUpdateDTO = {
        id_Sala: this.currentId,
        nombre: this.form.value.nombre,
        tipo: this.form.value.tipo
      };
      this.salasService.updateSala(salaUpdateData).subscribe({
        next: () => {
          alert("Sala actualizada exitosamente");
          this.loadSalas();
          this.clearForm();
        },
        error: (err) => console.error('Error al actualizar:', err)
      });
    } else {
      const salaCreateData: SalaCineCreateDTO = {
        nombre: this.form.value.nombre,
        tipo: this.form.value.tipo,
      };
      this.salasService.createSala(salaCreateData).subscribe({
        next: () => {
          alert("Sala creada exitosamente");
          this.loadSalas();
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
  
  editar(sala: SalaCineDTO) {
    this.isEditMode = true;
    this.currentId = sala.id_Sala;
    this.form.patchValue({
      nombre: sala.nombre,
      tipo: sala.tipo,
    });
    window.scrollTo(0, 0); 
  }
  
  eliminar(sala: SalaCineDTO) {

    const mensajeConfirmacion = `¿Estás seguro de ELIMINAR PERMANENTEMENTE la sala "${sala.nombre}"? Esta acción no se puede deshacer.`;

    if (window.confirm(mensajeConfirmacion)) {

      this.salasService.deleteSala(sala.id_Sala).subscribe({
        next: () => {
          alert("Sala eliminada permanentemente.");
          this.loadSalas(); 
        },
        error: (err) => {
          console.error('Error al eliminar permanentemente:', err);
          alert('Ocurrió un error al intentar eliminar la sala.'); 
        }
      });
    }

  }
}