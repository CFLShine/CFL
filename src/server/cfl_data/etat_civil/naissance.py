from mongoengine import *

from src.server.cfl_data.coordonnees.lieu import Lieu


class Naissance(Document):
    lieu = ReferenceField(Lieu, default = None)
    date = DateTimeField(default = None)