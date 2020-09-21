import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { RoutingModule } from './routing.module';
import { UiModule } from './ui.module';

@NgModule({
	declarations: [RoutingModule.components],
	imports: [
		UiModule,
		RoutingModule,
		SharedModule,
	],
})
export class EmployeePageModule { }
