from src.server.cfl_data.planning_journalier.actioncode import ActionCode


class ActionManager:
    def __init__(self, actioncode: ActionCode, subject):
        self.actionCode = actioncode
        self.subject = subject
        self.heure = ""
        self.action = ""

    def exe(self):
        """
        Le code pourvu par actionLogic.heureCode et actionLogic.actionCode
        doit utiliser une variable nomée subject et assigner une valeur str
        aux variables nomées heure et action.
        """

        exec(self.actionCode.actionCode)
        exec(self.actionCode.heureCode)
