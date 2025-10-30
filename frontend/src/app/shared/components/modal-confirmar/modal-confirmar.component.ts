import { Component, inject } from '@angular/core';
import { DialogData, ModalFormComponent } from '../modal-form-layout/modal-form.component';
import {MatDialogModule} from '@angular/material/dialog';
import {MatButtonModule} from '@angular/material/button';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogClose,
  MatDialogRef,
  MatDialogTitle,
} from '@angular/material/dialog';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule,  } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-modal-confirmar',
  imports: [
    MatDialogModule,
    MatButtonModule,
    MatDialogActions,
    MatDialogClose,
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
