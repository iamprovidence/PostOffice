import { TextFieldModule } from '@angular/cdk/text-field';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatInputModule } from '@angular/material/input';
import { MatStepperModule } from '@angular/material/stepper';

const modules = [
	TextFieldModule,
	MatButtonModule,
	MatFormFieldModule,
	MatInputModule,
	MatCardModule,
	MatGridListModule,
	MatStepperModule,
];

@NgModule({
	imports: [...modules],
	exports: [...modules],
})
export class UiModule { }
