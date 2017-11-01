from mongoengine import *


class Deces(Document):
    lieu = ReferenceField('Lieu', default=None)
    date = DateTimeField(default=None)
    heure = DateTimeField(default=None)
    medecin = StringField(default="")


if (__name__ == "__main__"):
    print ('fuck')