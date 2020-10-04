import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ConfirmationDialogComponent } from '../confirmation-dialog.component';
import { ConfirmationDialogResult } from '../enums/confirmation-dialog-result';
import { ConfirmationDialogData } from '../models/confirmation-dialog-data';

@Injectable()
export class ConfirmationDialogService {

	constructor(
		private dialog: MatDialog,
	) { }

	public open(data: ConfirmationDialogData): Observable<ConfirmationDialogResult> {
		const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
			data,
		});

		return dialogRef.afterClosed()
			.pipe(map(result => result as ConfirmationDialogResult));
	}
}
