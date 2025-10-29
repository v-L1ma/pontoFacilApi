import {MatDialogModule} from '@angular/material/dialog';
import {Component, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule,  } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { cpfValidator } from '../../validators/CPF.validator'; 

@Component({
  selector: 'app-cadastro-colaborador-form',
  imports: [
    MatDialogModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatSelectModule
  ],
  templateUrl: './cadastro-colaborador-form.component.html',
  styleUrl: './cadastro-colaborador-form.component.scss',
})
export class CadastroColaboradorFormComponent implements OnInit{
  cadastroForm!:FormGroup;
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

  constructor(private fb:FormBuilder){}

  ngOnInit(): void {
    this.cadastroForm = this.fb.group({
          nome: ['', [Validators.required, Validators.minLength(3),Validators.pattern(/^[\p{L}\s'-]+$/u)]],
          CPF: ['', [Validators.required, cpfValidator]],
          cargoId: ['', [Validators.required]]
        });
  }

}
