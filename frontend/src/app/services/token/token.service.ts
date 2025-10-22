import { Injectable } from '@angular/core';
import { usuario } from '../../types/types';

const KEY = 'token'

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  salvarToken(token:string){
    return localStorage.setItem(KEY, token);
  }

  exluirToken(){
    localStorage.removeItem(KEY);
  }

  retornarToken(){
    return localStorage.getItem(KEY);
  }

  possuiToken():boolean{
    return !!this.retornarToken()
  }

  isTokenValido(token:usuario):boolean{
    // console.log("EXPIROU OU NÃO ", token.exp * 1000 > Date.now())
    return token.exp * 1000 > Date.now()
  }
}
