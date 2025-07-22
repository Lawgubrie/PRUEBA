export interface SalaCineDTO {
  id_Sala: number;
  nombre: string;
  estado?: number;
  tipo: string;
  activo: boolean;
}

export interface SalaCineCreateDTO {
  nombre: string;
  estado?: number;
  tipo: string;
}

export interface SalaCineUpdateDTO {
  id_Sala: number;
  nombre: string;
  estado?: number;
  tipo: string;
}
