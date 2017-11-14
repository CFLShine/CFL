
"""
inhumation = self.operation
display = "inhumation " + inhumation.sepulture.seputure_type

if inhumation.cimetiere
    display += ' cimetière ' + inhumation.cimetiere.lieu.nom + ' de ' + inhumation.cimetiere.lieu.adresse.commune.nom
"""

"""
# déclarations
inhumation = self.operation



display = "inhumation " + STRING('inhumation.sepulture.seputure_type')

if MEMBER(inhumation.cimetiere)
    display += ' cimetière ' + STRING('inhumation.cimetiere.lieu.nom') + ' de ' + STRING('inhumation.cimetiere.lieu.adresse.commune.nom')

def MEMBER(member_expr: str):
    result = None
    try:
        exec("result = " + member_expr)
    except:
        print("member expression non valide : " + member_expr)
    return result
    
def STRING(member_expr: string
    value = MEMBER(member_expr)
    if value:
        return value
    return ""
    
"""

class CodeBuilder:
    string_method =\
"""
def STRING(self, member_expr):
    value = MEMBER(self, member_expr)
    if value:
        return value
    return ""
"""
    member_method =\
"""
def MEMBER(self, expr):
    names = expr.split(".")
    obj = self
    for name in names:
        obj = getattr(obj, name)
        if not obj:
            return None
    return obj
"""


    def __init__(self, code: str):
        self.code = code

    def rewritten(self):
        if not self.code.startswith('#rewritten'):
            self.code = "#rewritten\n" +\
                        CodeBuilder.member_method + '\n'+\
                        CodeBuilder.string_method + '\n' +\
                        self.code

        return self.code


