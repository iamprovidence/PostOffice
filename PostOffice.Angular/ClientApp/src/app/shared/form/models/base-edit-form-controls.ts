import { AbstractControl } from '@angular/forms';

export type BaseEditFormControls = { [key: string]: AbstractControl } | AbstractControl[];
