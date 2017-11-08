from mongoengine import *


class Planning(Document):
    intitule = StringField(default="")

    zonesModel = ListField('ActionCode', default=list())
    """
    liste de ActionCode.\n
    Un ActionCode contient heureCode et actionCode (string),
    le code qui sera executé pour afficher l'heure et l'action
    correspondants à une opération.
    """
