import { HttpInterceptorFn } from '@angular/common/http';
import { TokenService } from '../../services/token/token.service';
import { inject } from '@angular/core';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const tokenService = inject(TokenService)

  if(tokenService.possuiToken()){
    const token = tokenService.retornarToken();

    req = req.clone({
      setHeaders:{
        'Authorization':`Bearer ${token}`
      }
    })
  }
  return next(req);
};
