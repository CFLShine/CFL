from flask import Flask, render_template
from src.client.start import start
from mongoengine import *
from src.settings import Config
import devpy.develop as log
from src.server.import_models import *

# Connexion à la Database
log.info(f"Connecting to database [{Config.db_name}@{Config.db_host}:{Config.db_port}]")
connect(Config.db_name, host=Config.db_host, port=Config.db_port)

# Création et démarrage de l'appli Flask
log.info("Starting Flask app")
app = Flask(__name__, template_folder='../client/templates')
start(app)
