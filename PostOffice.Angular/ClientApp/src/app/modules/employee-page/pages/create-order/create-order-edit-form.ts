import { Injectable } from "@angular/core";
import { FormControl, FormGroup, Validators, FormArray } from "@angular/forms";
import { Cargo } from '@modules/employee-page/models/cargo.model';
import { EditOrder } from '@modules/employee-page/models/edit-order.model.ts';
import { BaseEditForm } from "@shared/form/base-edit-form";
import { Observable, of } from 'rxjs';

@Injectable()
export class CreateOrderEditForm extends BaseEditForm<EditOrder> {
	public form: FormGroup;

	private static readonly MIN_LENGTH = 3;

	public description = new FormControl('');
	public senderCity = new FormControl('', [Validators.required, Validators.minLength(CreateOrderEditForm.MIN_LENGTH)]);
	public senderStreet = new FormControl('', [Validators.required, Validators.minLength(CreateOrderEditForm.MIN_LENGTH)]);
	public revieverCity = new FormControl('', [Validators.required, Validators.minLength(CreateOrderEditForm.MIN_LENGTH)]);
	public revieverStreet = new FormControl('', [Validators.required, Validators.minLength(CreateOrderEditForm.MIN_LENGTH)]);
	public cargos = new FormArray([], [Validators.required]);

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


	public patchValue(value: EditOrder): void {
		if (value === null) return;

		this.description.setValue(value.description);
	}

	public getValue(): Observable<EditOrder> {
		const cargos: Cargo[] = [
			...this.cargos.value,
		];

		const editOrder: EditOrder = {
			...this.orderFormGroup.value,
			cargos,
		};

		return of(editOrder);
	}

	public addCargo(cargo: Cargo): void {
		this.cargos.push(new FormGroup({
			height: new FormControl(cargo?.height, [Validators.required]),
			width: new FormControl(cargo?.width, [Validators.required]),
			length: new FormControl(cargo?.length, [Validators.required]),
		}));
	}

	public deleteCargo(index: number): void {
		this.cargos.controls.splice(index, 1);
		this.cargos.updateValueAndValidity();
	}
}
