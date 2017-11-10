from datetime import datetime


class ActionManager:
    def __init__(self, actioncode: str, date: datetime, matin: bool, operation):
        self.actioncode = actioncode  # :type: str
        self.operation = operation
        self.matin = matin  # :type : bool
        self.date = date  # :type : datetime

        self.heure = ...  # :type : datetime
        self.action = ""  # :type : str

    def exe(self):
        """
        Le code contenu dans actioncode
        doit utiliser self.operation, self.date, self.matin
        et assigner une valeur str aux variables self.heure et self.action.
        ex :
        '
        if (self.operation.operation.date == self.date and       # les operations n'héritent pas d'une classe operation
                self.matin == True and                           # mais ont un composant operation
                self.operation.operation.datetime.time() <= time(12, 00)):
            self.heure = self.operation.operation.datetime.time()
            self.action = 'cette action a lieu'
        '
        """

        exec(self.actioncode)  # builtins.exe(str)
