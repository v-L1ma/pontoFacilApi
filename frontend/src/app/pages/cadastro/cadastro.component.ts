import {ChangeDetectionStrategy, Component, OnInit, signal} from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import { MatFormFieldControl } from '@angular/material/form-field';
import { RouterLink } from "@angular/router";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormBannerLayoutComponent } from "../../components/form-banner-layout/form-banner-layout.component";
import { compararSenhaValidator } from '../../validators/compararSenha.validator';


@Component({
  selector: 'app-cadastro',
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    RouterLink,
    ReactiveFormsModule,
    FormBannerLayoutComponent
  ],
  templateUrl: './cadastro.component.html',
  styleUrl: './cadastro.component.scss'
})
export class CadastroComponent {
  mostrarSenha = signal(true);
  cadastroForm!: FormGroup;

  constructor(private fb: FormBuilder){}

  ngOnInit(): void {
    this.cadastroForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(5)]],
      email: ['', [Validators.required, Validators.email]],
      senha: ['', [Validators.required, Validators.minLength(5)]],
      confirmarSenha: ['', [Validators.required]]
    }, { validators: compararSenhaValidator('senha','confirmarSenha')});
  }

  enviar(){
    console.log(this.cadastroForm.value)
  }

  esconder(){
    this.mostrarSenha.set(!this.mostrarSenha());
  }
}
