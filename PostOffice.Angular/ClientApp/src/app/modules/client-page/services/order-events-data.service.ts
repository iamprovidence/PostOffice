import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { OrderEventListItem } from '../models/order-event-list-item';

@Injectable()
export class OrderEventsDataService {

	orderEvents: OrderEventListItem[] = [
		{
			type: "assignment_turned_in",
			title: "Order recieved",
			time: "10:48",
			details: [
				{
					type: "Note:",
					value: "My order was broken"
				}
			]
		},
		{
			type: "assignment_returned",
			title: "Order delivered",
			time: "3 min",
			details: [
				{
					type: "Lorem",
					value: "Ipsum"
				}
			]
		},
		{
			type: "delete",
			title: "Order deleted",
			time: "10:49",
			details: [
				{
					type: "Deleted by",
					value: "Employee"
				}
			]
		},
		{
			type: "cancel",
			title: "Order canceled",
			time: "10:56",
			details: [
			]
		},
		{
			type: "room",
			title: "Location changed",
			time: "10:55",
			details: [
				{
					type: "Lorem",
					value: "Ipsum"
				},
			]
		},
		{
			type: "add_circle",
			title: "New order created",
			time: "10:43",
			details: [
				{
					type: "Lorem",
					value: "Ipsum"
				},
				{
					type: "Lorem",
					value: "Ipsum"
				}
			]
		},
	]

	public addEvent() {

		const event: OrderEventListItem = {
			type: "add_circle",
			time: "10:15",
			title: "New order",
			details: [],
		}

		this.orderEvents.unshift(event);
	}

	public getOrderEvents(orderTtn: string): Observable<OrderEventListItem[]> {
		return of(this.orderEvents);
	}
}
