import {Component} from '@angular/core';
import { NavController } from 'ionic-angular';
import {RestapiServiceProvider} from "../../providers/restapi-service/restapi-service";
import {DatePipe} from "@angular/common";
import {Zone} from "./zone";

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {
  zones: Zone[] = [];
  defunts: { [id: string] : any } = {};

  date: string;

  shownItems: any[] = [];

  getTodayZone() {
    let path = "AGENDAPF/Zones";
    path += this.datepipe.transform(this.date, "ddMMyyyy");
    this.zones.length = 0;

    this.restapiService.getData(path)
      .then(data => {
        let _data: any = data;
        for (let z of _data.__children) {
          this.zones.push(new Zone(_data.JourDate, z.CodeDft, z.Ordo,
            [z.Equipe_0, z.Equipe_1, z.Equipe_2, z.Equipe_3], this.restapiService));
        }
      });
  }

  onDatePickerChanged() {
    this.getTodayZone();
  }

  toggleItem(item) {
    console.log(item);
    if(this.isItemShown(item)) {
      this.shownItems.splice(this.shownItems.indexOf(item), 1);
    }
    else {
      this.shownItems.push(item);
    }
  }

  isItemShown(item) {
    return this.shownItems.some(x=> x == item);
  }

  constructor(public navCtrl: NavController, public restapiService: RestapiServiceProvider, public datepipe: DatePipe) {
    this.date = new Date().toISOString();
    this.getTodayZone();
  }
}
