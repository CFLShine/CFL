from mongoengine import *


class OperationFune(Document):
    meta = {'allow_inheritance': True}

    defunt = ReferenceField('Defunt', default=None)
    date = DateTimeField(default=None)
    heure = DateTimeField(default=None)
    commentaire = StringField(default="")
