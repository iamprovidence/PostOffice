import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChooseActionComponent } from './pages/choose-action/choose-action.component';
import { OrderHistoryComponent } from './pages/order-history/order-history.component';

const routes: Routes = [
	{
		path: '',
		redirectTo: 'choose-action',
		pathMatch: 'full',
		data: {
			depth: "1",
			animation: 'HomePage',
		},
	},
	{
		path: 'choose-action',
		component: ChooseActionComponent,
		data: {
			depth: "1",
			animation: 'HomePage',
		},
	},
	{
		path: 'order-history',
		component: OrderHistoryComponent,
		data: {
			depth: "1",
			animation: 'HomePage',
		},
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class RoutingModule {
	public static components = [ChooseActionComponent, OrderHistoryComponent];
}
