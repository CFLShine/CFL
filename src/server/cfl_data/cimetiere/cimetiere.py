from mongoengine import *

import src.server.cfl_data.coordonnees.lieu as lieu
import src.server.cfl_data.cimetiere.sepulture as sepulture

class Cimetiere(Document):
    lieu = ReferenceField(lieu.Lieu, default=None)
    sepultures = DateTimeField(ReferenceField(sepulture.Sepulture), default=list)