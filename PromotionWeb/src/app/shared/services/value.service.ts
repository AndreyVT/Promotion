import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';

@Injectable()
export class ValueService {

  private apiHostValue = environment.api.host + 'values';

  constructor(private router: Router,  private http: HttpClient) {
  }

  getValues() {
    return this.http.get(this.apiHostValue, {});
  }
}
