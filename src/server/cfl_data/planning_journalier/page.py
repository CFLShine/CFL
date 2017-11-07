from mongoengine import *


class Page(Document):
    planning = ReferenceField('Planning', default=None)
    date = DateTimeField(default=None)
    zonesMatin = ListField(EmbeddedDocumentField('Zone'), default=list)
    zonesApresMidi = ListField(EmbeddedDocumentField('Zone'), default=list)
