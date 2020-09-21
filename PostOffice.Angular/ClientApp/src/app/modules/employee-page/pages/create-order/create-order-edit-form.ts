import { Injectable } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { EditOrder } from '@modules/employee-page/models/edit-order.model.ts';
import { BaseEditForm } from "@shared/form/base-edit-form";
import { Observable, of } from 'rxjs';

@Injectable({
	providedIn: 'root',
})
export class CreateOrderEditForm extends BaseEditForm<EditOrder> {

	private static readonly MIN_LENGTH = 3;

	public description = new FormControl('');
	public senderCity = new FormControl('', [Validators.required, Validators.minLength(CreateOrderEditForm.MIN_LENGTH)]);
	public senderStreet = new FormControl('', [Validators.required, Validators.minLength(CreateOrderEditForm.MIN_LENGTH)]);
	public revieverCity = new FormControl('', [Validators.required, Validators.minLength(CreateOrderEditForm.MIN_LENGTH)]);
	public revieverStreet = new FormControl('', [Validators.required, Validators.minLength(CreateOrderEditForm.MIN_LENGTH)]);
	public cargos = new FormControl([], [Validators.required]);

	public orderFormGroup = new FormGroup({
		description: this.description,
		senderCity: this.senderCity,
		senderStreet: this.senderStreet,
		revieverCity: this.revieverCity,
		revieverStreet: this.revieverStreet,
	});

	public cargoFormGroup = new FormGroup({
		cargos: this.cargos,
	});

	public form: FormGroup = new FormGroup({
		//...this.orderFormGroup.controls,
		//...this.cargoFormGroup.controls,
	});

	public patchValue(value: EditOrder): void {
		if (value === null) return;

		this.description.setValue(value.description);
	}

	public getValue(): Observable<EditOrder> {
		const editOrder: EditOrder = {
			...this.orderFormGroup.value,
			...this.cargoFormGroup.value,
		};

		return of(editOrder);
	}
}
