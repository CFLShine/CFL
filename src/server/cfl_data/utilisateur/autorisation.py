from mongoengine import *


class Autorisation(Document):
    lecture = BooleanField(default=False)
    ecriture = BooleanField(default=False)