import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { RouterModule } from '@angular/router';
import { ConfirmationDialogComponent } from './components/confirmation-dialog/confirmation-dialog.component';
import { ConfirmationDialogService } from './components/confirmation-dialog/services/confirmation-dialog.service';
import { NotificationDialogService } from './components/notification-dialog/notification-dialog.service';
import { LetDirective } from './directives/let.directive';
import { LayoutModule } from './layout/layout.module';

const modules = [
	MatButtonModule,
	MatDialogModule,
	MatSnackBarModule,

	CommonModule,
	RouterModule,
	FormsModule,
	ReactiveFormsModule,
	LayoutModule,
];

const directives = [
	LetDirective,
];

const components = [
	ConfirmationDialogComponent,
];

const services = [
	ConfirmationDialogService,
	NotificationDialogService,
];

@NgModule({
	imports: [
		modules,
	],
	declarations: [
		directives,
		components,
	],
	exports: [
		modules,
		directives,
	],
	providers: [
		services,
	],
})
export class SharedModule { }
