import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import {Md5} from 'ts-md5/dist/md5';
import {RestapiServiceProvider} from "../../providers/restapi-service/restapi-service";
import {HomePage} from "../home/home";

/**
 * Generated class for the LoginPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-login',
  templateUrl: 'login.html',
})
export class LoginPage {
  username: string;
  password: string;

  constructor(public navCtrl: NavController, public navParams: NavParams, public restApiService: RestapiServiceProvider) {
  }

  ionViewDidLoad() {
    this.username = window.localStorage.getItem('username');
    this.password = window.localStorage.getItem('password');

    this.checkUser(this.username, this.password, false);
    /*this.secureStorage.create("userid").then((storage: SecureStorageObject) => {
      storage.get('username').then(data => {
          this.username = data;
          storage.get('password').then(data=> {
            this.password = data;
            this.checkUser(this.username, this.password);
          })
            .catch(err => {});
        },
        err => {}
      );
    })*/
  }

  checkUser(username, password, hash=true) {
    if (username == null || password == null)
      return null;

    let md5pass = password;
    if(hash) {
      md5pass = Md5.hashStr(password);
      for (let i = 0; i < 1000; i++)
        md5pass = Md5.hashStr(md5pass.toString());
    }

    this.restApiService.getData("users").then(data => {
      if (!(username in data))
        return null;

      if (data[username].password == md5pass)
      {
        window.localStorage.setItem('username', username);
        window.localStorage.setItem('password', md5pass.toString());
        /*this.secureStorage.create("userid").then((storage:SecureStorageObject) => {
          storage.set('username', username).catch(
            err => {alert("Could not store username: " + JSON.stringify(err))});
          storage.set('password', md5pass.toString()).catch(
            err => {alert("Could not store username: " + JSON.stringify(err))});
        });*/
        this.navCtrl.setRoot(HomePage);
      }
    });
  }

}
