import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EnvironmentService } from '../../environment/services/environment.service';
import { CreateOrderRequest } from '../models/orders/create-order-request';
import { BaseApiService } from './base-api.service';

@Injectable({
	providedIn: 'root',
})
export class OrderApiService extends BaseApiService {

	constructor(environmentService: EnvironmentService) {
		super(environmentService);
	}

	protected initialize(): void {
		this.hubSpot = "orders";
	}

	public createOrder(createOrderRequest: CreateOrderRequest): Observable<boolean> {
		return this.call<boolean>("CreateOrder", createOrderRequest);
	}
	// TODO: example resolving event as response
/*
	public createOrder2() {
		this.hubConnection.invoke<number>("MakeOrder", {})

		from(
			new Promise<T>((resolve, reject) => {
				this.hubConnection.on("OrderCreated", res => resolve(res))
			}))
			.subscribe(d => console.log("OrderCreated", d))
	}
	*/
}
