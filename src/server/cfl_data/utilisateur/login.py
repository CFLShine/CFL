from mongoengine import *


class Login(Document):
    identifiant = StringField(default="")
    password = StringField(default="")