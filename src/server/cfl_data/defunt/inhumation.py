from mongoengine import *


class Inhumation(Document):
    operation = EmbeddedDocumentField('OperationFune', default=None)
    enveloppe = StringField(default="") # EnumEnveloppe
    sepulture = ReferenceField('Sepulture', default=None)
    scellementUrne = BooleanField(default=False)