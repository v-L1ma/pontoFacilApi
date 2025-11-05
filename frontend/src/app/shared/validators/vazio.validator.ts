import { AbstractControl, ValidationErrors } from '@angular/forms';

export function vazioValidator(control: AbstractControl): ValidationErrors | null {
  if (!control.value) {
    return null;
  }

  const campo = control.value.trim()

  if(campo.length==0){
    return { campoVazio: true }
  }

  return null;
}
