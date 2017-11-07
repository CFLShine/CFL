class ActionManager:
    def __init__(self, actionLogic, subject):
        actionLogic = actionLogic
        subject = subject
        heure = ""
        action = ""

    def exe(self):
        """
        Le code pourvu par actionLogic.heureCode et actionLogic.actionCode
        doit utiliser une variable nomée subject et assigner une valeur str
        aux variables nomées heure et action.
        """

        exec(self.actionLogic.actionCode)
        exec(self.actionLogic.heureCode)
