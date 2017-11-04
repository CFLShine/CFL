from mongoengine import *


class Naissance(Document):
    lieu = ReferenceField('Lieu', default = None)
    date = DateTimeField(default = None)