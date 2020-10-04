import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable()
export class NotificationDialogService {
	private readonly defaultDurationInMilliseconds = 2500;

	constructor(
		private snackBar: MatSnackBar,
	) { }

	public open(message: string, durationInMilliseconds?: number): void {
		const duration = durationInMilliseconds ?? this.defaultDurationInMilliseconds;

		this.snackBar.open(message, null, {
			duration,
		});
	}
}
