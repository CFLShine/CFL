from mongoengine import *


class Pouvoir(Document):
    personne = ReferenceField('Personne', default=None)
    qualite = StringField(default="")  # enumQualite
