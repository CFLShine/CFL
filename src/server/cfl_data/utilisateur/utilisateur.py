from mongoengine import *

import src.server.cfl_data.etat_civil.personne as personne


class Utilisateur(personne.Personne):
    habilitation = StringField(default="")
    login = ReferenceField('Login', default=None)
    autorisations = ListField(ReferenceField('Autorisation'))