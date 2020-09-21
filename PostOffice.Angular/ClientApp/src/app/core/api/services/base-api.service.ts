import { Injectable } from "@angular/core";
import * as signalR from "@microsoft/signalr";
import { from, Observable, of } from "rxjs";
import { EnvironmentService } from "../../environment/services/environment.service";

@Injectable({
	providedIn: "root",
})
export abstract class BaseApiService {
	protected abstract hubSpot: string;

	private hubConnection: signalR.HubConnection;

	constructor(private environmentService: EnvironmentService) {
		this.startConnection();
	}

	private startConnection(): void {
		const hubUrl = `${this.environmentService.environmentUrls.postOfficeApiServerUrl}/${this.hubSpot}`;

		this.hubConnection = new signalR.HubConnectionBuilder()
			.configureLogging(signalR.LogLevel.Information)
			.withUrl(hubUrl, {
				skipNegotiation: true,
				transport: signalR.HttpTransportType.WebSockets,
				accessTokenFactory: () => of("asd").toPromise(),
			})
			.withAutomaticReconnect()
			.build();

		this.hubConnection
			.start()
			.then(_ => console.log("Connected!"))
			.catch(error => console.error(error.toString()));
	}

	public dispose(): void {
		this.stopConnection();
	}

	private stopConnection(): void {
		this.hubConnection
			.stop()
			.then(_ => console.log("Disconnected!"))
			.catch(error => console.error(error.toString()));
	}

	public call<TResult>(uri: string, params?: any): Observable<TResult> {
		return from(this.hubConnection.invoke<TResult>(uri, params));
	}

	public asyncEventAsPromise<T>(eventName: string): Promise<T> {
		return new Promise<T>(resolve => {
			this.hubConnection.on(eventName, argument => resolve(argument));
		});
	}

	public asyncEventAsObservable<T>(eventName: string): Observable<T> {
		return from(this.asyncEventAsPromise<T>(eventName));
	}
}
