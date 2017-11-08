import typing

import src.server.cfl_data.planning_journalier.planning as pl
import src.server.cfl_logic.plannings.planningmanager as planningmanager
import src.server.sh_core.db_helper.dbobjectsprovider as objectsprovider


class PlanningsManager():
    def __init__(self):
        self.plannings = list()  # :type : List[pl.Planning]

    def loadPlannings(self):
        plannings = objectsprovider.getDocuments(pl.Planning)
        self.plannings = list()
        for planninginfo in plannings:
            self.plannings.append(planningmanager.PlanningManager(planninginfo))

    def getPlanningManager(self, intitule: str) -> typing.Union[planningmanager.PlanningManager, None]:
        for plManager in self.plannings:
            if plManager.planning.intitule == intitule:
                return plManager
        return None
