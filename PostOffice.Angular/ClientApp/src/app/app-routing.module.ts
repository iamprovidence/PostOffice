import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
	{
		path: '',
		redirectTo: 'client',
		pathMatch: 'full',
	},
	{
		path: 'client',
		loadChildren: () => import('@modules/client-page/client-page.module').then(m => m.ClientPageModule),

	},
	{
		path: 'employee',
		loadChildren: () => import('@modules/employee-page/employee-page.module').then(m => m.EmployeePageModule),
	},
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule],
})
export class AppRoutingModule { }
