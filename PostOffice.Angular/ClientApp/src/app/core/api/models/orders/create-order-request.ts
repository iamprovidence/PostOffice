import { Location } from "../location";

export interface CreateOrderRequest {
	description: string;
	senderLocation: Location;
	recipientLocation: Location;
}
