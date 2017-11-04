from flask import Blueprint

fiche_defunt = Blueprint('fiche_defunt', __name__,
                         template_folder='templates')


@fiche_defunt.route('/fiche_defunt/')
def show():
    pass

    # return render_template('fiche_defunt.html', config=Config, page_name='fiche_defunt', dfts=Defunt.objects)
