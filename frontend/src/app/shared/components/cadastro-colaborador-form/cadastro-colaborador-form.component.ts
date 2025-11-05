import {MatDialogModule} from '@angular/material/dialog';
import {Component, Input, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule,  } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { cpfValidator } from '../../validators/CPF.validator'; 
import { Cargo, responseBase, Setor } from '../../types/types';
import { SetoresService } from '../../services/setores/setores.service';
import { CargoService } from '../../services/cargos/cargo.service';
import { vazioValidator } from '../../validators/vazio.validator';

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
  @Input() formData: any;
  cargos:Cargo[] = [];

  setores:Setor[] = [];

  constructor(
    private fb:FormBuilder,
    private setoresService:SetoresService,
    private cargosService:CargoService
  ){}

  ngOnInit(): void {   
    this.buscarSetores();

    if(this.formData){     
      this.cadastroForm = this.fb.group({
      nome: [this.formData.nome, [Validators.required, Validators.minLength(3),Validators.pattern(/^[\p{L}\s'-]+$/u),vazioValidator]],
      CPF: [this.formData.CPF, [Validators.required, cpfValidator]],
      setorId: [this.formData.setorId, [Validators.required]],
      cargoId: [this.formData.cargoId, [Validators.required]]
      });
      this.buscarCargos(this.formData.setorId)
    } else{
      this.cadastroForm = this.fb.group({
        nome: ['', [Validators.required,Validators.minLength(3),Validators.pattern(/^[\p{L}\s'-]+$/u),vazioValidator]],
        CPF: ['', [Validators.required, cpfValidator]],
        setorId: ['', [Validators.required]],
        cargoId: ['', [Validators.required]]
      });
    }

    this.cadastroForm.get('setorId')?.valueChanges.subscribe((value:number)=>{
      this.buscarCargos(value);
    })
  }

  buscarCargos(idSetor:number){
    this.cargosService.buscarPorSetor(idSetor).subscribe((response:responseBase)=>{
      this.cargos=response.dados
    }); 
  }

  buscarSetores(){
    this.setoresService.buscarTodos().subscribe((response:responseBase)=>{
      this.setores=response.dados
    }); 
  }

}