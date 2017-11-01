from mongoengine import *

import src.cflserver.cfl_data.etat_civil.personne as personne
import src.cflserver.cfl_data.defunt.operation_fune as opfune
import src.cflserver.cfl_data.defunt.deces as deces

class Defunt(Document):

    personne = ReferenceField(personne.Personne, default=None)
    deces = ReferenceField(deces.Deces, default=None)
    situation = StringField(default="") # enumSituation

    operations = ListField(opfune.OperationFune, default=None)
