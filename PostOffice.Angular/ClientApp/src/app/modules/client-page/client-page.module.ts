import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { RoutingModule } from './routing.module';
import { OrderEventsDataService } from './services/order-events-data.service';
import { UiModule } from './ui.module';

@NgModule({
	declarations: [RoutingModule.components],
	imports: [
		SharedModule,
		UiModule,
		RoutingModule,
	],
	providers: [
		OrderEventsDataService,
	]
})
export class ClientPageModule { }
