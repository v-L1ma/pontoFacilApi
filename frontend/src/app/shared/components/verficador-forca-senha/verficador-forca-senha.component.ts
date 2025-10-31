import { Component, effect, input, signal } from '@angular/core';
import { NgClass } from "@angular/common";

@Component({
  selector: 'app-verficador-forca-senha',
  imports: [
    NgClass
  ],
  templateUrl: './verficador-forca-senha.component.html',
  styleUrl: './verficador-forca-senha.component.scss'
})
export class VerficadorForcaSenhaComponent {
  forcaDaSenha = signal<string>("");
  senha = input.required<string>()

  constructor() {
    effect(() => {
      this.verificarForcaDaSenha(this.senha())
    });
  }

  verificarForcaDaSenha(senha:string){
    const especiais = /[!@#$%^&*(),.?":{}|<>_\-+=~[\]\\;'/`]/;
    const letrasMaiusculasEMinusculas = /[A-Z]/;
    const apenasMinusculas = /[a-z]/;
    const numeros = /[0-9]/;

    if( letrasMaiusculasEMinusculas.test(senha) && 
        apenasMinusculas.test(senha) &&
        numeros.test(senha) &&
        especiais.test(senha) && 
        senha.length>=5
    ){
      this.forcaDaSenha.set('forte');
      return;
    }

    if(letrasMaiusculasEMinusculas.test(senha) && apenasMinusculas.test(senha) ||
      letrasMaiusculasEMinusculas.test(senha) && numeros.test(senha) ||
      letrasMaiusculasEMinusculas.test(senha) && especiais.test(senha) ||
      apenasMinusculas.test(senha) && numeros.test(senha) ||
      apenasMinusculas.test(senha) && especiais.test(senha) ||
      numeros.test(senha) && especiais.test(senha) && senha.length>=3
    ){
      this.forcaDaSenha.set('media');
      return;
    }

    if(apenasMinusculas.test(senha) &&
        senha.length>=3){
      this.forcaDaSenha.set('fraca');
      return;
    }

    if(letrasMaiusculasEMinusculas.test(senha) &&
        senha.length>=3){
      this.forcaDaSenha.set('fraca');
      return;
    }

    if(especiais.test(senha) &&
        senha.length>=3){
      this.forcaDaSenha.set('fraca');
      return;
    }

    if(numeros.test(senha) &&
        senha.length>=3){
      this.forcaDaSenha.set('fraca');
      return;
    }
    
    this.forcaDaSenha.set('');
  }
}
