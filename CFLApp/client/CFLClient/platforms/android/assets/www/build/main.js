webpackJsonp([3],{

/***/ 104:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return LoginPage; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_ionic_angular__ = __webpack_require__(32);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_ts_md5_dist_md5__ = __webpack_require__(241);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_ts_md5_dist_md5___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_2_ts_md5_dist_md5__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__providers_restapi_service_restapi_service__ = __webpack_require__(41);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__home_home__ = __webpack_require__(79);
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





/**
 * Generated class for the LoginPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */
var LoginPage = (function () {
    function LoginPage(navCtrl, navParams, restApiService) {
        this.navCtrl = navCtrl;
        this.navParams = navParams;
        this.restApiService = restApiService;
    }
    LoginPage.prototype.ionViewDidLoad = function () {
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
    };
    LoginPage.prototype.checkUser = function (username, password) {
        var _this = this;
        if (username == null || password == null)
            return null;
        var md5pass = __WEBPACK_IMPORTED_MODULE_2_ts_md5_dist_md5__["Md5"].hashStr(password);
        for (var i = 0; i < 1000; i++)
            md5pass = __WEBPACK_IMPORTED_MODULE_2_ts_md5_dist_md5__["Md5"].hashStr(md5pass.toString());
        this.restApiService.getData("users").then(function (data) {
            if (!(username in data))
                return null;
            if (data[username].password == md5pass) {
                /*this.secureStorage.create("userid").then((storage:SecureStorageObject) => {
                  storage.set('username', username).catch(
                    err => {alert("Could not store username: " + JSON.stringify(err))});
                  storage.set('password', md5pass.toString()).catch(
                    err => {alert("Could not store username: " + JSON.stringify(err))});
                });*/
                _this.navCtrl.setRoot(__WEBPACK_IMPORTED_MODULE_4__home_home__["a" /* HomePage */]);
            }
        });
    };
    return LoginPage;
}());
LoginPage = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["n" /* Component */])({
        selector: 'page-login',template:/*ion-inline-start:"E:\dev\CFLApp\client\CFLClient\src\pages\login\login.html"*/'<!--\n  Generated template for the LoginPage page.\n\n  See http://ionicframework.com/docs/components/#navigation for more info on\n  Ionic pages and navigation.\n-->\n<ion-header>\n\n  <ion-navbar>\n    <ion-title>login</ion-title>\n  </ion-navbar>\n\n</ion-header>\n\n\n<ion-content padding>\n\n  <ion-input [(ngModel)]="username" type="text" placeholder="Nom d\'utilisateur"></ion-input>\n  <ion-input [(ngModel)]="password" type="password" placeholder="Mot de passe"></ion-input>\n  <button (click)="checkUser(username, password)">Se connecter</button>\n</ion-content>\n'/*ion-inline-end:"E:\dev\CFLApp\client\CFLClient\src\pages\login\login.html"*/,
    }),
    __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_1_ionic_angular__["f" /* NavController */], __WEBPACK_IMPORTED_MODULE_1_ionic_angular__["g" /* NavParams */], __WEBPACK_IMPORTED_MODULE_3__providers_restapi_service_restapi_service__["a" /* RestapiServiceProvider */]])
], LoginPage);

//# sourceMappingURL=login.js.map

/***/ }),

/***/ 105:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return PlanningAstreintePage; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_ionic_angular__ = __webpack_require__(32);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__providers_restapi_service_restapi_service__ = __webpack_require__(41);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_common__ = __webpack_require__(25);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__pipes_astreinte_filter_day_astreinte_filter_day__ = __webpack_require__(80);
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





/**
 * Generated class for the PlanningAstreintePage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */
var PlanningAstreintePage = PlanningAstreintePage_1 = (function () {
    function PlanningAstreintePage(navCtrl, navParams, datepipe, astreinteFilterDay, restapiService) {
        this.navCtrl = navCtrl;
        this.navParams = navParams;
        this.datepipe = datepipe;
        this.astreinteFilterDay = astreinteFilterDay;
        this.restapiService = restapiService;
        this.undertakers = {};
        this.date = new Date().toISOString();
        this.onDatePickerChanged();
    }
    PlanningAstreintePage.prototype.formatDates = function () {
        this.todayOnCall.__children.forEach(function (item) {
            return item.fmtDate = PlanningAstreintePage_1.formatIdDate(item.Id);
        });
    };
    PlanningAstreintePage.formatIdDate = function (id) {
        if (!(id.length == 8))
            id = "0" + id;
        return id.substr(0, 2) + "/" + id.substr(2, 2) + "/" + id.substr(4);
    };
    PlanningAstreintePage.prototype.getUndertakers = function () {
        var _this = this;
        var path = "Personnel";
        this.restapiService.getData(path)
            .then(function (data) {
            _this.undertakers_data = data;
            for (var _i = 0, _a = _this.undertakers_data.__children; _i < _a.length; _i++) {
                var undertaker = _a[_i];
                console.log("id = " + undertaker.Id);
                console.log(undertaker);
                _this.undertakers[undertaker.Id] = undertaker;
            }
        });
    };
    PlanningAstreintePage.prototype.getTodayOnCall = function (year) {
        var _this = this;
        var path = "ASTREINTE/Astreinte" + year;
        this.restapiService.getData(path)
            .then(function (data) {
            _this.todayOnCall = data;
            _this.formatDates();
            _this.getUndertakers();
        });
    };
    PlanningAstreintePage.prototype.onDatePickerChanged = function () {
        this.getTodayOnCall(this.datepipe.transform(this.date, "yyyy"));
    };
    return PlanningAstreintePage;
}());
PlanningAstreintePage = PlanningAstreintePage_1 = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["n" /* Component */])({
        selector: 'page-planning-astreinte',template:/*ion-inline-start:"E:\dev\CFLApp\client\CFLClient\src\pages\planning-astreinte\planning-astreinte.html"*/'<!-- Good default declaration:\n    * gap: is required only on iOS (when using UIWebView) and is needed for JS->native communication\n    * https://ssl.gstatic.com is required only on Android and is needed for TalkBack to function properly\n    * Disables use of eval() and inline scripts in order to mitigate risk of XSS vulnerabilities. To change this:\n        * Enable inline JS: add \'unsafe-inline\' to default-src\n        * Enable eval(): add \'unsafe-eval\' to default-src\n-->\n<meta http-equiv="Content-Security-Policy" content="default-src \'self\' data: gap: https://ssl.gstatic.com; style-src \'self\' \'unsafe-inline\'; media-src *">\n\n<!-- Allow everything but only from the same origin and foo.com -->\n<meta http-equiv="Content-Security-Policy" content="default-src \'self\' foo.com">\n\n<!-- This policy allows everything (eg CSS, AJAX, object, frame, media, etc) except that\n    * CSS only from the same origin and inline styles,\n    * scripts only from the same origin and inline styles, and eval()\n-->\n<meta http-equiv="Content-Security-Policy" content="default-src *; style-src \'self\' \'unsafe-inline\'; script-src \'self\' \'unsafe-inline\' \'unsafe-eval\'">\n\n<!-- Allows XHRs only over HTTPS on the same domain. -->\n<meta http-equiv="Content-Security-Policy" content="default-src \'self\' https:">\n\n<!-- Allow iframe to https://cordova.apache.org/ -->\n<meta http-equiv="Content-Security-Policy" content="default-src \'self\'; frame-src \'self\' https://cordova.apache.org">\n\n<ion-header>\n  <ion-navbar color="secondary">\n    <button ion-button menuToggle>\n      <ion-icon name="menu"></ion-icon>\n    </button>\n    <ion-title>CflApp - Planning des Astreintes</ion-title>\n  </ion-navbar>\n</ion-header>\n\n\n<ion-content padding>\n  <ion-label class="datePicker"><span>Date</span>\n    <!--<ion-input type="date" #datePicker (ionChange)="onDatePickerChanged(date)"></ion-input>-->\n    <ion-datetime (ionChange)="onDatePickerChanged()" displayFormat="MM/YYYY" [(ngModel)]="date"></ion-datetime>\n  </ion-label>\n\n  <div padding class="body-secondary" *ngIf="zones?.err">\n    Pas de contenu pour cette date !\n  </div>\n\n  <ion-content padding class="body-secondary" *ngIf="!zones?.err">\n\n    <!--<ion-list>\n      <ion-item *ngFor="let item of todayOnCall?.__children | orderById: datepipe.transform(date, \'MMyyyy\')">\n        {{item?.fmtDate}} - {{undertakers[item.IdPers]?.Nom}} {{undertakers[item.IdPers]?.Prenom}}\n      </ion-item>\n    </ion-list>-->\n    <ion-card>\n      <ion-card-header>\n        Astreintes pour la date {{datepipe.transform(date, "MM/yyyy")}}\n      </ion-card-header>\n      <ion-list>\n        <ion-item *ngFor="let item of todayOnCall?.__children | orderById: datepipe.transform(date, \'MMyyyy\')">\n          {{item?.fmtDate}} - {{undertakers[item.IdPers]?.Nom}} {{undertakers[item.IdPers]?.Prenom}}\n        </ion-item>\n      </ion-list>\n    </ion-card>\n  </ion-content>\n</ion-content>\n'/*ion-inline-end:"E:\dev\CFLApp\client\CFLClient\src\pages\planning-astreinte\planning-astreinte.html"*/,
    }),
    __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_1_ionic_angular__["f" /* NavController */], __WEBPACK_IMPORTED_MODULE_1_ionic_angular__["g" /* NavParams */],
        __WEBPACK_IMPORTED_MODULE_3__angular_common__["d" /* DatePipe */],
        __WEBPACK_IMPORTED_MODULE_4__pipes_astreinte_filter_day_astreinte_filter_day__["a" /* AstreinteFilterDayPipe */],
        __WEBPACK_IMPORTED_MODULE_2__providers_restapi_service_restapi_service__["a" /* RestapiServiceProvider */]])
], PlanningAstreintePage);

var PlanningAstreintePage_1;
//# sourceMappingURL=planning-astreinte.js.map

/***/ }),

/***/ 114:
/***/ (function(module, exports) {

function webpackEmptyAsyncContext(req) {
	// Here Promise.resolve().then() is used instead of new Promise() to prevent
	// uncatched exception popping up in devtools
	return Promise.resolve().then(function() {
		throw new Error("Cannot find module '" + req + "'.");
	});
}
webpackEmptyAsyncContext.keys = function() { return []; };
webpackEmptyAsyncContext.resolve = webpackEmptyAsyncContext;
module.exports = webpackEmptyAsyncContext;
webpackEmptyAsyncContext.id = 114;

/***/ }),

/***/ 156:
/***/ (function(module, exports, __webpack_require__) {

var map = {
	"../pages/disponibilites/disponibilites.module": [
		272,
		0
	],
	"../pages/login/login.module": [
		273,
		2
	],
	"../pages/planning-astreinte/planning-astreinte.module": [
		274,
		1
	]
};
function webpackAsyncContext(req) {
	var ids = map[req];
	if(!ids)
		return Promise.reject(new Error("Cannot find module '" + req + "'."));
	return __webpack_require__.e(ids[1]).then(function() {
		return __webpack_require__(ids[0]);
	});
};
webpackAsyncContext.keys = function webpackAsyncContextKeys() {
	return Object.keys(map);
};
webpackAsyncContext.id = 156;
module.exports = webpackAsyncContext;

/***/ }),

/***/ 199:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_platform_browser_dynamic__ = __webpack_require__(200);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__app_module__ = __webpack_require__(218);


Object(__WEBPACK_IMPORTED_MODULE_0__angular_platform_browser_dynamic__["a" /* platformBrowserDynamic */])().bootstrapModule(__WEBPACK_IMPORTED_MODULE_1__app_module__["a" /* AppModule */]);
//# sourceMappingURL=main.js.map

/***/ }),

/***/ 218:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppModule; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_platform_browser__ = __webpack_require__(26);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_ionic_angular__ = __webpack_require__(32);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__app_component__ = __webpack_require__(260);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__pages_home_home__ = __webpack_require__(79);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__ionic_native_status_bar__ = __webpack_require__(196);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__ionic_native_splash_screen__ = __webpack_require__(198);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__providers_restapi_service_restapi_service__ = __webpack_require__(41);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__angular_http__ = __webpack_require__(78);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9__angular_common__ = __webpack_require__(25);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_10__pipes_zone_filter_zone_filter__ = __webpack_require__(269);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_11__ionic_native_date_picker__ = __webpack_require__(270);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_12__pages_planning_astreinte_planning_astreinte__ = __webpack_require__(105);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_13__pipes_astreinte_filter_day_astreinte_filter_day__ = __webpack_require__(80);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_14__pipes_order_by_id_order_by_id__ = __webpack_require__(271);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_15__pages_login_login__ = __webpack_require__(104);
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
















var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_1__angular_core__["L" /* NgModule */])({
        declarations: [
            __WEBPACK_IMPORTED_MODULE_3__app_component__["a" /* MyApp */],
            __WEBPACK_IMPORTED_MODULE_4__pages_home_home__["a" /* HomePage */],
            __WEBPACK_IMPORTED_MODULE_15__pages_login_login__["a" /* LoginPage */],
            __WEBPACK_IMPORTED_MODULE_12__pages_planning_astreinte_planning_astreinte__["a" /* PlanningAstreintePage */],
            __WEBPACK_IMPORTED_MODULE_10__pipes_zone_filter_zone_filter__["a" /* ZoneFilterPipe */],
            __WEBPACK_IMPORTED_MODULE_13__pipes_astreinte_filter_day_astreinte_filter_day__["a" /* AstreinteFilterDayPipe */],
            __WEBPACK_IMPORTED_MODULE_14__pipes_order_by_id_order_by_id__["a" /* OrderByIdPipe */]
        ],
        imports: [
            __WEBPACK_IMPORTED_MODULE_0__angular_platform_browser__["a" /* BrowserModule */],
            __WEBPACK_IMPORTED_MODULE_8__angular_http__["b" /* HttpModule */],
            __WEBPACK_IMPORTED_MODULE_2_ionic_angular__["c" /* IonicModule */].forRoot(__WEBPACK_IMPORTED_MODULE_3__app_component__["a" /* MyApp */], {}, {
                links: [
                    { loadChildren: '../pages/disponibilites/disponibilites.module#DisponibilitesPageModule', name: 'DisponibilitesPage', segment: 'disponibilites', priority: 'low', defaultHistory: [] },
                    { loadChildren: '../pages/login/login.module#LoginPageModule', name: 'LoginPage', segment: 'login', priority: 'low', defaultHistory: [] },
                    { loadChildren: '../pages/planning-astreinte/planning-astreinte.module#PlanningAstreintePageModule', name: 'PlanningAstreintePage', segment: 'planning-astreinte', priority: 'low', defaultHistory: [] }
                ]
            })
        ],
        bootstrap: [__WEBPACK_IMPORTED_MODULE_2_ionic_angular__["a" /* IonicApp */]],
        entryComponents: [
            __WEBPACK_IMPORTED_MODULE_3__app_component__["a" /* MyApp */],
            __WEBPACK_IMPORTED_MODULE_4__pages_home_home__["a" /* HomePage */],
            __WEBPACK_IMPORTED_MODULE_15__pages_login_login__["a" /* LoginPage */],
            __WEBPACK_IMPORTED_MODULE_12__pages_planning_astreinte_planning_astreinte__["a" /* PlanningAstreintePage */]
        ],
        providers: [
            __WEBPACK_IMPORTED_MODULE_5__ionic_native_status_bar__["a" /* StatusBar */],
            __WEBPACK_IMPORTED_MODULE_6__ionic_native_splash_screen__["a" /* SplashScreen */],
            __WEBPACK_IMPORTED_MODULE_9__angular_common__["d" /* DatePipe */],
            __WEBPACK_IMPORTED_MODULE_10__pipes_zone_filter_zone_filter__["a" /* ZoneFilterPipe */],
            __WEBPACK_IMPORTED_MODULE_13__pipes_astreinte_filter_day_astreinte_filter_day__["a" /* AstreinteFilterDayPipe */],
            __WEBPACK_IMPORTED_MODULE_14__pipes_order_by_id_order_by_id__["a" /* OrderByIdPipe */],
            __WEBPACK_IMPORTED_MODULE_11__ionic_native_date_picker__["a" /* DatePicker */],
            { provide: __WEBPACK_IMPORTED_MODULE_1__angular_core__["v" /* ErrorHandler */], useClass: __WEBPACK_IMPORTED_MODULE_2_ionic_angular__["b" /* IonicErrorHandler */] },
            __WEBPACK_IMPORTED_MODULE_7__providers_restapi_service_restapi_service__["a" /* RestapiServiceProvider */]
        ]
    })
], AppModule);

//# sourceMappingURL=app.module.js.map

/***/ }),

/***/ 260:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return MyApp; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_ionic_angular__ = __webpack_require__(32);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__ionic_native_status_bar__ = __webpack_require__(196);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__ionic_native_splash_screen__ = __webpack_require__(198);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__pages_home_home__ = __webpack_require__(79);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__pages_planning_astreinte_planning_astreinte__ = __webpack_require__(105);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__pages_login_login__ = __webpack_require__(104);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__angular_http__ = __webpack_require__(78);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__providers_restapi_service_restapi_service__ = __webpack_require__(41);
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};









var MyApp = (function () {
    function MyApp(platform, statusBar, splashScreen, http, restApiService) {
        this.platform = platform;
        this.statusBar = statusBar;
        this.splashScreen = splashScreen;
        this.http = http;
        this.restApiService = restApiService;
        this.rootPage = __WEBPACK_IMPORTED_MODULE_6__pages_login_login__["a" /* LoginPage */];
        this.initializeApp();
        // used for an example of ngFor and navigation
        this.pages = [
            { title: 'Planning Pompes Funèbres', component: __WEBPACK_IMPORTED_MODULE_4__pages_home_home__["a" /* HomePage */] },
            { title: 'Planning Astreintes', component: __WEBPACK_IMPORTED_MODULE_5__pages_planning_astreinte_planning_astreinte__["a" /* PlanningAstreintePage */] }
        ];
    }
    MyApp.prototype.checkUser = function (userid, password) {
        var _this = this;
        this.restApiService.getData("users").then(function (data) {
            if (data[userid].password == password)
                _this.rootPage = __WEBPACK_IMPORTED_MODULE_4__pages_home_home__["a" /* HomePage */];
        });
    };
    MyApp.prototype.initializeApp = function () {
        var _this = this;
        this.platform.ready().then(function () {
            // Okay, so the platform is ready and our plugins are available.
            // Here you can do any higher level native things you might need.
            _this.statusBar.styleDefault();
            _this.splashScreen.hide();
            //this.checkUser(obj.connected, obj.password);
        });
    };
    MyApp.prototype.openPage = function (page) {
        // Reset the content nav to have just this page
        // we wouldn't want the back button to show in this scenario
        this.nav.setRoot(page.component);
    };
    return MyApp;
}());
__decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_14" /* ViewChild */])(__WEBPACK_IMPORTED_MODULE_1_ionic_angular__["e" /* Nav */]),
    __metadata("design:type", __WEBPACK_IMPORTED_MODULE_1_ionic_angular__["e" /* Nav */])
], MyApp.prototype, "nav", void 0);
MyApp = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["n" /* Component */])({template:/*ion-inline-start:"E:\dev\CFLApp\client\CFLClient\src\app\app.html"*/'<ion-menu [content]="content">\n  <ion-header>\n    <ion-toolbar>\n      <ion-title>Menu</ion-title>\n    </ion-toolbar>\n  </ion-header>\n\n  <ion-content>\n    <ion-list>\n      <button menuClose ion-item *ngFor="let p of pages" (click)="openPage(p)">\n        {{p.title}}\n      </button>\n    </ion-list>\n  </ion-content>\n\n</ion-menu>\n\n<!-- Disable swipe-to-go-back because it\'s poor UX to combine STGB with side menus -->\n<ion-nav [root]="rootPage" #content swipeBackEnabled="false"></ion-nav>'/*ion-inline-end:"E:\dev\CFLApp\client\CFLClient\src\app\app.html"*/
    }),
    __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_1_ionic_angular__["h" /* Platform */], __WEBPACK_IMPORTED_MODULE_2__ionic_native_status_bar__["a" /* StatusBar */], __WEBPACK_IMPORTED_MODULE_3__ionic_native_splash_screen__["a" /* SplashScreen */],
        __WEBPACK_IMPORTED_MODULE_7__angular_http__["a" /* Http */], __WEBPACK_IMPORTED_MODULE_8__providers_restapi_service_restapi_service__["a" /* RestapiServiceProvider */]])
], MyApp);

//# sourceMappingURL=app.component.js.map

/***/ }),

/***/ 269:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ZoneFilterPipe; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};

/**
 * Generated class for the ZoneFilterPipe pipe.
 *
 * See https://angular.io/api/core/Pipe for more info on Angular Pipes.
 */
var ZoneFilterPipe = (function () {
    function ZoneFilterPipe() {
    }
    /**
     *  Renvoie les éléments du matin (DemiJour: "0") ou de l'après-midi (DemiJour: "1")
     */
    ZoneFilterPipe.prototype.transform = function (items, demijour) {
        if (items == null) {
            return [];
        }
        return items.filter(function (item) { return item.DemiJour != demijour; });
    };
    return ZoneFilterPipe;
}());
ZoneFilterPipe = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["W" /* Pipe */])({
        name: 'zoneFilterPipe',
        pure: false
    })
], ZoneFilterPipe);

//# sourceMappingURL=zone-filter.js.map

/***/ }),

/***/ 271:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return OrderByIdPipe; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__astreinte_filter_day_astreinte_filter_day__ = __webpack_require__(80);
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};


/**
 * Generated class for the OrderByIdPipe pipe.
 *
 * See https://angular.io/api/core/Pipe for more info on Angular Pipes.
 */
var OrderByIdPipe = (function () {
    function OrderByIdPipe() {
    }
    /**
     * Takes a value and makes it lowercase.
     */
    OrderByIdPipe.prototype.transform = function (items, date) {
        if (items == null)
            return [];
        var arr = new __WEBPACK_IMPORTED_MODULE_1__astreinte_filter_day_astreinte_filter_day__["a" /* AstreinteFilterDayPipe */]().transform(items, date);
        arr.sort(function (a, b) {
            console.log("new cmp");
            console.log(a.fmtDate);
            console.log(b.fmtDate);
            if (new Date(a.fmtDate).getTime() < new Date(b.fmtDate).getTime()) {
                return -1;
            }
            else if (new Date(a.fmtDate).getTime() == new Date(b.fmtDate).getTime())
                return 0;
            else
                return 1;
        });
        console.log(arr);
        return arr;
    };
    return OrderByIdPipe;
}());
OrderByIdPipe = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["W" /* Pipe */])({
        name: 'orderById',
    })
], OrderByIdPipe);

//# sourceMappingURL=order-by-id.js.map

/***/ }),

/***/ 41:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return RestapiServiceProvider; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_http__ = __webpack_require__(78);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_rxjs_add_operator_map__ = __webpack_require__(242);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_rxjs_add_operator_map___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_2_rxjs_add_operator_map__);
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



/*
  Generated class for the RestapiServiceProvider provider.

  See https://angular.io/guide/dependency-injection for more info on providers
  and Angular DI.
*/
var RestapiServiceProvider = (function () {
    function RestapiServiceProvider(http) {
        this.http = http;
        //apiUrl = 'http://176.129.40.225:5003/data/'; //mobile url
        this.apiUrl = '/api/data/'; //browser url
        console.log('Hello RestapiServiceProvider Provider');
    }
    RestapiServiceProvider.prototype.getData = function (path) {
        var _this = this;
        console.log("Acessing " + this.apiUrl + path);
        return new Promise(function (resolve) {
            _this.http.get(_this.apiUrl + path)
                .map(function (res) { return res.json(); })
                .subscribe(function (data) {
                resolve(data);
            });
        });
    };
    return RestapiServiceProvider;
}());
RestapiServiceProvider = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["B" /* Injectable */])(),
    __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_1__angular_http__["a" /* Http */]])
], RestapiServiceProvider);

//# sourceMappingURL=restapi-service.js.map

/***/ }),

/***/ 79:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return HomePage; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_ionic_angular__ = __webpack_require__(32);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__providers_restapi_service_restapi_service__ = __webpack_require__(41);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_common__ = __webpack_require__(25);
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




var HomePage = (function () {
    function HomePage(navCtrl, restapiService, datepipe) {
        this.navCtrl = navCtrl;
        this.restapiService = restapiService;
        this.datepipe = datepipe;
        this.defunts = {};
        this.shownItems = [];
        this.date = new Date().toISOString();
        this.getTodayZone();
    }
    HomePage.prototype.updateDeads = function () {
        for (var _i = 0, _a = this.zones.__children; _i < _a.length; _i++) {
            var action = _a[_i];
            this.getDead(action.CodeDft);
        }
    };
    HomePage.prototype.getDead = function (id) {
        var _this = this;
        var path = "DEFUNTS/" + id;
        this.restapiService.getData(path)
            .then(function (data) {
            _this.defunts[id] = data;
        });
    };
    HomePage.prototype.getTodayZone = function () {
        var _this = this;
        var path = "AGENDAPF/Zones";
        path += this.datepipe.transform(this.date, "ddMMyyyy");
        this.restapiService.getData(path)
            .then(function (data) {
            _this.zones = data;
            _this.updateDeads();
        });
    };
    HomePage.prototype.onDatePickerChanged = function () {
        this.getTodayZone();
    };
    HomePage.prototype.toggleItem = function (item) {
        if (this.isItemShown(item)) {
            this.shownItems.splice(this.shownItems.indexOf(item), 1);
        }
        else {
            this.shownItems.push(item);
        }
    };
    HomePage.prototype.isItemShown = function (item) {
        return this.shownItems.some(function (x) { return x == item; });
    };
    return HomePage;
}());
HomePage = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["n" /* Component */])({
        selector: 'page-home',template:/*ion-inline-start:"E:\dev\CFLApp\client\CFLClient\src\pages\home\home.html"*/'\n<ion-header>\n  <ion-navbar color="secondary">\n    <button ion-button menuToggle>\n      <ion-icon name="menu"></ion-icon>\n    </button>\n    <ion-title>CflApp - Planning Pompes Funèbres</ion-title>\n  </ion-navbar>\n</ion-header>\n\n<ion-content padding class="body">\n  <ion-label class="datePicker"><span>Date</span>\n    <!--<ion-input type="date" #datePicker (ionChange)="onDatePickerChanged(date)"></ion-input>-->\n    <ion-datetime (ionChange)="onDatePickerChanged()" displayFormat="DD/MM/YYYY" [(ngModel)]="date"></ion-datetime>\n  </ion-label>\n\n  <div padding class="body-secondary" *ngIf="zones?.err">\n    Pas de contenu pour cette date !\n  </div>\n\n  <ion-content padding class="body-secondary" *ngIf="!zones?.err">\n    <h1>Matinée du {{datepipe.transform(date, "dd/MM/yyyy")}}</h1>\n    <ion-list row-no-padding>\n      <ion-item *ngFor="let d of zones?.__children | zoneFilterPipe:\'0\'" (click)="toggleItem(d)" class="zone"\n                color="secondary" no-lines row-no-padding>\n        <div class="zoneHeader">\n          <div>\n            <span class="defunt">{{defunts[d.CodeDft]?.DefuntNom}} {{defunts[d.CodeDft]?.DefuntPrenom}}</span>\n            <span *ngIf="d.HeureComment != \':\'">- {{d.HeureComment}}</span><br>\n            <span class="ordo">{{d.Ordo}}</span>\n          </div>\n          <div>\n            <!--<span class="police-true" *ngIf="d.police">Police</span>\n            <span class="police-false" *ngIf="!d.police">Police</span>-->\n\n            <button class="expand">{{isItemShown(d) ? "-" : "+"}}</button>\n          </div>\n        </div>\n        <ion-list *ngIf="isItemShown(d)" row-no-padding class="undertakers">\n          <span class="undertaker {{d.EquipeOk_0}}">{{d.Equipe_0 ? d.Equipe_0 : "-"}}</span>\n          <span class="undertaker {{d.EquipeOk_1}}">{{d.Equipe_1 ? d.Equipe_1 : "-"}}</span>\n          <span class="undertaker {{d.EquipeOk_2}}">{{d.Equipe_2 ? d.Equipe_2 : "-"}}</span>\n          <span class="undertaker {{d.EquipeOk_3}}">{{d.Equipe_3 ? d.Equipe_3 : "-"}}</span>\n        </ion-list>\n\n        <ion-list *ngIf="isItemShown(d)" row-no-padding>\n          <hr>\n          <div class="zoneItem">\n            <div class="infosZone">\n              <span *ngIf="defunts[d.CodeDft]?.MEBHeure != \':\'">\n                {{defunts[d.CodeDft]?.MEBHeure}} - Mise en bière {{defunts[d.CodeDft]?.MEBLieu}}\n              </span>\n              <span *ngIf="defunts[d.CodeDft]?.CeremonieHeure != \':\'">\n                {{defunts[d.CodeDft]?.CeremonieHeure}} - Cérémonie {{defunts[d.CodeDft]?.CeremonieType}}\n              </span>\n              <span *ngIf="defunts[d.CodeDft]?.CremationHeure && defunts[d.CodeDft]?.CremationHeure != \':\'">\n                {{defunts[d.CodeDft]?.CremationHeure}} - Crémation\n                <span *ngIf="defunts[d.CodeDft]?.CremationArriveeDe != \'\'">(Arrivée de {{defunts[d.CodeDft]?.CremationArriveeDe}})</span>\n              </span>\n              <span *ngIf="defunts[d.CodeDft]?.InhumationHeure != \':\'">\n                {{defunts[d.CodeDft]?.InhumationHeure}} - Inhumation {{defunts[d.CodeDft]?.InhumationLieu}}\n              </span>\n              <span *ngIf="defunts[d.CodeDft]?.Inhumation2Heure != \':\'">\n                {{defunts[d.CodeDft]?.Inhumation2Heure}} - Inhumation {{defunts[d.CodeDft]?.Inhumation2Lieu}}\n              </span>\n            </div>\n\n            <hr>\n            <span *ngIf="d.Comment_0 != \'\'" class="comment">{{d.Comment_0}}</span>\n            <span *ngIf="d.Comment_0 != \'\'" class="comment">{{d?.Comment_1}}</span>\n          </div>\n        </ion-list>\n      </ion-item>\n    </ion-list>\n\n    <hr>\n\n    <h1>Après-midi du {{datepipe.transform(date, "dd/MM/yyyy")}}</h1>\n    <ion-list row-no-padding>\n      <ion-item *ngFor="let d of zones?.__children | zoneFilterPipe:\'1\'" (click)="toggleItem(d)" class="zone"\n                color="secondary" no-lines row-no-padding>\n        <div class="zoneHeader">\n          <div>\n            <span class="defunt">{{defunts[d.CodeDft]?.DefuntNom}} {{defunts[d.CodeDft]?.DefuntPrenom}}</span>\n            <span *ngIf="d.HeureComment != \':\'">- {{d.HeureComment}}</span><br>\n            <span class="ordo">{{d.Ordo}}</span>\n          </div>\n          <div>\n            <!--<span class="police-true" *ngIf="d.police">Police</span>\n            <span class="police-false" *ngIf="!d.police">Police</span>-->\n\n            <button class="expand">{{isItemShown(d) ? "-" : "+"}}</button>\n          </div>\n        </div>\n        <ion-list *ngIf="isItemShown(d)" row-no-padding class="undertakers">\n          <span class="undertaker {{d.EquipeOk_0}}">{{d.Equipe_0 ? d.Equipe_0 : "-"}}</span>\n          <span class="undertaker {{d.EquipeOk_1}}">{{d.Equipe_1 ? d.Equipe_1 : "-"}}</span>\n          <span class="undertaker {{d.EquipeOk_2}}">{{d.Equipe_2 ? d.Equipe_2 : "-"}}</span>\n          <span class="undertaker {{d.EquipeOk_3}}">{{d.Equipe_3 ? d.Equipe_3 : "-"}}</span>\n        </ion-list>\n\n        <ion-list *ngIf="isItemShown(d)" row-no-padding>\n          <hr>\n          <div class="zoneItem">\n            <div class="infosZone">\n              <span *ngIf="defunts[d.CodeDft]?.MEBHeure != \':\'">\n                {{defunts[d.CodeDft]?.MEBHeure}} - Mise en bière {{defunts[d.CodeDft]?.MEBLieu}}\n              </span>\n              <span *ngIf="defunts[d.CodeDft]?.CeremonieHeure != \':\'">\n                {{defunts[d.CodeDft]?.CeremonieHeure}} - Cérémonie {{defunts[d.CodeDft]?.CeremonieType}}\n              </span>\n              <span *ngIf="defunts[d.CodeDft]?.CremationHeure && defunts[d.CodeDft]?.CremationHeure != \':\'">\n                {{defunts[d.CodeDft]?.CremationHeure}} - Crémation\n                <span *ngIf="defunts[d.CodeDft]?.CremationArriveeDe != \'\'">(Arrivée de {{defunts[d.CodeDft]?.CremationArriveeDe}})</span>\n              </span>\n              <span *ngIf="defunts[d.CodeDft]?.InhumationHeure != \':\'">\n                {{defunts[d.CodeDft]?.InhumationHeure}} - Inhumation {{defunts[d.CodeDft]?.InhumationLieu}}\n              </span>\n              <span *ngIf="defunts[d.CodeDft]?.Inhumation2Heure != \':\'">\n                {{defunts[d.CodeDft]?.Inhumation2Heure}} - Inhumation {{defunts[d.CodeDft]?.Inhumation2Lieu}}\n              </span>\n            </div>\n\n            <hr>\n            <span *ngIf="d.Comment_0 != \'\'" class="comment">{{d.Comment_0}}</span>\n            <span *ngIf="d.Comment_0 != \'\'" class="comment">{{d?.Comment_1}}</span>\n          </div>\n        </ion-list>\n      </ion-item>\n    </ion-list>\n  </ion-content>\n</ion-content>\n'/*ion-inline-end:"E:\dev\CFLApp\client\CFLClient\src\pages\home\home.html"*/
    }),
    __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_1_ionic_angular__["f" /* NavController */], __WEBPACK_IMPORTED_MODULE_2__providers_restapi_service_restapi_service__["a" /* RestapiServiceProvider */], __WEBPACK_IMPORTED_MODULE_3__angular_common__["d" /* DatePipe */]])
], HomePage);

//# sourceMappingURL=home.js.map

/***/ }),

/***/ 80:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AstreinteFilterDayPipe; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__(0);
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};

/**
 * Generated class for the AstreinteFilterDayPipe pipe.
 *
 * See https://angular.io/api/core/Pipe for more info on Angular Pipes.
 */
var AstreinteFilterDayPipe = (function () {
    function AstreinteFilterDayPipe() {
    }
    /**
     * Takes a value and makes it lowercase.
     */
    AstreinteFilterDayPipe.prototype.transform = function (items, date) {
        if (items == null) {
            return [];
        }
        //items.forEach(item => console.log("date:" + date + ", Id: " + item.Id + ", id filtered: " + item.Id.substr((item.Id.length - 6))));
        return items.filter(function (item) {
            return item.Id.substr(item.Id.length - 6) == date;
        });
    };
    return AstreinteFilterDayPipe;
}());
AstreinteFilterDayPipe = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["W" /* Pipe */])({
        name: 'astreinteFilterDay',
    })
], AstreinteFilterDayPipe);

//# sourceMappingURL=astreinte-filter-day.js.map

/***/ })

},[199]);
//# sourceMappingURL=main.js.map