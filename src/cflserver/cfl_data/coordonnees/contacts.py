from mongoengine import *


class Contacts(Document):
    telFixPerso  = StringField(default="")
    telFixPro    = StringField(default="")
    telPortPerso = StringField(default="")
    telPortPro   = StringField(default="")
    fax          = StringField(default="")
    mail         = StringField(default="")
