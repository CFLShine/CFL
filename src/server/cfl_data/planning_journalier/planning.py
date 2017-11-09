from mongoengine import *


class Planning(Document):
    intitule = StringField(default="")

    zonesModel = ListField(str, default=list())
    """
    liste de code.\n
    Voir explications dans ZoneManager et ActionManager.
    """
