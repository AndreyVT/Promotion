import { Component, OnInit } from '@angular/core';
import { ValueService } from '../shared/services/value.service';

@Component({
  selector: 'app-promote',
  templateUrl: './promote.component.html',
  styleUrls: ['./promote.component.css']
})
export class PromoteComponent implements OnInit {

  constructor(private valueService: ValueService) { }

  getValues() {
    this.valueService.getValues();
    console.log(this.valueService);
  }

  ngOnInit() {
  }
}
