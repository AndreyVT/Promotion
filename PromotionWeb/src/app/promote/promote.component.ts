import { Component, OnInit } from '@angular/core';
import { ValueService } from '../shared/services/value.service';
import { Month } from '../shared/classes/month';

@Component({
  selector: 'app-promote',
  templateUrl: './promote.component.html',
  styleUrls: ['./promote.component.css']
})
export class PromoteComponent implements OnInit {

  currentPeriod: Month;

  constructor(private valueService: ValueService) { }

  getValues() {
    this.valueService.getValues();
    console.log(this.valueService);
  }

  ngOnInit() {
    this.currentPeriod = new Month(new Date());
  }

  onPeriodChanged($event: Month) {
    console.log('Event: ', $event);
  }
}
