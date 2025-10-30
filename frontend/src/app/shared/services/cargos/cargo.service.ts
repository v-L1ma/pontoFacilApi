import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment'; 
import { responseBase } from '../../types/types';

@Injectable({
  providedIn: 'root'
})
export class CargoService {
  constructor(
    private http:HttpClient
  ){}

  buscarPaginado(pageSize:number,pageNumber:number):Observable<responseBase>{
    return this.http.get<responseBase>(`${environment.API_URL}/Cargos?pageSize=${pageSize}&pageNumber=${pageNumber}`)
  }

  buscarPorSetor(setorId:number):Observable<responseBase>{
    return this.http.get<responseBase>(`${environment.API_URL}/Cargos/setor/${setorId}`)
  }

  editar(id:number,cargo:any){
    return this.http.put<responseBase>(`${environment.API_URL}/Cargos/${id}`, cargo);
  }

  excluir(id:number):Observable<responseBase>{
    return this.http.delete<responseBase>(`${environment.API_URL}/Cargos/${id}`);
  }

  cadastrar(cargo:any){
    return this.http.post<responseBase>(`${environment.API_URL}/Cargos`, cargo);
  }
}