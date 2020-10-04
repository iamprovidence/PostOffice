import { Injectable, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable()
export abstract class BaseComponent implements OnInit, OnDestroy {
	protected $unsubscribe = new Subject();

	public ngOnInit(): void {}

	public ngOnDestroy(): void {
		this.$unsubscribe.next();
		this.$unsubscribe.complete();
	}
}
