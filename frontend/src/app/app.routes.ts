import { Routes } from '@angular/router';

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
        )
    }
];
