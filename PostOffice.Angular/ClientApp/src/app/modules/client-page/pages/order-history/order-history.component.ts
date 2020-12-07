import { Component, OnInit } from '@angular/core';
import { EventType } from "../../models/event-type";
import { OrderEventListItem } from '../../models/order-event-list-item';
import { OrderEventsDataService } from '../../services/order-events-data.service';
import { displayEventAnimation } from './display-event-animation';

@Component({
	selector: 'app-order-history',
	templateUrl: './order-history.component.html',
	styleUrls: ['./order-history.component.sass'],
	animations: [displayEventAnimation],
})
export class OrderHistoryComponent implements OnInit {

	hasEvents: boolean = false;
	events: OrderEventListItem[] = [];

	constructor(
		private orderEventsDataService: OrderEventsDataService,
	) { }

	public ngOnInit(): void {
		setTimeout(() => {
			this.hasEvents = true

			this.orderEventsDataService
				.getOrderEvents("")
				.subscribe(events => {
					this.events = events;
				});
		}, 500)
	}

	public getIcon(eventType: EventType): string {
		return eventType;
	}

	public getClassName(eventType: EventType): string {
		switch (eventType) {
			case "add_circle": return "success";
			case "assignment_returned": return "warning";
			case "assignment_turned_in": return "success";
			case "cancel": return "error";
			case "delete": return "error";
			case "room": return "info";

			default: return "info"
		}
	}

	public addEvent() {
		this.orderEventsDataService.addEvent();
	}
}
