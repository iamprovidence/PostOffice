import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '@app/shared/components/base-component';
import { OrderListItem } from '@core/api/models/orders/order-list-item';
import { EditOrderLocationData } from '@modules/employee-page/components/edit-order-location-dialog/models/edit-order-location-data';
import { EditOrderLocationDialogService } from '@modules/employee-page/components/edit-order-location-dialog/services/edit-order-location-dialog.service';
import { OrderDataService } from '@modules/employee-page/services/order-data.service';
import { ConfirmationDialogResult } from '@shared/components/confirmation-dialog/enums/confirmation-dialog-result';
import { ConfirmationDialogService } from '@shared/components/confirmation-dialog/services/confirmation-dialog.service';
import { NotificationDialogService } from '@shared/components/notification-dialog/notification-dialog.service';
import { filter, switchMap, takeUntil } from 'rxjs/operators';
import { OrderLocationChanged } from '@app/core/api/models/orders/order-location-changed';

@Component({
	selector: 'app-order-list',
	templateUrl: './order-list.component.html',
	styleUrls: ['./order-list.component.sass'],
	providers: [EditOrderLocationDialogService],
})
export class OrderListComponent extends BaseComponent implements OnInit {

	public orderList: OrderListItem[];

	constructor(
		private orderDataService: OrderDataService,
		private confirmationDataService: ConfirmationDialogService,
		private notificationDialogService: NotificationDialogService,
		private editOrderLocationDialogService: EditOrderLocationDialogService,
	) {
		super();
	}

	public ngOnInit(): void {
		this.orderDataService
			.getOrders()
			.pipe(takeUntil(this.$unsubscribe))
			.subscribe(orders => this.orderList = orders);

		this.orderDataService.orderDeleted.subscribe(this.onOrderDeleted.bind(this));
		this.orderDataService.orderLocationChanged.subscribe(this.onOrderLocationChanged.bind(this));
	}

	public editLocation(currentOrder: OrderListItem): void {
		const dialogData: EditOrderLocationData = {
			ttn: currentOrder.ttn,
			currentLocation: currentOrder.currentLocation,
		};

		this.editOrderLocationDialogService
			.open(dialogData)
			.pipe(
				takeUntil(this.$unsubscribe),
				switchMap((newLocation) => this.orderDataService.editOrderLocation(currentOrder.ttn, newLocation)),
			)
			.subscribe();
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

	public onOrderDeleted(ttn: string): void {
		this.orderList = this.orderList?.filter(o => o.ttn !== ttn);
	}

	public onOrderLocationChanged(model: OrderLocationChanged): void {
		const orderIndex = this.orderList.findIndex(o => o.ttn == model.ttn);
		if (orderIndex === -1) return;

		const order = this.orderList[orderIndex];
		order.currentLocation = model.location;
		this.orderList[orderIndex] = order;
	}
}
