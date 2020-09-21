import { ValidationErrors } from "@angular/forms";

export interface BaseEditFormError {
	id: string;
	error: ValidationErrors;
}
