import { Cargo } from '@modules/employee-page/models/cargo.model';
import { Location } from "../location";

export interface CreateOrderRequest {
	description: string;
	senderLocation: Location;
	recipientLocation: Location;
	cargos: Cargo[];
}
