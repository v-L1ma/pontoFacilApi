import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class SnackBarService {
  constructor(private snackBar: MatSnackBar){}

  mostrarMensagemSucesso(mensagem:string){
    const config: MatSnackBarConfig = {
      duration:3000,
      horizontalPosition:'right',
      verticalPosition:'top',
      panelClass: ['aviso__sucesso']
    }
    this.snackBar.open(mensagem, 'Fechar', config);
  }

  mostrarMensagemErro(mensagem:string){
    const config: MatSnackBarConfig = {
      duration:3000,
      horizontalPosition:'right',
      verticalPosition:'top',
      panelClass:['aviso__erro']
    }
    this.snackBar.open(mensagem, 'Fechar', config);
  }
}
