import { Injectable } from '@angular/core';
import { TokenService } from '../token/token.service';
import { BehaviorSubject } from 'rxjs';
import { colaborador, usuario } from '../../types/types';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class usuarioLogadoService {

  private usuarioLogadoSubject = new BehaviorSubject<usuario | null>(null);

  constructor(private tokenService: TokenService){
    if(this.tokenService.possuiToken()){
        this.decodificarToken();
    }
  }

  decodificarToken(){
    const token = this.tokenService.retornarToken()!;
    let usuarioLogado:usuario;
    try {
      usuarioLogado = jwtDecode(token) as usuario;

      if(!this.tokenService.isTokenValido(usuarioLogado)){
        console.log("Token expirado")
        this.logout();
        return;
      }
    } catch (error) {
      console.log("Token inválido na localStorage");
      this.logout();
      return;
    }
    this.usuarioLogadoSubject.next(usuarioLogado);
  }

  retornarUser(){
    return this.usuarioLogadoSubject.asObservable();
  }

  salvarToken(token:string){
    this.tokenService.salvarToken(token);
    this.decodificarToken();
  }

  logout(){
    this.tokenService.exluirToken();
    this.usuarioLogadoSubject.next(null);
  }

  estaLogado(){
    return this.tokenService.possuiToken();
  }
}
