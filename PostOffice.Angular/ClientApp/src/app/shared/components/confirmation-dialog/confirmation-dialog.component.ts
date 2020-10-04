import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ConfirmationDialogResult } from './enums/confirmation-dialog-result';
import { ConfirmationDialogData } from './models/confirmation-dialog-data';

@Component({
	selector: 'app-confirmation-dialog',
	templateUrl: './confirmation-dialog.component.html',
	styleUrls: ['./confirmation-dialog.component.sass'],
})
export class ConfirmationDialogComponent implements OnInit {

	constructor(
		public dialogRef: MatDialogRef<ConfirmationDialogComponent>,
		@Inject(MAT_DIALOG_DATA) public data: ConfirmationDialogData,
	) { }

	public ngOnInit(): void { }

	public confirm(): void {
		this.dialogRef.close(ConfirmationDialogResult.Confirmed);
	}

	public reject(): void {
		this.dialogRef.close(ConfirmationDialogResult.Rejected);
	}
}
