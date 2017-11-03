from mongoengine import *


class Entreprise(Document):
    lieu = ReferenceField('Lieu', default=None)
    habilitation = StringField(default="")