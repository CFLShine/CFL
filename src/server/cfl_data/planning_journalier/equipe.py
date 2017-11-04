from mongoengine import *


class Equipier(EmbeddedDocument):
    role = StringField(default="")
    personne = ReferenceField('User', default=None)


class Equipe(EmbeddedDocument):
    equipiers = ListField(Equipier, default=list)
