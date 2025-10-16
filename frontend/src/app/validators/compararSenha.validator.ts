    import { AbstractControl, ValidatorFn } from '@angular/forms';

    export function compararSenhaValidator(controlName1: string, controlName2: string): ValidatorFn {
      return (formGroup: AbstractControl): { [key: string]: any } | null => {
        const control1 = formGroup.get(controlName1);
        const control2 = formGroup.get(controlName2);

        if (!control1 || !control2) {
          return null;
        }

        if (control1.value !== control2.value) {
          control2.setErrors({ camposDiferentes: true });
          return { camposDiferentes: true }; 
        } else {
          control2.setErrors(null);
          return null;
        }
      };
    }