from mongoengine import *


class Identite(Document):
    civilite    = StringField() #EnumCivilite
    nom         = StringField(default = "")
    prenom      = StringField(default = "")
    prenoms     = StringField(default = "")
    nationalite = StringField(default = "")
    profession  = StringField(default = "")