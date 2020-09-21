import { APP_INITIALIZER, ModuleWithProviders, NgModule } from '@angular/core';
import { EnvironmentService } from './services/environment.service';

@NgModule()
export class EnvironmentModule {
	public static forRoot(): ModuleWithProviders<EnvironmentModule> {
		return {
			ngModule: EnvironmentModule,
			providers: [
				{
					provide: APP_INITIALIZER,
					useFactory: (environmentService: EnvironmentService) => () => environmentService.initialize(),
					deps: [EnvironmentService],
					multi: true,
				},
			],
		};
	}
}
