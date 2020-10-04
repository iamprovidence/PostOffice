import { OrderStatus } from '../enums/order-status.enum';
import { Location } from '../location';

export interface OrderListItem {
	ttn: string;
	description: string;
	status: OrderStatus;
	currentLocation: Location;
}
