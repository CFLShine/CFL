from mongoengine import *

from src.settings import Config


connect(Config.db_name, host=Config.db_host, port=Config.db_port)


class DocumentSaver:
    def __init__(self):
        self._proceededs = list()

    def save_document_and_subdocuments(self, doc):
        if not isinstance(doc, Document
                        or doc in self._proceededs):
            return
        for attrname, attr in doc._data.items():
            if isinstance(attr, Document):
                self._proceededs.append(attr)
                self.save_document_and_subdocuments(attr)
            else:
                if isinstance(attr, list):
                    for item in attr:
                        self.save_document_and_subdocuments(item)
        doc.save()
