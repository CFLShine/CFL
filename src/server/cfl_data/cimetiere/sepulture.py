from mongoengine import *


class Sepulture(Document):
    cimetiere = ReferenceField('Cimetiere', default=None)

    sepulture_type = StringField(default="")

    division = StringField(default = "")
    carre = StringField(default = "")
    ligne = StringField(default = "")
    tombe = StringField(default = "")

    monument = BooleanField(default=False)
