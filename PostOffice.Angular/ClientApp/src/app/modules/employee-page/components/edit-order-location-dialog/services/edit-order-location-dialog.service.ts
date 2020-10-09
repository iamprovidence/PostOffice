import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Location } from '@core/api/models/location';
import { Observable } from 'rxjs';
import { map, filter } from 'rxjs/operators';
import { EditOrderLocationData } from '../../edit-order-location-dialog/models/edit-order-location-data';
import { EditOrderLocationDialogComponent } from '../edit-order-location-dialog.component';

@Injectable()
export class EditOrderLocationDialogService {

	constructor(
		private dialog: MatDialog,
	) { }

	public open(data: EditOrderLocationData): Observable<Location> {
		const dialogRef = this.dialog.open(EditOrderLocationDialogComponent, {
			width: '800px',
			data,
		});

		return dialogRef
			.afterClosed()
			.pipe(
				filter((value) => !!value),
				map(result => result as Location),
			);
	}
}
