import { Routes } from '@angular/router';
import { authGuard } from './guards/auth/auth.guard';

export const routes: Routes = [
    {
        path:'',
        pathMatch:'full',
        redirectTo:'login'
    },
    {
        path:'login',
        loadComponent: ()=> import('./pages/login/login.component').then((c)=>(c.LoginComponent))
    },
    {
        path:'cadastro',
        loadComponent: ()=> import('./pages/cadastro/cadastro.component').then((c)=>(c.CadastroComponent))
    },
    {
        path:'portal',
        redirectTo:'portal/gerencia',
        pathMatch:"full"
    },
    {
        path:'portal',
        loadComponent: ()=> import('./pages/portal/portal.component').then((c)=>(c.PortalComponent)),
        canActivate: [authGuard],
        children:[
            {
                path:'gerencia',
                loadComponent: ()=> import('./pages/gerenciar-colaboradores/gerenciar-colaboradores.component').then((c)=>(c.GerenciarColaboradoresComponent)),
                canActivate: [authGuard]
            },
            {
                path:'perfil',
                loadComponent: ()=> import('./pages/perfil/perfil.component').then((c)=>(c.PerfilComponent)),
                canActivate: [authGuard]
            },
            {
                path:'dashboard',
                loadComponent: ()=> import('./pages/dashboard/dashboard.component').then((c)=>(c.DashboardComponent)),
                canActivate: [authGuard]
            }
        ]
    },
    {
        path:"**",
        redirectTo:'login'
    }
];
