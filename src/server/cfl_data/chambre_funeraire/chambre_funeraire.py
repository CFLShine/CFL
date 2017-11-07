from mongoengine import *


class ChambreFuneraire(Document):
    lieu = ReferenceField('Lieu', default=None)
    salons = ListField(ReferenceField('Salon'), default=list())
    cases = ListField(ReferenceField('Case'), default=list())
