from flask import Blueprint, render_template
from src.settings import Config

index = Blueprint('index', __name__,
                  template_folder='templates')


@index.route('/')
def show():
    return render_template('index.html', config=Config, page_name='index')
