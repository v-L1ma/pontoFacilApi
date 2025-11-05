import {MatDialogModule} from '@angular/material/dialog';
import {Component, Input, input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule,  } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { vazioValidator } from '../../validators/vazio.validator';

@Component({
  selector: 'app-cadastro-setor-form',
  imports: [
    MatDialogModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatSelectModule
  ],
  templateUrl: './cadastro-setor-form.component.html',
  styleUrl: './cadastro-setor-form.component.scss',
})
export class CadastroSetorFormComponent implements OnInit {
  cadastroForm!:FormGroup;
  @Input() formData: any;

  constructor(private fb:FormBuilder){}

  ngOnInit(): void {
    if(this.formData){
      console.log("LOG NO FORM",this.formData)
      this.cadastroForm = this.fb.group({
        nome: [this.formData.nome, [Validators.required, Validators.minLength(3),Validators.pattern(/^[\p{L}\s'-]+$/u),vazioValidator]],
      });
      return;
    }
    this.cadastroForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(3),Validators.pattern(/^[\p{L}\s'-]+$/u),vazioValidator]],
    });
  }
}
