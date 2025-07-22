
export interface PeliculaSalaCineDTO {
  idPeliculaSala: number;
  idPelicula: number;
  idSalaCine: number;
  fechaPublicacion: string;
  fechaFin: string;
}

export interface PeliculaSalaCineCreateDTO {
  idPelicula: number;
  idSalaCine: number;
  fechaPublicacion: string;
  fechaFin: string;
}

export interface PeliculaSalaDetalleDTO {
  nombrePelicula: string;
  nombreSala: string;
  tipoSala: string;
  fechaPublicacion: string;
}
