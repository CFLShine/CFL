from src.server.cfl_data.planning_journalier.zone import Zone
from src.server.cfl_logic.plannings.actionmanager import ActionManager

class ZoneManager:
    def __init__(self, zone: Zone):
        self.zone = zone
        self.actions = None  # type: list[ActionManager]

    def showactions(self):
        self.actions = list()
        planning = self.zone.page.planning
        subject = self.zone.subject

        for actionlogic in planning.zonesModel:  # planning.zonesModel est une liste<ActionCode>
            actionmanager = ActionManager(actionlogic, subject)
            actionmanager.exe()
            if (actionmanager.heure is not None and
                    actionmanager.action is not None and
                    actionmanager.heure != '' and
                    actionmanager.action != ''):
                yield actionmanager

    def showequipiersByRole(self, role: str):
        if self.zone.equipe is not None:
            for equipier in self.zone.equipe.equipiers:
                if equipier.role == role:
                    yield equipier
