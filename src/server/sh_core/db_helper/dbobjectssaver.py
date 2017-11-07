from mongoengine import *


class DocumentSaver:
    def __init__(self):
        self._proceededs = list()

    def saveDocumentAndSubDocuments(self, document):
        if not isinstance(document, Document
        or document in self._proceededs):
            return
        for attrname, attr in document._data.items():
            if isinstance(attr, Document):
                self._proceededs.append(attr)
                self.saveDocumentAndSubDocuments(attr)
            else:
                if isinstance(attr, list):
                    for item in attr:
                        self.saveDocumentAndSubDocuments(item)
        document.save()
