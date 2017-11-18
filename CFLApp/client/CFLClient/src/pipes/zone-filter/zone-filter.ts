import { Pipe, PipeTransform } from '@angular/core';

/**
 * Generated class for the ZoneFilterPipe pipe.
 *
 * See https://angular.io/api/core/Pipe for more info on Angular Pipes.
 */
@Pipe({
  name: 'zoneFilterPipe',
  pure: false
})
export class ZoneFilterPipe implements PipeTransform {
  /**
   *  Renvoie les éléments du matin (DemiJour: "0") ou de l'après-midi (DemiJour: "1")
   */
  transform(items: any[], morning: boolean): any {
    if(items == null){
      return [];
    }
    return items.filter(item => item.hasAnyDataForHalfDay(morning));
  }
}
