import { Component, OnInit } from '@angular/core';
import { OrderDataService } from '../../services/order-data.service';

@Component({
  selector: 'app-employee-choose-action',
  templateUrl: './choose-action.component.html',
	styleUrls: ['./choose-action.component.sass'],
})
export class ChooseActionComponent implements OnInit {

	// TODO: fix initilization of service
	// to init service before loading any components
  constructor(orderDataService: OrderDataService) { }

  public ngOnInit(): void { }

}
