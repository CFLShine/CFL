from mongoengine import *

from src.server.cfl_data.etat_civil.identite import Identite
from src.server.cfl_data.etat_civil.naissance import Naissance


class Personne(Document):
    identite = ReferenceField(Identite, default = None)
    naissance = ReferenceField(Naissance, default = None)