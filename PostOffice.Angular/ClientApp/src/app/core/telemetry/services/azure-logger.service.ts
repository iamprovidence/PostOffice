import { Injectable } from '@angular/core';
import { ApplicationInsights } from '@microsoft/applicationinsights-web';
import { of } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable()
export class AzureLoggerService {

	private appInsights: ApplicationInsights;

	constructor() { }

	public async initialize(): Promise<void> {
		await this.initializeApplicationInsights();
	}

	private initializeApplicationInsights(): Promise<object> {
		return of({})
			.pipe(
				tap(() => {
					this.appInsights = new ApplicationInsights({
						config: {
							instrumentationKey: "d6f39865-52cc-4a99-8e97-bde3208d0ebf",
							enableAutoRouteTracking: true, // option to log all route changes
						},
					});
				}),
			)
			.toPromise();
	}

	public logPageView(name?: string, url?: string): void {
		this.appInsights?.trackPageView({
			name: name,
			uri: url,
		});
	}

	public logEvent(name: string, properties?: { [key: string]: any }): void {
		this.appInsights?.trackEvent({ name: name }, properties);
	}

	public logMetric(name: string, average: number, properties?: { [key: string]: any }): void {
		this.appInsights?.trackMetric({ name: name, average: average }, properties);
	}

	public logException(exception: Error, severityLevel?: number): void {
		this.appInsights?.trackException({ exception: exception, severityLevel: severityLevel });
	}

	public logTrace(message: string, properties?: { [key: string]: any }): void {
		this.appInsights?.trackTrace({ message: message }, properties);
	}
}
