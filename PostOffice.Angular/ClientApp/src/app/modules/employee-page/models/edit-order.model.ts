import { Cargo } from './cargo.model';

export interface EditOrder {
	description: string;
	senderCity: string;
	senderStreet: string;
	revieverCity: string;
	revieverStreet: string;
	cargos: Cargo[];
}
