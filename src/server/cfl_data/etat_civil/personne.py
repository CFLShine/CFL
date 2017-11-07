from mongoengine import *


class Personne(Document):
    identite = ReferenceField('Identite', default=None)
    naissance = ReferenceField('Naissance', default=None)
    deces = ReferenceField('Deces', default = None)

    situation = StringField(default="") # enumSituation
    situation_avec = ReferenceField('Personne', default=None)

    pere = ReferenceField('Personne', default=None)
    mere = ReferenceField('Personne', default=None)

