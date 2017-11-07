from mongoengine import *


class Four(Document):
    nom = StringField(default="")
