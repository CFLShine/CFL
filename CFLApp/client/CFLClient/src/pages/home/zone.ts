import {RestapiServiceProvider} from "../../providers/restapi-service/restapi-service";

export class Zone {
  dftId: string;
  day: string;

  restapiService: RestapiServiceProvider;

  dft: any;
  ordo: string;
  equipe: string[];
  datatypelist = ["RDVOrdonnateur", "MEB", "Ceremonie", "Cremation", "Inhumation", "Inhumation2"];
  contextmsg = ["Rendez vous ordonnateur", "Mise en bière", "Cérémonie", "Inhumation", "Inhumation"];
  contextprop = ["", "", "Type", "", ""];
  id: string;

  hasAnyDataForHalfDay(morning: boolean = true) {
    for (let datatype of this.datatypelist) {
      if (this.getData(datatype, morning) != null)
        return true;
    }
    return false;
  }

  getDatas(morning: boolean = true) {
    let datas = [];
    for (let datatype of this.datatypelist) {
      let data = this.getData(datatype, morning);
      if (data != null)
        datas.push(data);
    }
    return datas;
  }

  getData(data: string, morning: boolean = true) {
    if (this.dft == null)
      return null;
    if (this.dft[data + 'Date'] === this.day) {
      if ((+this.dft[data + 'Heure'].substr(0, 2)) <= 12 ? morning : !morning) {
        return this.dft[data + "Heure"] + ' - ' +
          this.contextmsg[this.datatypelist.indexOf(data)] + " " +
          ((this.contextprop[this.datatypelist.indexOf(data)] != "") ?
          this.dft[data + this.contextprop[this.datatypelist.indexOf(data)]] : "") + " " +
          (this.dft[data + 'Lieu'] != null ? this.dft[data + 'Lieu'] : "");
      }
    }

    return null;
  }

  static newGuid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
      var r = Math.random()*16|0, v = c == 'x' ? r : (r&0x3|0x8);
      return v.toString(16);
    });
  }

  getDead(id: string) {
    let path = "DEFUNTS/" + id;

    this.restapiService.getData(path)
      .then(data => {
        let _data: any = data;
        this.dft = data;
        this.dft["InhumationLieu"] = _data.SepultureCimetiere;
        this.dft.Inhumation2Lieu = _data.SepultureCimetiere;
      });
  }

  constructor(day: string, dftId: string, ordo: string, equipe: string[], restapiService: RestapiServiceProvider) {
    this.day = day;
    this.dftId = dftId;
    this.ordo = ordo;
    this.equipe = equipe;
    this.id = Zone.newGuid();

    for (let i = 0; i < this.equipe.length; ++i) {
      if (this.equipe[i] === "")
        this.equipe[i] = "--";
    }

    this.restapiService = restapiService;

    this.getDead(this.dftId);
  }
}
