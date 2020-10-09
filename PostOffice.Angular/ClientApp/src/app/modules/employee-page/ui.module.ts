import { TextFieldModule } from '@angular/cdk/text-field';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatStepperModule } from '@angular/material/stepper';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';

const modules = [
	TextFieldModule,
	MatButtonModule,
	MatFormFieldModule,
	MatInputModule,
	MatCardModule,
	MatGridListModule,
	MatIconModule,
	MatStepperModule,
	MatTableModule,
	MatMenuModule,
	MatDialogModule,
];

@NgModule({
	imports: [...modules],
	exports: [...modules],
})
export class UiModule { }
