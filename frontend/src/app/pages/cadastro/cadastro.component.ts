import {ChangeDetectionStrategy, Component, OnInit, signal} from '@angular/core';
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
    MatSelectModule
  ],
  templateUrl: './cadastro.component.html',
  styleUrl: './cadastro.component.scss'
})
export class CadastroComponent {
  mostrarSenha = signal(true);
  cadastroForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService
  ){}

  ngOnInit(): void {
    this.cadastroForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(5)]],
      email: ['', [Validators.required, Validators.email]],
      senha: ['', [Validators.required, Validators.minLength(5)]],
      confirmarSenha: ['', [Validators.required]]
    }, { validators:compararSenhaValidator('senha','confirmarSenha')});
  }
  

  enviar(){
    const cadastrarUsuarioDTO:cadastrarUsuarioDTO = {
      username: this.cadastroForm.value.nome,
      email: this.cadastroForm.value.email,
      password: this.cadastroForm.value.senha,
      rePassword: this.cadastroForm.value.confirmarSenha
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

  esconder(){
    this.mostrarSenha.set(!this.mostrarSenha());
  }
}
