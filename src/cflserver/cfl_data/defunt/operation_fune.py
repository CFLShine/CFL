from mongoengine import *

import src.cflserver.cfl_data.defunt.defunt as dft

class OperationFune(Document):
    defunt = ReferenceField(dft.Defunt, default=None)
    date = DateTimeField()
    heure = DateTimeField()
    commentaire = StringField(default="")
