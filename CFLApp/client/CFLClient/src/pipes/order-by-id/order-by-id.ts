import { Pipe, PipeTransform } from '@angular/core';
import {AstreinteFilterDayPipe} from "../astreinte-filter-day/astreinte-filter-day";

/**
 * Generated class for the OrderByIdPipe pipe.
 *
 * See https://angular.io/api/core/Pipe for more info on Angular Pipes.
 */
@Pipe({
  name: 'orderById',
})
export class OrderByIdPipe implements PipeTransform {
  /**
   * Takes a value and makes it lowercase.
   */
  transform(items: any[], date: string) {
    if (items == null)
      return [];

    let arr = new AstreinteFilterDayPipe().transform(items, date);

    arr.sort(function(a, b) {
      console.log("new cmp");
      console.log(a.fmtDate); console.log(b.fmtDate);
      if(new Date(a.fmtDate).getTime() < new Date(b.fmtDate).getTime()) {
        return -1;
      }
      else if (new Date(a.fmtDate).getTime() == new Date(b.fmtDate).getTime())
        return 0;
      else
        return 1;
    });

    console.log(arr);
    return arr;
  }
}
