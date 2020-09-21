import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoreModule } from '@core/core.module';
import { SharedModule } from '@shared/shared.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

@NgModule({
	declarations: [
		AppComponent,
	],
	imports: [
		CoreModule,
		BrowserAnimationsModule,
		AppRoutingModule,
		SharedModule,
	],
	bootstrap: [
		AppComponent,
	],
})
export class AppModule { }
