import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChooseActionComponent } from "./pages/choose-action/choose-action.component";
import { CreateOrderComponent } from "./pages/create-order/create-order.component";
import { OrderCreatedComponent } from "./pages/order-created/order-created.component";
import { OrderListComponent } from "./pages/order-list/order-list.component";

const routes: Routes = [
	{
		path: '',
		redirectTo: 'choose-action',
		pathMatch: 'full',
		data: {
			depth: "0",
			animation: 'AboutPage',
		},
	},
	{
		path: 'choose-action',
		component: ChooseActionComponent,
		data: {
			depth: "0",
			animation: 'AboutPage',
		},
	},
	{
		path: 'create-order',
		component: CreateOrderComponent,
		data: {
			depth: "0",
			animation: 'AboutPage',
		},
	},
	{
		path: 'order-list',
		component: OrderListComponent,
		data: {
			depth: "0",
			animation: 'AboutPage',
		},
	},
	{
		path: 'order-created',
		component: OrderCreatedComponent,
		data: {
			depth: "0",
			animation: 'AboutPage',
		},
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class RoutingModule {
	public static pages = [ChooseActionComponent, CreateOrderComponent, OrderCreatedComponent, OrderListComponent];
}
