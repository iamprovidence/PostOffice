import { EventEmitter, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EnvironmentService } from '../../environment/services/environment.service';
import { CreateOrderRequest } from '../models/orders/create-order-request';
import { DeleteOrderRequest } from '../models/orders/delete-order-request';
import { EditOrderLocationRequest } from '../models/orders/edit-order-location-request';
import { OrderListItem } from '../models/orders/order-list-item';
import { OrderLocationChanged } from '../models/orders/order-location-changed';
import { BaseApiService } from './base-api.service';

@Injectable({
	providedIn: 'root',
})
export class OrderApiService extends BaseApiService {

	public readonly orderDeleted = new EventEmitter<string>();
	public readonly orderLocationChanged = new EventEmitter<OrderLocationChanged>();

	constructor(environmentService: EnvironmentService) {
		super(environmentService);
	}

	protected initialize(): void {
		this.hubSpot = "orders";
	}

	protected subscribeToEvents(): void {
		this.on<string>("OrderDeleted").subscribe((ttn: string) => this.orderDeleted.emit(ttn));
		this.on<OrderLocationChanged>("OrderLocationChanged").subscribe(model => this.orderLocationChanged.emit(model));
	}

	public getOrders(): Observable<OrderListItem[]> {
		return this.callAsync('GetOrders', 'OrdersLoaded');
	}

	public createOrder(createOrderRequest: CreateOrderRequest): Observable<boolean> {
		return this.call<boolean>("CreateOrder", createOrderRequest);
	}

	public changeOrderLocation(request: EditOrderLocationRequest): Observable<void> {
		return this.call<void>("ChangeOrderLocation", request);
	}

	public deleteOrder(deleteOrderRequest: DeleteOrderRequest): Observable<void> {
		// do not wait for response, better use event
		// sinve event in shared for method end integration event as well
		// return this.callAsync('DeleteOrder', 'OrderDeleted', deleteOrderRequest);

		return this.call<void>("DeleteOrder", deleteOrderRequest);
	}
}
