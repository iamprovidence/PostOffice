import { NgModule, Optional, SkipSelf } from '@angular/core';
import { EnvironmentModule } from './environment/environment.module';
import { TelemetryModule } from './telemetry/telemetry.module';

class EnsureModuleLoadedOnceGuard {
	constructor(targetModule: object) {
		if (targetModule) {
			throw new Error(this.getErrorMessage(targetModule.constructor.name));
		}
	}

	private getErrorMessage(moduleName: string): string {
		return `${moduleName} has already been loaded. Import this module in the AppModule only.`;
	}
}

@NgModule({
	imports: [
		EnvironmentModule.forRoot(),
		TelemetryModule.forRoot(),
	],
})
export class CoreModule extends EnsureModuleLoadedOnceGuard {
	constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
		super(parentModule);
	}
}
