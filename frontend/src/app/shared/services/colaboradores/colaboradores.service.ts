import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment.development'; 
import { cadastrarColaboradorDTO, colaborador, responseBase } from '../../types/types';

@Injectable({
  providedIn: 'root'
})
export class ColaboradoresService {
  constructor(
    private http:HttpClient
  ){}

  buscarColaboradores(pageSize:number, pageNumber:number):Observable<responseBase>{
    return this.http.get<responseBase>(`${environment.API_URL}/Colaborador/buscarColaboradoresPaginado?pageSize=${pageSize}&pageNumber=${pageNumber}`)
  }

  editarColaborador(id:string,colaborador:cadastrarColaboradorDTO){
    return this.http.put<responseBase>(`${environment.API_URL}/Colaborador/${id}`, colaborador);
  }

  excluirColaborador(id:string):Observable<responseBase>{
    return this.http.delete<responseBase>(`${environment.API_URL}/Colaborador/${id}`);
  }

  cadastrar(colaborador:cadastrarColaboradorDTO){
    return this.http.post<responseBase>(`${environment.API_URL}/Colaborador`, colaborador);
  }

  estatisticas():Observable<responseBase>{
    return this.http.get<responseBase>(`${environment.API_URL}/Colaborador/estatisticas`);
  }
}
