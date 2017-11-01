from mongoengine import *

from src.cflserver.cfl_data.coordonnees.adresse import Adresse
from src.cflserver.cfl_data.coordonnees.contacts import Contacts
from src.cflserver.cfl_data.chambre_funeraire.salon import Salon
from src.cflserver.cfl_data.chambre_funeraire.case import Case

class ChambreFuneraire(Document):
    adresse = ReferenceField(Adresse)
    contacts = ReferenceField(Contacts)
    salons = ListField(Salon, default = list)
    cases = ListField(Case, default = list)

