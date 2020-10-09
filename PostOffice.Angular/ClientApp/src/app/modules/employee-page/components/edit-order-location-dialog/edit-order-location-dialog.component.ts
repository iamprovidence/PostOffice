import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Location } from '@core/api/models/location';
import { EditOrderLocationData } from './models/edit-order-location-data';

@Component({
	selector: 'app-edit-order-location-dialog',
	templateUrl: './edit-order-location-dialog.component.html',
	styleUrls: ['./edit-order-location-dialog.component.sass'],
})
export class EditOrderLocationDialogComponent implements OnInit {

	public city: string;
	public street: string;

	constructor(
		public dialogRef: MatDialogRef<EditOrderLocationDialogComponent>,
		@Inject(MAT_DIALOG_DATA) public data: EditOrderLocationData,
	) { }

	public ngOnInit(): void { }

	public onCancelClick(): void {
		this.dialogRef.close();
	}

	public onUpdateClick(): void {
		this.dialogRef.close(new Location(this.city, this.street));
	}
}
