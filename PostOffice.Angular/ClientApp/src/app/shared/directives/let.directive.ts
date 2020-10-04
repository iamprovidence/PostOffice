import { Directive, Input, TemplateRef, ViewContainerRef } from '@angular/core';

interface LetContext<T> {
	ngLet: T;
}

@Directive({
	selector: '[ngLet]',
})
export class LetDirective<T> {
	private context: LetContext<T> = { ngLet: null };

	constructor(
		viewContainer: ViewContainerRef,
		templateRef: TemplateRef<LetContext<T>>,
	) {
		viewContainer.createEmbeddedView(templateRef, this.context);
	}

	@Input()
	public set ngLet(value: T) {
		this.context.ngLet = value;
	}
}
