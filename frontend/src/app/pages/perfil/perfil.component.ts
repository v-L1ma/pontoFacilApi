import { Component, effect, signal } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, NgModel, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import { compararSenhaValidator } from '../../validators/compararSenha.validator';
import { usuarioLogadoService } from '../../services/usuario-logado/usuario-logado.service';
import { usuario } from '../../types/types';
import { AuthService } from '../../services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-perfil',
  imports: [
    MatFormFieldModule,
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatSlideToggleModule,
    FormsModule
  ],
  templateUrl: './perfil.component.html',
  styleUrl: './perfil.component.scss'
})
export class PerfilComponent {
  cadastroForm!: FormGroup;
  senhaForm!: FormGroup;
  isEditando = signal(false)
  isEditandoSenha = false
  usuarioLogado!:usuario;

  constructor(
    private fb: FormBuilder,
    private usuarioLogadoService: usuarioLogadoService,
    private authService: AuthService,
    private router: Router
  ){
     effect(() => {
        if (this.isEditando()) {
          this.cadastroForm.get('nome')?.enable();
          this.cadastroForm.get('email')?.enable();
        } else {
          this.cadastroForm.get('nome')?.disable();
          this.cadastroForm.get('email')?.disable();
        }
      });
  }

  ngOnInit(): void {
    this.usuarioLogadoService.retornarUser().subscribe(usuario=>{
      this.usuarioLogado = usuario!;
    })

    this.cadastroForm = this.fb.group({
      nome: [this.usuarioLogado.Username, [Validators.required, Validators.minLength(3), Validators.pattern(/^[\p{L}\s'-]+$/u)]],
      email: [this.usuarioLogado.Email, [Validators.required, Validators.email]],
    });

    this.senhaForm = this.fb.group({
      senhaAtual: ['', [Validators.required, Validators.minLength(3)]],
      senhaNova: ['', [Validators.required, Validators.minLength(3)]],
      confirmarSenhaNova: ['', [Validators.required]]
    }, { validators:compararSenhaValidator('senhaNova','confirmarSenha')});
  }

  salvarEdicao(){
    this.usuarioLogadoService.editarPerfil(this.cadastroForm.value)
  }

  alterarSenha(){
    this.authService.alterarSenha(this.senhaForm.value).subscribe({
      next:(response)=>{
        setTimeout(()=>{
          this.limparFormulario();
          this.isEditandoSenha=false;
        },1000)
      },
      error:(error)=>{
        console.log(error)
      }
    })
  }

  editar(){
    this.isEditando.set(true);
    this.isEditandoSenha = false;
  }

  excluirConta(){
    this.usuarioLogadoService.excluirConta().subscribe({
      next:(response)=>{
        this.usuarioLogadoService.logout();
        this.router.navigate(['login']);
      },
      error:(erro)=>{
      }
    })
  }

  cancelar(){
    this.limparFormulario();
    this.isEditando.set(false)
  }

  limparFormulario(){
    this.cadastroForm.get("nome")?.setValue(this.usuarioLogado.Username);
    this.cadastroForm.get("email")?.setValue(this.usuarioLogado.Email);
    this.senhaForm.get("senha")?.setValue('');
    this.senhaForm.get("senhaNova")?.setValue('');
    this.senhaForm.get("confirmarSenha")?.setValue('');
  }
}
