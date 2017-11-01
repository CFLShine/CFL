from mongoengine import *

from src.server.cfl_data.coordonnees.adresse import Adresse
from src.server.cfl_data.coordonnees.contacts import Contacts


class Lieu(Document):
    nom = StringField()
    adresse = ReferenceField(Adresse, default = None)
    contacts = ReferenceField(Contacts, default = None)
