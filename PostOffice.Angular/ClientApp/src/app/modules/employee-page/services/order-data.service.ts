import { EventEmitter, Injectable, OnDestroy } from '@angular/core';
import { Location } from '@app/core/api/models/location';
import { CreateOrderRequest } from '@app/core/api/models/orders/create-order-request';
import { DeleteOrderRequest } from '@app/core/api/models/orders/delete-order-request';
import { EditOrderLocationRequest } from '@app/core/api/models/orders/edit-order-location-request';
import { OrderListItem } from '@app/core/api/models/orders/order-list-item';
import { OrderLocationChanged } from '@app/core/api/models/orders/order-location-changed';
import { OrderApiService } from '@app/core/api/services/order-api.service';
import { Observable } from 'rxjs';
import { EditOrder } from '../models/edit-order.model';

@Injectable()
export class OrderDataService implements OnDestroy {

	public readonly orderDeleted = new EventEmitter<string>();
	public readonly orderLocationChanged = new EventEmitter<OrderLocationChanged>();

	constructor(private orderApiService: OrderApiService) {
		console.warn("ctor");

		this.orderApiService.connect();

		this.orderApiService.orderDeleted.subscribe((ttn: string) => this.orderDeleted.next(ttn))
		this.orderApiService.orderLocationChanged.subscribe((model: OrderLocationChanged) => this.orderLocationChanged.next(model))
	}

	public ngOnDestroy(): void {
		console.error("SHOULD DISPOSE");

		this.orderApiService.dispose();
	}

	public getOrders(): Observable<OrderListItem[]> {
		return this.orderApiService.getOrders();
	}

	public editOrderLocation(ttn: string, newLocation: Location): Observable<void> {
		const request: EditOrderLocationRequest = {
			ttn: ttn,
			newLocation: newLocation,
		};

		return this.orderApiService.changeOrderLocation(request);
	}


	public deleteOrder(ttn: string): Observable<void> {
		const request: DeleteOrderRequest = {
			ttn: ttn,
		};

		return this.orderApiService.deleteOrder(request);
	}

	public registerOrder(editOrderItem: EditOrder): Observable<boolean> {
		const request: CreateOrderRequest = {
			description: editOrderItem.description,
			recipientLocation: new Location(editOrderItem.revieverCity, editOrderItem.revieverStreet),
			senderLocation: new Location(editOrderItem.senderCity, editOrderItem.senderStreet),
			cargos: editOrderItem.cargos,
		};

		return this.orderApiService.createOrder(request);
	}

}
