import {MatDialogModule} from '@angular/material/dialog';
import {Component, input, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule,  } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';

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
export class CadastroSetorFormComponent implements OnInit{
  cadastroForm!:FormGroup;
  formData = input<any>()

  constructor(private fb:FormBuilder){}

  ngOnInit(): void {
    this.cadastroForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(3),Validators.pattern(/^[\p{L}\s'-]+$/u)]],
    });
  }
}
