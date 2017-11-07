from mongoengine import *


class CodeWithSubject():
    subject = GenericReferenceField(default=None)
    code = StringField(default=None)
