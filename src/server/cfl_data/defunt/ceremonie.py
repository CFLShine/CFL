from mongoengine import *


class Ceremonie(Document):
    operation = EmbeddedDocumentField('OperationFune', default=None)
    genre = StringField(default="")  # civile, religieuse, etc
    registreCondoleances = BooleanField(default=False)
    bourseDons = BooleanField(default=False)
    presse = BooleanField(default=False)