from mongoengine import *


class Inhumation(Document):
    operation = EmbeddedDocumentField('OperationFune', default=None)
    enveloppe = StringField(default="") # Enum_enveloppe
    sepulture = ReferenceField('Sepulture', default=None)
    scellementUrne = BooleanField(default=False)