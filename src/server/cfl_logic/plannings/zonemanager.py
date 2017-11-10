from datetime import date

from src.server.cfl_data.planning_journalier.zone import Zone
from src.server.cfl_logic.plannings.actionmanager import ActionManager


class ZoneManager:
    def __init__(self, zone: Zone, date_: date, matin: bool):
        self.zone = zone
        self.date = date_
        self.matin = matin  # type : bool

    def showactions(self):
        """
        Construit la liste self.actions d'ActionManager.

        Le document Planning contient l'attribut zonesModel,
        une liste de ActionCode
        Un actionCode cotient du code (str) à executer dans l'objet de classe ActionManager.exe(),\n
        et un nom de classe, le type de l'opération concerné par ce code.\n
        Ce code doit assigner une valeur à ActionManager.heure et ActionManager.action.\n
        Chaque ActionManager est ajouté à cette liste si ses membres heure et action ont
        été pourvu d'une valeur (string non vide) par ActionManager.exe().
        """

        subject = self.zone.subject
        if not subject:
            yield

        planning = self.zone.page.planning
        assert planning

        for operation in subject.operations:
            for actioncode in planning.zonesModel:
                if actioncode.classname == operation.__class__.__name__:
                    actionmanager = ActionManager(actioncode.code, self.date, self.matin, operation)
                    actionmanager.exe()
                    if actionmanager.heure and actionmanager.action:
                        yield actionmanager


    def showequipiers_byrole(self, role: str):
        if self.zone.equipe:
            for equipier in self.zone.equipe.equipiers:
                if equipier.role == role:
                    yield equipier
