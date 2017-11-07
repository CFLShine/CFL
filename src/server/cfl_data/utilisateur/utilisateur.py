from mongoengine import *


class Utilisateur(Document):
    personne = ReferenceField("Personne")
    habilitation = StringField(default="")
    login = ReferenceField('Login', default=None)
    autorisations = ListField(ReferenceField('Autorisation'))