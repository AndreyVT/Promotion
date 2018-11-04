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
    this.http.get(this.apiHostValue, {})
      .subscribe(
        (data: any) => {
          console.log(data);
        },
        error => console.log(error)
      );
  }
}
