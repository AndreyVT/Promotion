import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Month } from '../../classes/month';

@Component({
  selector: 'app-select-month',
  templateUrl: './select-month.component.html',
  styleUrls: ['./select-month.component.css']
})
export class SelectMonthComponent implements OnInit {

  @Input() currentPeriod: Month;
  @Output() periodChanged: EventEmitter<Month> = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

  onButtonLeftClick() {
    this.currentPeriod.monthBack();
    this.periodChanged.emit(this.currentPeriod);
  }

  onButtonRightClick() {
    this.currentPeriod.monthForward();
    this.periodChanged.emit(this.currentPeriod);
  }
}
