import { Routes } from "@angular/router";
import { DashBoard } from "./dash-board";
import { CrudSalas } from "../salas/salas";

export const dashboardRoutes: Routes = [
    {
        path:'',
        component: DashBoard, 
        children: [
            {
                path: 'salas', component:CrudSalas
            },
            
        ]
    }


]

export default dashboardRoutes;