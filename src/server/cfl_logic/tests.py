
if __name__ == '__main__':
    import src.server.import_models
    import src.server.cfl_logic.document_factory as docs
    from datetime import datetime

    planning = docs.PlanningFactory.new()
    planning.intitule = "planning pf"

    actioncode = docs.ActionCodeFactory.new()
    actioncode.classname = "Inhumation"
    actioncode.code = \
"""
display = 'inhumation ' + STRING(self, 'operation.sepulture.sepulture_type')

inhumation = self.operation # juste pour l'exemple
if MEMBER(inhumation, 'sepulture.cimetiere.lieu.nom'):
    display += ' cimetière ' + STRING(self, 'operation.sepulture.cimetiere.lieu.nom') + ' de ' + STRING(self, 'operation.sepulture.cimetiere.lieu.adresse.commune.nom')

self.action = display
"""

    defunt = docs.DefuntFactory.new()
    defunt.personne.identite.nom = "DURAND"
    defunt.personne.identite.prenom = "Marcel"

    inhumation = docs.InhumationFactory.new()
    inhumation.operation.date = datetime(year=2017, month=11, day=14, hour=11, minute=00)

    commune = docs.CommuneFactory.new()
    commune.nom = "Chambéry"

    cimetiere = docs.CimetiereFactory.new()
    cimetiere.lieu.nom = 'Charrière-Neuve'
    cimetiere.lieu.adresse.commune = commune

    sepulture = docs.SepultureFactory.new()
    sepulture.cimetiere = cimetiere
    sepulture.sepulture_type = "caveau ouverture dessus"

    inhumation.sepulture = sepulture

    # ***************   Display   *******************
    import src.server.cfl_logic.plannings.actionmanager as actionmanager

    action_manager = actionmanager.ActionManager(actioncode=actioncode.code,
                                                 date=datetime(year=2017, month=11, day=10),
                                                 matin=True,
                                                 operation=inhumation)

    action_manager.exe()
    actiondisplay = action_manager.action
    print(actiondisplay)

