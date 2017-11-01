from mongoengine import *

from src.server.cfl_data.chambre_funeraire.case import Case
from src.server.cfl_data.chambre_funeraire.salon import Salon
from src.server.cfl_data.coordonnees.adresse import Adresse
from src.server.cfl_data.coordonnees.contacts import Contacts


class ChambreFuneraire(Document):
    adresse = ReferenceField(Adresse)
    contacts = ReferenceField(Contacts)
    salons = ListField(Salon, default = list)
    cases = ListField(Case, default = list)

