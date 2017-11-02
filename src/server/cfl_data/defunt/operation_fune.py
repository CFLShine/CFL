from mongoengine import *


class OperationFune(Document):
    defunt = ReferenceField('Defunt', default=None)
    date = DateTimeField(default=None)
    heure = DateTimeField(default=None)
    commentaire = StringField(default="")
