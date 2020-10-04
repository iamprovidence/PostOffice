import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { RoutingModule } from './routing.module';
import { OrderDataService } from './services/order-data.service';
import { UiModule } from './ui.module';

@NgModule({
	declarations: [RoutingModule.components],
	imports: [
		UiModule,
		RoutingModule,
		SharedModule,
	],
	providers: [
		OrderDataService,// TODO: find a way to init this service in components and dispose onDestroy
	]
})
export class EmployeePageModule { }
