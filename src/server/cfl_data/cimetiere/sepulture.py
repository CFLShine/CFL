from mongoengine import *

import src.server.cfl_data.cimetiere.cimetiere as cimetiere

class Sepulture(Document):
    cimetiere = ReferenceField(cimetiere.Cimetiere, default=None)

    sepultureType = StringField(default="")

    division = StringField(default = "")
    carre = StringField(default = "")
    ligne = StringField(default = "")
    tombe = StringField(default = "")

    monument = BooleanField(default=False)
