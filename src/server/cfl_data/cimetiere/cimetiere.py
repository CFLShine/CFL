from mongoengine import *

import src.server.cfl_data.cimetiere.sepulture as sepulture


class Cimetiere(Document):
    lieu = ReferenceField('Lieu', default=None)
    sepultures = ListField(ReferenceField(sepulture.Sepulture), default=list)