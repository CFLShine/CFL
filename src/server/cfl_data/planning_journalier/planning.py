from mongoengine import *


class Planning(Document):
    intitule = StringField(default="")

    """
    Le code qui sera éxécuté pour afficher le contenu d'une zone
    """
    zonesLogic = StringField(default="")
