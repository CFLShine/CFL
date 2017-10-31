from mongoengine import *

import src.server.cfl_data.defunt.defunt as dft


class OperationFune(Document):

    def __init__(self):
        self.defunt = None
        self.date = None
        self.heure = None
        self.commentaire = ""
