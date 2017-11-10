from mongoengine import *


class Planning(Document):
    intitule = StringField(default="")

    zonesModel = ListField(EmbeddedDocumentField('ActionCode'))
    """
    liste de ActionCode.\n
    Voir explications dans ZoneManager et ActionManager.
    """
