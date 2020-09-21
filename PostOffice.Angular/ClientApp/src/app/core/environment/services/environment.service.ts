import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { tap } from 'rxjs/operators';
import { EnvironmentUrls } from '../models/environment-urls';

@Injectable({
	providedIn: 'root',
})
export class EnvironmentService {

	public environmentUrls: EnvironmentUrls;

	public async initialize(): Promise<void> {
		await this.loadEnvironmentData();
	}

	private loadEnvironmentData(): Promise<EnvironmentUrls> {
		const environmentData = JSON.parse(document.getElementById("environmentUrls").textContent) as EnvironmentUrls;

		return of(environmentData)
			.pipe(
				tap(environmentUrls => this.environmentUrls = environmentUrls),
			)
			.toPromise();
	}
}
