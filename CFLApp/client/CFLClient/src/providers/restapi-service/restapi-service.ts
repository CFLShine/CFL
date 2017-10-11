import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

/*
  Generated class for the RestapiServiceProvider provider.

  See https://angular.io/guide/dependency-injection for more info on providers
  and Angular DI.
*/
@Injectable()
export class RestapiServiceProvider {
  //apiUrl = 'http://176.129.40.225:5003/data/'; //mobile url
  apiUrl = '/api/data/'; //browser url

  constructor(public http: Http) {
    console.log('Hello RestapiServiceProvider Provider');
  }

  getData(path) {
    console.log("Acessing " + this.apiUrl + path);

    return new Promise(resolve => {
      this.http.get(this.apiUrl + path)
        .map(res => res.json())
        .subscribe(data => {
          resolve(data);
        });
    });
  }
}
