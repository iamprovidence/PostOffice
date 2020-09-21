import { NgModule } from '@angular/core';
import { RoutingModule } from './routing.module';
import { UiModule } from './ui.module';

@NgModule({
	declarations: [RoutingModule.components],
	imports: [
		UiModule,
		RoutingModule,
	],
})
export class ClientPageModule { }
