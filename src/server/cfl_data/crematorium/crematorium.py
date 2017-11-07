from mongoengine import *


class Crematorium(Document):
    lieu = ReferenceField('Lieu', default=None)
    fours = ListField(ReferenceField('Four'), default=list())
    planning = ReferenceField('Planning', default=None)
