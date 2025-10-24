import { Injectable } from '@angular/core';
import { TokenService } from '../token/token.service';
import { BehaviorSubject, Observable } from 'rxjs';
import { alterarSenhaDto, colaborador, editarPerfilUsuarioDto, responseBase, usuario } from '../../types/types';
import { jwtDecode } from 'jwt-decode';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class usuarioLogadoService {

  private usuarioLogadoSubject = new BehaviorSubject<usuario | null>(null);

  constructor(
    private tokenService: TokenService,
    private http:HttpClient
  ){
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

  editarPerfil(dados: editarPerfilUsuarioDto):void{
    let usuario = this.usuarioLogadoSubject.value;
    usuario!.Username=dados.nome;
    usuario!.Email=dados.email; 
    this.http.put<responseBase>(`${environment.API_URL}/Usuarios`, dados).subscribe({
      next:(response)=>{
        this.usuarioLogadoSubject.next(usuario);
      },
      error:(erro)=>{
      }
    })
  }

  alterarSenha(dto:alterarSenhaDto):Observable<responseBase>{
    return this.http.put<responseBase>(`${environment.API_URL}/Usuarios/senha`,dto)
  }

  excluirConta(){
    return this.http.delete<responseBase>(`${environment.API_URL}/Usuarios`)
  }
}
