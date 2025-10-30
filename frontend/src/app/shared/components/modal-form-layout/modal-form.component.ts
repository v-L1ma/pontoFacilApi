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
