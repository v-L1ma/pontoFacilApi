import {MatDialogModule} from '@angular/material/dialog';
import {Component, Input, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule,  } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { cpfValidator } from '../../validators/CPF.validator'; 
import { SetoresService } from '../../services/setores/setores.service';
import { Setor } from '../../types/types';

@Component({
  selector: 'app-cadastro-cargo-form',
  imports: [
    MatDialogModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatSelectModule
  ],
  templateUrl: './cadastro-cargo-form.component.html',
  styleUrl: './cadastro-cargo-form.component.scss',
})
export class CadastroCargoFormComponent implements OnInit{
  cadastroForm!:FormGroup;
  @Input() formData: any;
  setores:Setor[] = [];

  constructor(
    private fb:FormBuilder,
    private setoresService:SetoresService
  ){}

  ngOnInit(): void {
    this.setoresService.buscarTodos().subscribe((response)=>{
      this.setores = response.dados
      console.log(this.setores)
    })
    
    if(this.formData){
      this.cadastroForm = this.fb.group({
        nome: [this.formData.nome, [Validators.required, Validators.minLength(3),Validators.pattern(/^[\p{L}\s'-]+$/u)]],
        setorId: [this.formData.setorId, [Validators.required]]
      });
      return;
    }
    this.cadastroForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(3),Validators.pattern(/^[\p{L}\s'-]+$/u)]],
      setorId: ['', [Validators.required]]
    });
  }

}
