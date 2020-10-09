import { Location } from "../location";

export interface EditOrderLocationRequest {
	ttn: string;
	newLocation: Location;
}
