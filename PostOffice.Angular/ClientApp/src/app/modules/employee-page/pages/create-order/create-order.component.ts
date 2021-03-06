import { CdkTextareaAutosize } from '@angular/cdk/text-field';
import { Component, NgZone, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { OrderDataService } from '@modules/employee-page/services/order-data.service';
import { BaseComponent } from '@shared/components/base-component';
import { switchMap, take, takeUntil } from 'rxjs/operators';
import { CreateOrderEditForm } from './create-order-edit-form';

@Component({
	selector: 'app-create-order',
	templateUrl: './create-order.component.html',
	styleUrls: ['./create-order.component.sass'],
	providers: [CreateOrderEditForm],
})
export class CreateOrderComponent extends BaseComponent {

	@ViewChild('autosize')
	public autosize: CdkTextareaAutosize;

	constructor(
		private ngZone: NgZone,
		private router: Router,
		private orderDataService: OrderDataService,
		public editForm: CreateOrderEditForm,
	) {
		super();
	}

	public triggerResize(): void {
		// Wait for changes to be applied, then trigger textarea resize.
		this.ngZone
			.onStable
			.pipe(
				take(1),
				takeUntil(this.$unsubscribe),
			)
			.subscribe(() => this.autosize.resizeToFitContent(true));
	}

	public addNewCargo(): void {
		this.editForm.addCargo(null);
	}

	public deleteCargo(index: number): void {
		this.editForm.deleteCargo(index);
	}

	public saveOrder(): void {
		this.editForm
			.getValue()
			.pipe(
				takeUntil(this.$unsubscribe),
				switchMap(editOrderItem => this.orderDataService.registerOrder(editOrderItem)),
			)
			.subscribe(didSucceed => {
				if (didSucceed) {
					this.router.navigate(['/employee/order-created']);
				}
			});
	}
}
