import { animate, state, style, transition, trigger } from '@angular/animations';

export const displayEventAnimation =
	trigger('childAnimation', [
		state('void', style({
			height: '0',
			opacity: 0,
		})),
		state('open', style({
			height: '*',
			opacity: 1,
		})),

		transition('* => *', [
			animate('300ms')
		]),
	]);

