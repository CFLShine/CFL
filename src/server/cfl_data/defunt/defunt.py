from mongoengine import *


class Defunt(Document):
    personne = ReferenceField('Personne', default=None)
    deces = ReferenceField('Deces', default=None)
    situation = StringField(default="") # enumSituation

    operations = ListField(ReferenceField('OperationFune'), default=None)
