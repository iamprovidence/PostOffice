import { Injectable } from "@angular/core";
import * as signalR from "@microsoft/signalr";
import { from, Observable, of, fromEvent } from "rxjs";
import { tap } from 'rxjs/operators';
import { EnvironmentService } from "../../environment/services/environment.service";

@Injectable({
	providedIn: "root",
})
export abstract class BaseApiService {
	protected hubSpot: string;

	private hubConnection: signalR.HubConnection;

	constructor(private environmentService: EnvironmentService) {
		this.initialize();
		this.createConnection();
		this.startConnection();
		this.subscribeToEvents();
	}

	protected abstract initialize(): void;
	protected abstract subscribeToEvents(): void;

	private createConnection(): void {
		const hubUrl = `${this.environmentService.environmentUrls.postOfficeApiServerUrl}/${this.hubSpot}`;

		this.hubConnection = new signalR.HubConnectionBuilder()
			.configureLogging(signalR.LogLevel.Information)
			.withUrl(hubUrl, {
				skipNegotiation: true,
				transport: signalR.HttpTransportType.WebSockets,
				accessTokenFactory: () => of("add_token_here").toPromise(), // TODO: add token
			})
			.configureLogging(signalR.LogLevel.Debug)
			.withAutomaticReconnect()
			.build();
	}

	private startConnection(): void {
		this.hubConnection
			.start()
			.then(_ => console.log("Connected!"))
			.catch(error => console.error(error.toString()));

	}

	private stopConnection(): void {
		this.hubConnection
			.stop()
			.then(_ => console.log("Disconnected!"))
			.catch(error => console.error(error.toString()));
	}

	public connect(): void {
		this.startConnection();
	}

	public dispose(): void {
		this.stopConnection();
	}

	protected on<TResult>(responseEvent: string): Observable<TResult> {
		return fromEvent<TResult>(this.hubConnection, responseEvent)
			.pipe(tap(value => console.log('[API] on event -response-', value)));
	}

	protected call<TResult>(uri: string, ...params: any[]): Observable<TResult> {
		console.log('[API] call', { hub: this.hubSpot, uri, params });

		return from(this.hubConnection.invoke<TResult>(uri, ...params))
			.pipe(tap(value => console.log('[API] call -response-', value)));
	}

	protected callAsync<TResult>(requestUri: string, responseEvent: string, ...params: any[]): Observable<TResult> {
		console.log('[API] call async', { hub: this.hubSpot, requestUri, responseEvent, params });

		this.hubConnection.invoke(requestUri, ...params);

		return this.asyncEventAsObservable<TResult>(responseEvent)
			.pipe(tap(value => console.log('[API] call async -response-', { hub: this.hubSpot, requestUri, value })));
	}

	protected asyncEventAsObservable<T>(eventName: string): Observable<T> {
		return from(this.asyncEventAsPromise<T>(eventName));
	}

	protected asyncEventAsPromise<T>(eventName: string): Promise<T> {
		return new Promise<T>(resolve => {
			this.hubConnection.on(eventName, argument => resolve(argument));
		});
	}
}
