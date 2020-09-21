import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { LayoutModule } from './layout/layout.module';

const modules = [
	CommonModule,
	RouterModule,
	FormsModule,
	ReactiveFormsModule,
	LayoutModule,
];

@NgModule({
	imports: [
		modules,
	],
	exports: [
		modules,
	],
})
export class SharedModule { }
