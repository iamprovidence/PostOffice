import { CdkTextareaAutosize } from '@angular/cdk/text-field';
import { Component, NgZone, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { OrderDataService } from '@modules/employee-page/services/order-data.service';
import { take } from 'rxjs/operators';
import { CreateOrderEditForm } from './create-order-edit-form';

@Component({
	selector: 'app-create-order',
	templateUrl: './create-order.component.html',
	styleUrls: ['./create-order.component.sass'],
	providers: [CreateOrderEditForm],
})
export class CreateOrderComponent implements OnInit {

	@ViewChild('autosize')
	public autosize: CdkTextareaAutosize;

	constructor(
		private ngZone: NgZone,
		private router: Router,
		private orderDataService: OrderDataService,
		public editForm: CreateOrderEditForm,
	) { }

	public ngOnInit(): void { }

	public triggerResize(): void {
		// Wait for changes to be applied, then trigger textarea resize.
		this.ngZone
			.onStable
			.pipe(take(1))
			.subscribe(() => this.autosize.resizeToFitContent(true));
	}

	public addNewCargo(): void {
		console.log(this.editForm)
		this.editForm.addCargo(null);
	}

	public deleteCargo(index: number): void {
		this.editForm.deleteCargo(index);
	}

	public saveOrder(): void {
		this.orderDataService
			.registerOrder(null)
			.subscribe(didSucceed => {
				if (didSucceed) {
					this.router.navigate(['/employee/order-created']);
				}
			});
	}
}
