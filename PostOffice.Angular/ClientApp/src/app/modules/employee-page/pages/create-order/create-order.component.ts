import { CdkTextareaAutosize } from '@angular/cdk/text-field';
import { Component, NgZone, OnInit, ViewChild } from '@angular/core';
import { take } from 'rxjs/operators';
import { CreateOrderEditForm } from './create-order-edit-form';

@Component({
	selector: 'app-create-order',
	templateUrl: './create-order.component.html',
	styleUrls: ['./create-order.component.sass'],
})
export class CreateOrderComponent implements OnInit {

	@ViewChild('autosize')
	public autosize: CdkTextareaAutosize;

	constructor(
		private ngZone: NgZone,
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

	public v()
	{
		console.log(this.editForm)
		this.editForm.getValue().subscribe(s => console.log(s))
	}
}
