from mongoengine import *


class Zone(EmbeddedDocument):
    page = ReferenceField('Page', default=None)
    subject = GenericReferenceField(default=None)
    equipe = EmbeddedDocumentField('Equipe', default=None)
