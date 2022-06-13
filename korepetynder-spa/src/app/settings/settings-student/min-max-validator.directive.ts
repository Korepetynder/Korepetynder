import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export const minMaxValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
  const min = control.get('minCost');
  const max = control.get('maxCost');

  if (!min || !max) {
    return null;
  }

  const minMaxInvalid = min.value > max.value;

  if (minMaxInvalid) {
    min.setErrors({ minMaxInvalid });
    max.setErrors({ minMaxInvalid });
  }
  else {
    min.setErrors(null);
    max.setErrors(null);
  }

  return null;
};
