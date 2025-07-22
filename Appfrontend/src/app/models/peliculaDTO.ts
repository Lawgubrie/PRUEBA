export interface PeliculaDTO {
  id_Pelicula: number;
  nombre: string;
  duracion: number;
  imagen?: string; 
  activo: boolean;
}

export interface PeliculaCreateDTO {
  nombre: string;
  duracion: number;
  imagen?: string;
}

export interface PeliculaUpdateDTO {
  id_Pelicula: number;
  nombre: string;
  duracion: number;
  imagen?: string;
}