from mongoengine import *


class MEB(Document):
    operation = EmbeddedDocumentField('OperationFune', default=None)
    salon = ReferenceField('Salon', default=None)
