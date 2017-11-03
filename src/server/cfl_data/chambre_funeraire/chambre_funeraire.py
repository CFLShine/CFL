from mongoengine import *

from src.server.cfl_data.chambre_funeraire.case import Case
from src.server.cfl_data.chambre_funeraire.salon import Salon


class ChambreFuneraire(Document):
    lieu = ReferenceField('Lieu', default=None)
    salons = ListField(Salon, default = list)
    cases = ListField(Case, default = list)

