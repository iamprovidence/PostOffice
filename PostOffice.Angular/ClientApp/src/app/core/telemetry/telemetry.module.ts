import { APP_INITIALIZER, ModuleWithProviders, NgModule } from '@angular/core';
import { AzureLoggerService } from './services/azure-logger.service';

@NgModule()
export class TelemetryModule {
	public static forRoot(): ModuleWithProviders<TelemetryModule> {
		return {
			ngModule: TelemetryModule,
			providers: [
				{
					provide: APP_INITIALIZER,
					useFactory: (azureLoggerService: AzureLoggerService) => () => azureLoggerService.initialize(),
					deps: [AzureLoggerService],
					multi: true,
				},
			],
		};
	}
}
