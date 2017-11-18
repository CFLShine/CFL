import { Component, ViewChild } from '@angular/core';
import { Nav, Platform } from 'ionic-angular';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';

import { HomePage } from '../pages/home/home';
import {PlanningAstreintePage} from "../pages/planning-astreinte/planning-astreinte";
import {Http} from "@angular/http";
import {RestapiServiceProvider} from "../providers/restapi-service/restapi-service";

@Component({
  templateUrl: 'app.html'
})
export class MyApp {
  @ViewChild(Nav) nav: Nav;

  rootPage: any = HomePage;

  pages: Array<{title: string, component: any}>;

  constructor(public platform: Platform, public statusBar: StatusBar, public splashScreen: SplashScreen,
              public http: Http, public restApiService: RestapiServiceProvider) {
    this.initializeApp();

    // used for an example of ngFor and navigation
    this.pages = [
      { title: 'Planning Pompes Funèbres', component: HomePage },
      { title: 'Planning Astreintes', component: PlanningAstreintePage }
    ];

  }

  checkUser(userid: string, password: string) {
    this.restApiService.getData("users").then(data => {
      if (data[userid].password == password)
        this.rootPage = HomePage;
    });
  }

  initializeApp() {
    this.platform.ready().then(() => {
      // Okay, so the platform is ready and our plugins are available.
      // Here you can do any higher level native things you might need.
      this.statusBar.styleDefault();
      this.splashScreen.hide();

      //this.checkUser(obj.connected, obj.password);

    });
  }

  openPage(page) {
    // Reset the content nav to have just this page
    // we wouldn't want the back button to show in this scenario
    this.nav.setRoot(page.component);
  }
}
