import {ChangeDetectionStrategy, Component, effect, OnInit, signal} from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import { Router, RouterLink } from "@angular/router";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormBannerLayoutComponent } from '../../shared/components/form-banner-layout/form-banner-layout.component'; 
import { compararSenhaValidator } from '../../shared/validators/compararSenha.validator';
import { AuthService } from '../../shared/services/auth/auth.service'; 
import { cadastrarUsuarioDTO } from '../../shared/types/types';
import { VerficadorForcaSenhaComponent } from '../../shared/components/verficador-forca-senha/verficador-forca-senha.component';
import { LoadingComponent } from "../../shared/components/loading/loading/loading.component"; 


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
    VerficadorForcaSenhaComponent,
    LoadingComponent
],
  templateUrl: './cadastro.component.html',
  styleUrl: './cadastro.component.scss'
})
export class CadastroComponent {
  mostrarSenha = signal(true);
  mostrarConfirmarSenha = signal(true);
  cadastroForm!: FormGroup;
  private passwordRegex: string = '^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*\\-_])[A-Za-z\\d!@#$%^&*\\-_]{8,}$';
  senha=signal<string>('');
  isLoading = signal<boolean>(false);


  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
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
      this.senha.set(senha)
    })
  }

  
  enviar(){
    const cadastrarUsuarioDTO:cadastrarUsuarioDTO = {
      Username: this.cadastroForm.value.nome,
      Email: this.cadastroForm.value.email,
      Password: this.cadastroForm.value.senha,
      RePassword: this.cadastroForm.value.confirmarSenha
    }

    this.isLoading.set(true);

    this.authService.cadastrar(cadastrarUsuarioDTO).subscribe({
      next:(response)=>{
        // console.log(response);
        this.isLoading.set(false);
        setTimeout(()=>{
          this.router.navigate(['login'])
        },200)
      },
      error:(error)=>{
        // console.log(error);
        this.isLoading.set(false);
      },
    });

  }

  esconder(input:string){
    if(input==="mostrarConfirmarSenha"){      
      this.mostrarConfirmarSenha.set(!this.mostrarConfirmarSenha());
    }
    if(input==="mostrarSenha"){
      this.mostrarSenha.set(!this.mostrarSenha());
    }
  }
}
