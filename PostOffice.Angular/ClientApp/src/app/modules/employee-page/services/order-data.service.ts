import { Injectable } from '@angular/core';
import { OrderApiService } from '@app/core/api/services/order-api.service';
import { Observable, of } from 'rxjs';
import { EditOrder } from '../models/edit-order.model';

@Injectable({
	providedIn: 'root',
})
export class OrderDataService {

	constructor(private orderApiService: OrderApiService) { }

	public registerOrder(editOrderItem: EditOrder): Observable<boolean> {
		return of(true);

		// return this.orderApiService.createOrder(null);
	}
	
}
