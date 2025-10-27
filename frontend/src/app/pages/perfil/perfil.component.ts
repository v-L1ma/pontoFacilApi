import { Component, effect, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import { compararSenhaValidator } from '../../shared/validators/compararSenha.validator';
import { usuarioLogadoService } from '../../shared/services/usuario-logado/usuario-logado.service'; 
import { usuario } from '../../shared/types/types';
import { AuthService } from '../../shared/services/auth/auth.service'; 
import { Router } from '@angular/router';
import { ModalConfirmarComponent } from '../../shared/components/modal-confirmar/modal-confirmar.component'; 
import { MatDialog } from '@angular/material/dialog';
import { VerficadorForcaSenhaComponent } from '../../shared/components/verficador-forca-senha/verficador-forca-senha.component';

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
    FormsModule,
    VerficadorForcaSenhaComponent
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
  readonly dialog = inject(MatDialog);
  senha=signal<string>('');
  private passwordRegex: string = '^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*\\-_])[A-Za-z\\d!@#$%^&*\\-_]{8,}$';
  mostrarSenhaAtual = signal(true);
  mostrarSenhaNova = signal(true);
  mostrarConfirmarSenhaNova = signal(true);

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
      senhaNova: ['', [Validators.required, Validators.minLength(3), Validators.pattern(this.passwordRegex)]],
      confirmarSenhaNova: ['', [Validators.required]]
    }, { validators:compararSenhaValidator('senhaNova','confirmarSenhaNova')});

    this.senhaForm.get('senhaNova')?.valueChanges.subscribe((senha)=>{
      this.senha.set(senha)
    })
  }

  salvarEdicao(){
    this.usuarioLogadoService.editarPerfil(this.cadastroForm.value)
  }

  alterarSenha(){
    this.usuarioLogadoService.alterarSenha(this.senhaForm.value).subscribe({
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

  openEditDialog(): void {
    const dialogRef = this.dialog.open(ModalConfirmarComponent, {
      data: {
        tituloModal: 'Deseja realmente excluir sua conta?', 
        descricaoModal:'Preencha o seu e-mail e senha para confirmar a exclusão da conta',
      },
    });

    dialogRef.afterClosed().subscribe((result:any) => {
      // console.log('The dialog was closed');
      if (result == true) {
        this.excluirConta();
      }
    });
  }

  esconder(campo:'atual'|'nova'|'confirmarNova'){

    switch (campo) {
      case 'atual':
    this.mostrarSenhaAtual.set(!this.mostrarSenhaAtual());
        break;
      case 'nova':
        this.mostrarSenhaNova.set(!this.mostrarSenhaNova());
        break;
      case 'confirmarNova':
        this.mostrarConfirmarSenhaNova.set(!this.mostrarConfirmarSenhaNova());
        break;
      default:
        break;
    }    
  }
}
