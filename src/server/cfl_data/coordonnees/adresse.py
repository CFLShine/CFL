from mongoengine import *

from src.server.cfl_data.coordonnees.commune import Commune


class Adresse(Document):
    adresse1 = StringField(default = "")
    adresse2 = StringField(default = "")
    commune  = ReferenceField(Commune, default = None)
    pays     = StringField(default = "")