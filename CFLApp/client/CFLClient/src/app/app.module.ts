import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';
import { IonicApp, IonicErrorHandler, IonicModule } from 'ionic-angular';

import { MyApp } from './app.component';
import { HomePage } from '../pages/home/home';

import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { RestapiServiceProvider } from '../providers/restapi-service/restapi-service';

import { HttpModule } from '@angular/http';
import { DatePipe } from '@angular/common'

import { ZoneFilterPipe } from "../pipes/zone-filter/zone-filter";
import {DatePicker} from "@ionic-native/date-picker";
import {PlanningAstreintePage} from "../pages/planning-astreinte/planning-astreinte";
import {AstreinteFilterDayPipe} from "../pipes/astreinte-filter-day/astreinte-filter-day";
import {OrderByIdPipe} from "../pipes/order-by-id/order-by-id";
import {LoginPage} from "../pages/login/login";

@NgModule({
  declarations: [
    MyApp,
    HomePage,
    LoginPage,
    PlanningAstreintePage,
    ZoneFilterPipe,
    AstreinteFilterDayPipe,
    OrderByIdPipe
  ],
  imports: [
    BrowserModule,
    HttpModule,
    IonicModule.forRoot(MyApp)
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    HomePage,
    LoginPage,
    PlanningAstreintePage
  ],
  providers: [
    StatusBar,
    SplashScreen,
    DatePipe,
    ZoneFilterPipe,
    AstreinteFilterDayPipe,
    OrderByIdPipe,
    DatePicker,
    {provide: ErrorHandler, useClass: IonicErrorHandler},
    RestapiServiceProvider
  ]
})
export class AppModule {}
