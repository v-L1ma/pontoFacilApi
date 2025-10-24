import {ChangeDetectionStrategy, Component, effect, OnInit, signal} from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import { RouterLink } from "@angular/router";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormBannerLayoutComponent } from "../../components/form-banner-layout/form-banner-layout.component";
import { compararSenhaValidator } from '../../validators/compararSenha.validator';
import { AuthService } from '../../services/auth/auth.service';
import { cadastrarColaboradorDTO, cadastrarUsuarioDTO } from '../../types/types';
import { NgClass } from "@angular/common";


@Component({
  selector: 'app-cadastro',
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    RouterLink,
    ReactiveFormsModule,
    FormBannerLayoutComponent,
    MatSelectModule,
    NgClass
],
  templateUrl: './cadastro.component.html',
  styleUrl: './cadastro.component.scss'
})
export class CadastroComponent {
  mostrarSenha = signal(true);
  cadastroForm!: FormGroup;
  forcaDaSenha = signal<string>("");
  private passwordRegex: string = '^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*\\-_])[A-Za-z\\d!@#$%^&*\\-_]{8,}$';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService
  ){
    
  }

  ngOnInit(): void {
    this.cadastroForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(3), Validators.pattern(/^[\p{L}\s'-]+$/u)]],
      email: ['', [Validators.required, Validators.email]],
      senha: ['', [Validators.required, Validators.minLength(3), Validators.pattern(this.passwordRegex)]],
      confirmarSenha: ['', [Validators.required]]
    }, { validators:compararSenhaValidator('senha','confirmarSenha')});

    this.cadastroForm.get('senha')?.valueChanges.subscribe((senha)=>{
      this.verificarForcaDaSenha(senha)
    })
  }

  
  enviar(){
    const cadastrarUsuarioDTO:cadastrarUsuarioDTO = {
      Username: this.cadastroForm.value.nome,
      Email: this.cadastroForm.value.email,
      Password: this.cadastroForm.value.senha,
      RePassword: this.cadastroForm.value.confirmarSenha
    }

    this.authService.cadastrar(cadastrarUsuarioDTO).subscribe({
      next:(response)=>{
        console.log(response);
      },
      error:(error)=>{
        console.log(error);
      },
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
        senha.length>=3
    ){
      this.forcaDaSenha.set('forte');
      return;
    }
    
    if(letrasMaiusculasEMinusculas.test(senha) && 
        apenasMinusculas.test(senha) &&
        senha.length>=3){
      this.forcaDaSenha.set('media');
      return;
    }
    
    if(apenasMinusculas.test(senha) &&
        senha.length>=3){
      this.forcaDaSenha.set('fraca');
      return;
    }
    
    this.forcaDaSenha.set('');
    
  }

  esconder(){
    this.mostrarSenha.set(!this.mostrarSenha());
  }
}
