from mongoengine import *

import src.server.cfl_data.planning_journalier.equipe as equipe


class Zone(Document):
    page = ReferenceField('Page', default=None)
    subject = GenericReferenceField(default=None)
    equipe = EmbeddedDocumentField(equipe.Equipe)
