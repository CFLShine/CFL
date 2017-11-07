from mongoengine import *


class Planning(Document):
    intitule = StringField(default="")

    """
    Un ActionLogic contient heureCode et actionCode (string),
    le code qui sera executé pour afficher l'eure et l'action
    correspondants à une opération.
    """
    zonesLogic = ListField('ActionLogic', default=list())
