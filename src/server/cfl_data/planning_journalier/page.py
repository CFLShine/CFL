from mongoengine import *


class Page(Document):
    planning = ReferenceField('Planning', default=None)
    date = DateTimeField(default=None)
    zones = ListField('Zone', default=list)
