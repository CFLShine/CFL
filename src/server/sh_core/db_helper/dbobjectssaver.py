from mongoengine import *


class DocumentSaver:
    def __init__(self):
        self._proceededs = list()

    def save_document_and_subdocuments(self, doc):
        if not isinstance(document, Document
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
        document.save()
