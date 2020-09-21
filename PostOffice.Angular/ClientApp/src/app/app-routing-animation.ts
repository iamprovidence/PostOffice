// tslint:disable: only-arrow-functions
import { animate, AnimationQueryMetadata, query, style, transition, trigger } from '@angular/animations';

export const slideRouteAnimation =
	trigger('routeAnimations', [
		transition('* <=> *', animateRouting()),
	]);

function animateRouting(): AnimationQueryMetadata[] {
	return [
		query(":enter, :leave", [
			style({
				position: 'absolute',
				left: 0,
				width: "100%",
				opacity: 0,
				transform: "scale(0) translateY(100%)",
			}),
		]),

		query(":enter", [
			animate('600ms ease', style({
				opacity: 0,

				transform: "scale(1) translateY(0)",
			})),
		]),
	];
}
