import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import {RestapiServiceProvider} from "../../providers/restapi-service/restapi-service";
import {DatePipe} from "@angular/common";
import {AstreinteFilterDayPipe} from "../../pipes/astreinte-filter-day/astreinte-filter-day";

/**
 * Generated class for the PlanningAstreintePage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-planning-astreinte',
  templateUrl: 'planning-astreinte.html',
})
export class PlanningAstreintePage {
  date: string;
  undertakers_data: any;
  undertakers: { [id: string] : any } = {};
  todayOnCall: any;

  constructor(public navCtrl: NavController, public navParams: NavParams,
              public datepipe: DatePipe,
              public astreinteFilterDay: AstreinteFilterDayPipe,
              public restapiService: RestapiServiceProvider) {
    this.date = new Date().toISOString();
    this.onDatePickerChanged();
  }

  formatDates() {
    this.todayOnCall.__children.forEach(item =>
    item.fmtDate = PlanningAstreintePage.formatIdDate(item.Id));
  }

  static formatIdDate(id: string) {
    if (!(id.length == 8) )
      id = "0" + id;

    return id.substr(0,2) + "/" + id.substr(2, 2) + "/" + id.substr(4);
  }

  getUndertakers() {
    let path = "Personnel";
    this.restapiService.getData(path)
      .then(data => {
        this.undertakers_data = data;
        for (let undertaker of this.undertakers_data.__children) {
          console.log("id = " + undertaker.Id);
          console.log(undertaker);
          this.undertakers[undertaker.Id] = undertaker;
        }
    });
  }

  getTodayOnCall(year: string) {
    let path = "ASTREINTE/Astreinte" + year;

    this.restapiService.getData(path)
      .then(data => {
        this.todayOnCall = data;
        this.formatDates();
        this.getUndertakers();
      });
  }

  onDatePickerChanged() {
    this.getTodayOnCall(this.datepipe.transform(this.date, "yyyy"));
  }

}
