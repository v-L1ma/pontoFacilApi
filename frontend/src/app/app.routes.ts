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
    }
    ,
    {
        path:'cadastro',
        loadComponent: ()=> import('./pages/cadastro/cadastro.component').then((c)=>(c.CadastroComponent))
    },
    {
        path:'gerencia',
        loadComponent: ()=> import('./pages/gerenciar-colaboradores/gerenciar-colaboradores.component').then(
            (c)=>(c.GerenciarColaboradoresComponent)        
        ),
        canActivate: [authGuard]
    }
];
