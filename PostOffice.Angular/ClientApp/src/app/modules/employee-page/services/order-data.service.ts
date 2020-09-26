import { Injectable } from '@angular/core';
import { CreateOrderRequest } from '@app/core/api/models/orders/create-order-request';
import { OrderApiService } from '@app/core/api/services/order-api.service';
import { Observable } from 'rxjs';
import { EditOrder } from '../models/edit-order.model';
import { Location } from '@app/core/api/models/location';

@Injectable({
	providedIn: 'root',
})
export class OrderDataService {

	constructor(private orderApiService: OrderApiService) { }

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
