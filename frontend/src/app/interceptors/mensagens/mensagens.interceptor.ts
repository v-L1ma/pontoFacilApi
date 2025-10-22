import { HttpErrorResponse, HttpEvent, HttpEventType, HttpHandlerFn, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { SnackBarService } from '../../services/snackBar/snack-bar.service';
import { inject } from '@angular/core';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { responseBase } from '../../types/types';
import { Router } from '@angular/router';


export function mensagensInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn): Observable<HttpEvent<unknown>> {
  const mensagemService = inject(SnackBarService);
  const router = inject(Router);

  return next(req).pipe(tap((event) => {
    if (event.type === HttpEventType.Response) {
      // console.log(req.url, 'returned a response with status', event.status);
      // console.log("body", body.message);
      const body = event.body as responseBase;

      if(body.message.includes("Colaboradores listados com sucesso!")){
        return;
      }

      mensagemService.mostrarMensagemSucesso(body.message)
    }
  }),
  catchError((erro:HttpErrorResponse)=>{
    if(erro.status == 0){
      mensagemService.mostrarMensagemErro("Erro de conexão. Verifique sua internet.")
    }

    if(erro.status == 401){
      mensagemService.mostrarMensagemErro("Usuário não autenticado. Por favor faça login novamente.")
      router.navigate(['/login'])
    }
      // console.log(req.url, 'returned a response with status', erro.status);
      // console.log("erro", erro.error.detail)
      mensagemService.mostrarMensagemErro(erro.error.detail)
      return throwError(()=>erro);
    })
);
}
