import { Component, OnInit } from '@angular/core';
import { OrderApiService } from '@core/api/services/order-api.service';

@Component({
	selector: 'app-client-choose-action',
	templateUrl: './choose-action.component.html',
	styleUrls: ['./choose-action.component.sass'],
})
export class ChooseActionComponent implements OnInit {

	constructor(public orderApiService: OrderApiService) { }

	public ngOnInit(): void { }
}
