from mongoengine import *


class Identite(Document):
    civilite = StringField() #EnumCivilite