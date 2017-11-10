from mongoengine import *


class Cremation(Document):
    operation = EmbeddedDocumentField('OperationFune', default=None)

    operateur = ReferenceField('Utilisateur', default=None)

    heure_debut = DateTimeField(default=None)
    heure_fin = DateTimeField(default=None)

    date_autorisation = DateTimeField(default=None)
    autorisation_delivree_a = ReferenceField('Commune', default=None)

    date_attestation = DateTimeField(default=None)

    date_remise_cendres = DateTimeField(default=None)
    cendres_remises_a = ReferenceField('Pouvoir', default=None)

    destination = StringField(default="")  # enum_destination_cendres