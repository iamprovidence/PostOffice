import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';

const modules = [
	MatToolbarModule,
	MatButtonModule,
];

@NgModule({
	imports: [...modules],
	exports: [...modules],
})
export class LayoutUiModule { }
