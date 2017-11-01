from mongoengine import *


class OperationFune(Document):
    defunt = ReferenceField('Defunt', default=None)
    date = DateTimeField()
    heure = DateTimeField()
    commentaire = StringField(default="")
