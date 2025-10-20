import { Injectable } from '@angular/core';
import { TokenService } from '../token/token.service';
import { BehaviorSubject } from 'rxjs';
import { colaborador } from '../../types/types';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class usuarioLogadoService {

  private usuarioLogadoSubject = new BehaviorSubject<colaborador | null>(null);

  constructor(private tokenService: TokenService){
    if(this.tokenService.possuiToken()){
      
    }
  }

  decodificarToken(){
    const token = this.tokenService.retornarToken()!;
    const usuarioLogado = jwtDecode(token) as colaborador;
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
