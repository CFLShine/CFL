from tokenize import tokenize, untokenize, NUMBER, STRING, NAME, OP
from io import BytesIO
import keyword

# constants

TRUE = 60
FALSE = 61
NONE = 62
AND = 63
OR = 64
CLASS = 65
DEF = 66
DEL = 67
IF = 68
ELIF = 69
ELSE = 70
ASSERT = 71
TRY = 72
EXCEPT = 73
RAISE = 74
FINALLY = 75
FOR = 76
IN = 77
IS = 78
NOT = 79
WHILE = 80
WITH = 90
RETURN = 91
YIELD = 92
PASS = 93
BREAK = 94
CONTINUE = 95
LAMBDA = 96
FROM = 97
IMPORT = 98
AS = 99
GLOBAL = 100
NONLOCAL = 101

KEYWORDS = {
    'False': FALSE,
    'None': NONE,
    'True': TRUE,
    'and': AND,
    'as': AS,
    'assert': ASSERT,
    'break': BREAK,
    'class': CLASS,
    'continue': CONTINUE,
    'def': DEF,
    'del': DEL,
    'elif': ELIF,
    'else': ELSE,
    'except': EXCEPT,
    'finally': FINALLY,
    'for': FOR,
    'from': FROM,
    'global': GLOBAL,
    'if': IF,
    'import': IMPORT,
    'in': IN,
    'is': IS,
    'lambda': LAMBDA,
    'nonlocal': NONLOCAL,
    'not': NOT,
    'or': OR,
    'pass': PASS,
    'raise':RAISE,
    'return': RETURN,
    'try': TRY,
    'while':WHILE,
    'with': WITH,
    'yield': YIELD
}

class TokenizePython:

    def run(self, s: str):
        g = tokenize(BytesIO(s.encode('utf-8')).readline)  # tokenize the string
        return g


if __name__ == '__main__':
    g = TokenizePython().run('Defunt.personne.deces.date.year == 2017 and Defunt.personne.deces.lieu.adresse.commune == "Chambéry"')
    for token in g:
        print(token.type)
        print(token.exact_type)
        print(token.string)
        print('----------------------')