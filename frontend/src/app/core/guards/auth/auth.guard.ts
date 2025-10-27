import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { usuarioLogadoService } from '../../../shared/services/usuario-logado/usuario-logado.service'; 

export const authGuard: CanActivateFn = (route, state) => {
  const usuarioService = inject(usuarioLogadoService);
  const router = inject(Router);

  if(usuarioService.estaLogado()){
    return true;
  } else{
    router.navigate(['/login']);
    return false;
  }
};
