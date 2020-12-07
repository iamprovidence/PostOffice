import { OrderEventDetailsItem } from "./order-event-details-item";
import { EventType } from "./event-type";

export interface OrderEventListItem {
	type: EventType;
	title: string;
	time: string;
	details: OrderEventDetailsItem[];
}
