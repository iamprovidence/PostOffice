import { FormArray, FormGroup } from "@angular/forms";
import { Observable } from "rxjs";
import { BaseEditFormControls } from "./models/base-edit-form-controls";
import { BaseEditFormError } from "./models/base-edit-form-error";

export abstract class BaseEditForm<T> {
	public abstract form: FormGroup;
	public abstract patchValue(value: T): void;
	public abstract getValue(): Observable<T>;

	public get isValid(): boolean {
		return this.form.valid;
	}

	public markAsDirty(): void {
		const errors = [];

		this.form.markAsDirty();
		this.checkForErrors(this.form.controls, "", errors)

		console.warn(errors, this.form);
	}

	public checkForErrors(
		controls: BaseEditFormControls,
		path: string,
		errors: BaseEditFormError[],
	): void {
		for (const control in controls) {
			if (!controls[control].valid) {
				errors.push({ id: path + control, error: controls[control].errors });

				if (controls[control] instanceof FormArray) {
					this.checkForErrors((<FormArray>controls[control]).controls, `${path}${control}.`, errors);
				}

				if (controls[control] instanceof FormGroup) {
					this.checkForErrors((<FormGroup>controls[control]).controls, `${path}${control}.`, errors);
				}
			}
		}
	}
}
