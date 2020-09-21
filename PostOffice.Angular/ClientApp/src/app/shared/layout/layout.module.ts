import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HeaderComponent } from './header/header.component';
import { LayoutUiModule } from './layout-ui.module';

@NgModule({
	imports: [
		LayoutUiModule,
		RouterModule,
	],
	declarations: [
		HeaderComponent,
	],
	exports: [
		HeaderComponent,
	],
})
export class LayoutModule { }
