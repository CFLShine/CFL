from mongoengine import *


class Cimetiere(Document):
    lieu = ReferenceField('Lieu', default=None)
    sepultures = ListField(ReferenceField('Sepulture'), default=list)
