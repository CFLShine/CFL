from datetime import date


class ActionManager:
    def __init__(self, actioncode: str, date_: date, matin: bool,  subject):
        self.actioncode = actioncode
        self.subject = subject
        self.matin = matin
        self.date = date_  # :type : datetime

        self.heure = ""
        self.action = ""

    def exe(self):
        """
        Le code contenu dans actioncode
        doit utiliser self.subject, self.date, self.matin
        et assigner une valeur str aux variables self.heure et self.action.
        ex :
        '
        if (self.subject.uneaction.date == date and
                self.matin == True and
                self.subject.uneaction.heure <= time(12, 00)):
            self.heure = self.subject.uneaction.heure
            self.action = 'cette action a lieu'
        '
        """

        exec(self.actioncode)
