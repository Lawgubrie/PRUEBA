import { Routes } from '@angular/router';
import { Login } from './components/login/login';
import { CrudSalas } from './components/salas/salas';
import { Peliculas } from './components/peliculas/peliculas';
import { PeliculaSalaCine } from './components/pelicula-sala-cine/pelicula-sala-cine';
import { PeliculaList } from './components/pelicula-list/pelicula-list';


export const routes: Routes = [

    {
        path:'login', 
        component: Login
    },
    {
        path: 'dashboard',
        loadChildren:()=> import('./components/dash-board/dash-board.routes')
    },
    {
        path: 'salas',
        component: CrudSalas
    },
    {
        path: 'peliculas',
        component: Peliculas
    },
    {
        path: 'asignacion',
        component: PeliculaSalaCine
    },
    {
        path:'peliculalist',
        component: PeliculaList
    },
    {
        path: '**',
        redirectTo: 'login'
    }

];

