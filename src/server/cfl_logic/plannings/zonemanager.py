from src.server.cfl_data.planning_journalier.zone import Zone
from src.server.cfl_logic.plannings.actionmanager import ActionManager
from datetime import date


class ZoneManager:
    def __init__(self, zone: Zone, date_: date, matin: bool):
        self.zone = zone
        self.date = date_
        self.matin = matin  # type : bool

    def showactions(self):
        """
        Construit la liste self.actions d'ActionManager.

        Le document Planning contient l'attribut zonesModel,
        une liste de str, chaque item étant du code à executer dans l'objet de classe ActionManager.exe().\n
        Ce code utilise ActionManager.subject pour retirer les données
        dont il a besoin pour assigner une valeur à ActionManager.heure et ActionManager.action.\n
        Chaque ActionManager est ajouté à cette liste si ses membres heure et action ont
        été pourvu d'une valeur (string non vide) par ActionManager.exe().
        """
        planning = self.zone.page.planning

        assert planning

        for actioncode in planning.zonesModel:  # planning.zonesModel est une liste<str>
            actionmanager = ActionManager(actioncode, self.date, self.matin, self.zone.subject)
            actionmanager.exe()
            if actionmanager.heure and actionmanager.action:
                yield actionmanager

    def showequipiers_byrole(self, role: str):
        if self.zone.equipe:
            for equipier in self.zone.equipe.equipiers:
                if equipier.role == role:
                    yield equipier
