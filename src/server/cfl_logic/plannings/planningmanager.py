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
        self.currentPage = None  # type: page.Page
        self.currentZone = None  # type: zn.Zone
        self.currentDate = None  # type: datetime.date

    def setCurrentDate(self, date):
        assert self.planning

        self.currentDate = date

        l = objectprovider.getDocuments(page.Page, (), {'$and': [{'date': date}, {'planning': self.planning}]})
        if l:
            self.currentPage = l[0]
        else:
            self.getCurrentPage()

    def getCurrentPage(self) -> page.Page:
        assert self.planning
        assert self.currentDate

        if self.currentPage:
            return self.currentPage

        self.currentPage = docfactory.Page()
        self.currentPage.planning = self.planning
        self.currentPage.date = datetime
        return self.currentPage

    def saveCurrentPage(self):
        if self.currentPage:
            saver = objectsaver.DocumentSaver()
            saver.saveDocumentAndSubDocuments(self.currentPage)

    def setCurrentZone(self, zone: zn.Zone):
        assert isinstance(zone, zn.Zone)
        assert self.currentPage

        if (not self.isZoneApresMidi(zone)
                and not self.isZoneMatin(zone)):
            raise Exception("zone ne fait pas partie de la page en cours.")

        self.currentZone = zone

    def addZoneMatin(self):
        zone = self.__getNewZone()
        self.currentZone = zone
        self.getCurrentPage().zonesMatin.append(zone)

    def addZoneApresMidi(self):
        zone = self.__getNewZone()
        self.currentZone = zone
        self.getCurrentPage().zonesApresMidi.append(zone)

    def deleteCurrentZone(self):
        if self.currentZone:
            if self.isZoneMatin(self.currentZone):
                self.currentPage.zonesMatin.remove(self.currentZone)

            elif self.isZoneApresMidi(self.currentZone):
                self.currentPage.zonesApresMidi.remove(self.currentZone)

            self.currentZone = None

    def zonesMatin(self):
        return self.getCurrentPage().zonesMatin

    def zonesAprem(self):
        return self.getCurrentPage().zonesApresMidi

    def isZoneMatin(self, zone) -> bool:
        return self.currentPage and zone in self.currentPage.zonesMatin

    def isZoneApresMidi(self, zone: zn.Zone) -> bool:
        return self.currentPage and zone in self.currentPage.zonesApresMidi

    def __getNewZone(self) -> zn.Zone:
        zone = docfactory.Zone()
        zone.page = self.currentPage
        return zone
