def populate():
    from mongoengine import connect

    connect('CFL')

    from src.server.populate_db.pdb_defunt import populate as dpop
    from src.server.populate_db.pdb_identite import populate as ipop
    from src.server.populate_db.pdb_personne import populate as ppop

    ipop()
    ppop()
    dpop()


populate()
