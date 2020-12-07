import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

const modules = [
	MatButtonModule,
	MatDialogModule,
	MatIconModule,
	MatInputModule,
	MatProgressSpinnerModule,
];

@NgModule({
  imports: [...modules],
  exports: [...modules],
})
export class UiModule { }
