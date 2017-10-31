from mongoengine import *


class Commune(Document):
    nom = StringField(default = "")
    codepost = StringField(default = "")