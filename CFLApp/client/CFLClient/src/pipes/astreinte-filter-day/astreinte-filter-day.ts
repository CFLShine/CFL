import { Pipe, PipeTransform } from '@angular/core';

/**
 * Generated class for the AstreinteFilterDayPipe pipe.
 *
 * See https://angular.io/api/core/Pipe for more info on Angular Pipes.
 */
@Pipe({
  name: 'astreinteFilterDay',
})
export class AstreinteFilterDayPipe implements PipeTransform {
  /**
   * Takes a value and makes it lowercase.
   */
  transform(items: any[], date: string) {
    if(items == null){
      return [];
    }

    //items.forEach(item => console.log("date:" + date + ", Id: " + item.Id + ", id filtered: " + item.Id.substr((item.Id.length - 6))));
    return items.filter(item =>
      item.Id.substr(item.Id.length - 6) == date)
  }
}
