import datetime

import src.server.cfl_data.planning_journalier.page as page
import src.server.cfl_data.planning_journalier.planning as plann
import src.server.cfl_data.planning_journalier.zone as zn
import src.server.cfl_logic.document_factory as docfactory
import src.server.sh_core.db_helper.dbobjectsprovider as objectprovider
import src.server.sh_core.db_helper.dbobjectssaver as objectsaver


class PlanningManager:
    def __init__(self, planning: plann.Planning):
        self.planning = planning  # type: plann.Planning
        self.current_page = None  # type: page.Page
        self.current_zone = None  # type: zn.Zone
        self.current_date = None  # type: datetime.date

    def set_current_date(self, date):
        assert self.planning

        self.current_date = date

        lst = objectprovider.getDocuments(page.Page, (),
                                          {'$and': [{'date': date}, {'planning': self.planning}]})
        if lst:
            self.current_page = lst[0]
        else:
            self.get_current_page()

    def get_current_page(self) -> page.Page:
        assert self.planning
        assert self.current_date

        if not self.current_page:
            self.current_page = docfactory.Page()
            self.current_page.planning = self.planning
            self.current_page.date = datetime
        return self.current_page

    def save_current_page(self):
        if self.current_page:
            saver = objectsaver.DocumentSaver()
            saver.save_document_and_subdocuments(self.current_page)

    def set_current_zone(self, zone: zn.Zone):
        assert isinstance(zone, zn.Zone)
        assert self.current_page

        if (not self.is_zone_apresmidi(zone)
                and not self.is_zone_matin(zone)):
            raise Exception("zone ne fait pas partie de la page en cours.")

        self.current_zone = zone

    def add_zone_matin(self):
        zone = self.__get_new_zone()
        self.current_zone = zone
        self.get_current_page().zonesMatin.append(zone)

    def add_zone_apresmidi(self):
        zone = self.__get_new_zone()
        self.current_zone = zone
        self.get_current_page().zones_apresmidi.append(zone)

    def delete_current_zone(self):
        if self.current_zone:
            if self.is_zone_matin(self.current_zone):
                self.current_page.zonesMatin.remove(self.current_zone)

            elif self.is_zone_apresmidi(self.current_zone):
                self.current_page.zonesApresMidi.remove(self.current_zone)

            self.current_zone = None

    def zones_matin(self):
        return self.get_current_page().zonesMatin

    def zones_apresmidi(self):
        return self.get_current_page().zonesApresMidi

    def is_zone_matin(self, zone) -> bool:
        return self.current_page and zone in self.current_page.zonesMatin

    def is_zone_apresmidi(self, zone: zn.Zone) -> bool:
        return self.current_page and zone in self.current_page.zonesApresMidi

    def __get_new_zone(self) -> zn.Zone:
        zone = docfactory.Zone()
        zone.page = self.current_page
        return zone
