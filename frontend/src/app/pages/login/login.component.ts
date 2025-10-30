import { Component, OnInit, signal} from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { Router, RouterLink } from "@angular/router";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormBannerLayoutComponent } from '../../shared/components/form-banner-layout/form-banner-layout.component'; 
import { AuthService } from '../../shared/services/auth/auth.service'; 

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

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ){}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      senha: ['', [Validators.required]]
    });
  }

  enviar(){
    const {email, senha} = this.loginForm.value;
    this.authService.autenticar(email,senha).subscribe({
      next:(response)=>{
        // console.log(response);
        this.router.navigateByUrl('/portal')
      },
      error:(error)=>{
        // console.log(error);
      }
    });
  }

  esconder(){
    this.mostrarSenha.set(!this.mostrarSenha());
  }
}
