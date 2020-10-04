import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '@app/shared/components/base-component';
import { OrderListItem } from '@core/api/models/orders/order-list-item';
import { takeUntil, filter, switchMap } from 'rxjs/operators';
import { OrderDataService } from '../../services/order-data.service';
import { ConfirmationDialogService } from '@shared/components/confirmation-dialog/services/confirmation-dialog.service';
import { ConfirmationDialogResult } from '@shared/components/confirmation-dialog/enums/confirmation-dialog-result';
import { NotificationDialogService } from '@shared/components/notification-dialog/notification-dialog.service';

@Component({
	selector: 'app-order-list',
	templateUrl: './order-list.component.html',
	styleUrls: ['./order-list.component.sass'],
})
export class OrderListComponent extends BaseComponent implements OnInit {

	public orderList: OrderListItem[];

	constructor(
		private orderDataService: OrderDataService,
		private confirmationDataService: ConfirmationDialogService,
		private notificationDialogService: NotificationDialogService,
	) {
		super();
	}

	public ngOnInit(): void {
		this.orderDataService
			.getOrders()
			.pipe(takeUntil(this.$unsubscribe))
			.subscribe(orders => this.orderList = orders);

		this.orderDataService.orderDeleted.subscribe(this.orderDeleted.bind(this));
	}

	public delete(ttn: string): void {
		this.confirmationDataService.open({
			questionText: "Are you sure you want to delete current order?",
			confirmationText: "Yes",
			rejectionText: "No",
		}).pipe(
			takeUntil(this.$unsubscribe),
			filter(result => result === ConfirmationDialogResult.Confirmed),
			switchMap(() => this.orderDataService.deleteOrder(ttn)),
		).subscribe(() => {
			this.notificationDialogService.open("Order has been successfully deleted");
		});
	}

	public orderDeleted(ttn: string): void {
		this.orderList = this.orderList?.filter(o => o.ttn !== ttn);
	}
}
