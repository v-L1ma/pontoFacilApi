import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../../environments/environment'; 
import { cadastrarUsuarioDTO } from '../../types/types'; 
import { usuarioLogadoService } from '../usuario-logado/usuario-logado.service';

interface authResponse{
  dados:string;
  message:string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(
    private http: HttpClient,
    private usuarioLogadoService:usuarioLogadoService
  ){}

  autenticar(email:string, password:string): Observable<HttpResponse<authResponse>>{
    return this.http.post<authResponse>(`${environment.API_URL}/Autenticacao/login`,
      {email,password},
      {observe: 'response'}).pipe(
        tap((response)=>{
          const authToken = response.body?.dados || '';
          this.usuarioLogadoService.salvarToken(authToken);
        })
      )
  }

  cadastrar(dto: cadastrarUsuarioDTO): Observable<any>{
    return this.http.post<any>(`${environment.API_URL}/Autenticacao/cadastro`,dto);
  }

}
