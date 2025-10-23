import { Component, inject, signal } from '@angular/core';
import { DialogData, ModalFormComponent } from '../modalForm/modal-form.component';
import {MatDialogModule} from '@angular/material/dialog';
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
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-modal-confirmar',
  imports: [
    MatDialogModule,
    MatButtonModule,
    MatDialogActions,
    MatDialogClose,
    MatDialogContent,
    MatDialogTitle,
    MatInputModule,
    MatFormFieldModule,
    MatSelectModule,
    ReactiveFormsModule,
    MatIconModule
  ],
  templateUrl: './modal-confirmar.component.html',
  styleUrl: './modal-confirmar.component.scss'
})
export class ModalConfirmarComponent {
  readonly dialogRef = inject(MatDialogRef<ModalFormComponent>);
  readonly data = inject<DialogData>(MAT_DIALOG_DATA);

  constructor(
    private fb: FormBuilder
  ){}

  onNoClick(): void {
    this.dialogRef.close();
  }
}
