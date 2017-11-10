from mongoengine import *


class Salon(Document):
    nom = StringField(default="")
    code = StringField(default="")
    defunt = ReferenceField('Defunt', default=None)
