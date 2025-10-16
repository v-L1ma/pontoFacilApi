import {ChangeDetectionStrategy, Component, OnInit, signal} from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import { MatFormFieldControl } from '@angular/material/form-field';
import { RouterLink } from "@angular/router";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormBannerLayoutComponent } from "../../components/form-banner-layout/form-banner-layout.component";

@Component({
  selector: 'app-login',
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    RouterLink,
    ReactiveFormsModule,
    FormBannerLayoutComponent
],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit{
  mostrarSenha = signal(true);
  loginForm!: FormGroup;

  constructor(private fb: FormBuilder){}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      senha: ['', [Validators.required, Validators.minLength(1)]]
    });
  }

  enviar(){
    console.log(this.loginForm.value)
  }

  esconder(){
    this.mostrarSenha.set(!this.mostrarSenha());
  }
}
