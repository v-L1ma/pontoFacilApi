import {MatDialogModule} from '@angular/material/dialog';
import {AfterViewInit, ChangeDetectorRef, Component, ComponentRef, Inject, inject, OnInit, Type, ViewChild, ViewContainerRef} from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
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

export interface DialogData {
  tituloModal: string;
  descricaoModal?: string;
  formData?: any;
  formComponent?: Type<any>;
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
  templateUrl: './modal-form.component.html',
  styleUrl: './modal-form.component.scss'
})
export class ModalFormComponent implements AfterViewInit{
  @ViewChild('formContainer', { read: ViewContainerRef }) formContainer!: ViewContainerRef;
  formComponentRef?: ComponentRef<any>;
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

  constructor(
    public dialogRef: MatDialogRef<ModalFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData){
      // console.log('💬 Data recebido no construtor:', data);
    }

    ngAfterViewInit(): void {
      console.log(this.data)
      if (this.data.formComponent) {
        this.formContainer.clear();
        this.formComponentRef = this.formContainer.createComponent(this.data.formComponent);

        if (this.data.formData) {
          this.formComponentRef.instance.formData = this.data.formData;
        }
      }
    }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onSave(){
    let formValue = this.formComponentRef?.instance?.cadastroForm?.value;
    return formValue;
  }

}
