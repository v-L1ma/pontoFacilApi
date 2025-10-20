import {MatDialogModule} from '@angular/material/dialog';
import {Component, inject, OnInit} from '@angular/core';
import {AbstractControlOptions, FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatButtonModule} from '@angular/material/button';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle,
} from '@angular/material/dialog';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule,  } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { cpfValidator } from '../../validators/CPF.validator';
import { colaborador } from '../../types/types';

export interface DialogData {
  tituloModal: string;
  descricaoModal:string;
  colaborador?:colaborador
}
@Component({
  selector: 'app-modal',
  imports: [
    MatDialogModule,
    MatButtonModule,
    MatDialogActions,
    MatDialogClose,
    MatDialogContent,
    MatDialogTitle,
    MatInputModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatSelectModule
],
  templateUrl: './modal.component.html',
  styleUrl: './modal.component.scss'
})
export class ModalComponent implements OnInit{
  readonly dialogRef = inject(MatDialogRef<ModalComponent>);
  readonly data = inject<DialogData>(MAT_DIALOG_DATA);
  cadastroForm!:FormGroup;
  private fb= inject(FormBuilder)
  cargos = [
    { Id: 1, Nome: "Estagiário"},
    { Id: 2, Nome: "Assistente Administrativo"},
    { Id: 3, Nome: "Analista Administrativo"},
    { Id: 4, Nome: "Coordenador Administrativo"},
    { Id: 5, Nome: "Assistente Financeiro"},
    { Id: 6, Nome: "Analista Financeiro"},
    { Id: 7, Nome: "Gerente Financeiro"},
    { Id: 8, Nome: "Analista de RH"},
    { Id: 9, Nome: "Coordenador de RH"},
    { Id: 10, Nome: "Recrutador"},
    { Id: 11, Nome: "Vendedor"},
    { Id: 12, Nome: "Representante Comercial"},
    { Id: 13, Nome: "Gerente Comercial"},
    { Id: 14, Nome: "Desenvolvedor"},
    { Id: 15, Nome: "Analista de Sistemas"},
    { Id: 16, Nome: "Administrador de Redes"},
    { Id: 17, Nome: "Coordenador de TI"},
    { Id: 18, Nome: "Auxiliar de Logística"},
    { Id: 19, Nome: "Supervisor de Logística"},
    { Id: 20, Nome: "Advogado"},
    { Id: 21, Nome: "Assistente Jurídico"},
    { Id: 22, Nome: "Analista de Marketing"},
    { Id: 23, Nome: "Designer Gráfico"},
    { Id: 24, Nome: "Social Media"},
    { Id: 25, Nome: "Operador de Máquina"},
    { Id: 26, Nome: "Supervisor de Produção"},
    { Id: 27, Nome: "Atendente"},
    { Id: 28, Nome: "Supervisor de Atendimento"}
  ];

  ngOnInit(): void {

    
    if(this.data.colaborador!==null){
      const {nome,cpf,cargo} = this.data.colaborador!;

      const cargoId = this.cargos.find(c=> c.Nome == cargo)?.Id;

      this.cadastroForm = this.fb.group({
        nome: [nome, [Validators.required, Validators.minLength(5)]],
        CPF: [cpf, [Validators.required, cpfValidator]],
        cargoId: [cargoId, [Validators.required]]
      });
      return;
    }

    this.cadastroForm = this.fb.group({
          nome: ['', [Validators.required, Validators.minLength(5)]],
          CPF: ['', [Validators.required, cpfValidator]],
          cargoId: ['', [Validators.required]]
        });
  }

  onNoClick(): void {
    this.limparFormulario();
    this.dialogRef.close();
  }

  limparFormulario(){
    this.cadastroForm.get("nome")?.setValue('');
    this.cadastroForm.get("email")?.setValue('');
    this.cadastroForm.get("cargo")?.setValue('');
    this.cadastroForm.get("senha")?.setValue('');
    this.cadastroForm.get("confirmarSena")?.setValue('');
  }

}
