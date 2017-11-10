import devpy.develop as log

log.info("Initializing MongoDB Documents...")

def import_documents():
    exec(
"""
import src.server.cfl_data.coordonnees.contacts
import src.server.cfl_data.coordonnees.commune
import src.server.cfl_data.coordonnees.adresse

import src.server.cfl_data.etat_civil.identite
import src.server.cfl_data.etat_civil.naissance
import src.server.cfl_data.etat_civil.deces
import src.server.cfl_data.etat_civil.personne
import src.server.cfl_data.etat_civil.pouvoir

import src.server.cfl_data.utilisateur.autorisation
import src.server.cfl_data.utilisateur.login
import src.server.cfl_data.utilisateur.utilisateur

import src.server.cfl_data.entreprise.entreprise

import src.server.cfl_data.cimetiere.sepulture
import src.server.cfl_data.cimetiere.cimetiere

import src.server.cfl_data.defunt.operation_fune
import src.server.cfl_data.defunt.defunt
import src.server.cfl_data.defunt.meb
import src.server.cfl_data.defunt.transport
import src.server.cfl_data.defunt.ceremonie
import src.server.cfl_data.defunt.inhumation
import src.server.cfl_data.defunt.cremation

import src.server.cfl_data.planning_journalier.equipe
import src.server.cfl_data.planning_journalier.actioncode
import src.server.cfl_data.planning_journalier.zone
import src.server.cfl_data.planning_journalier.page
import src.server.cfl_data.planning_journalier.planning

"""
    )


import_documents()

log.info("...importing MongoDB Documents done")
