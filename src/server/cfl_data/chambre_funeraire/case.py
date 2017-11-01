from mongoengine import *


class Case(Document):
    nom = StringField(default = "")
    negative = BooleanField(default = False)