import { Component } from "@angular/core";
import { RouterOutlet } from '@angular/router';
import { slideRouteAnimation } from './app-routing-animation';

@Component({
	selector: "app-root",
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.sass'],
//	animations: [slideRouteAnimation],
})
export class AppComponent {
/*
	public prepareRoute(outlet: RouterOutlet): boolean {
		return outlet && outlet.activatedRouteData && outlet.activatedRouteData["animation"];
	}*/
}
