def isPrimitive(object):
    return isinstance(object, (bool, int, float, complex,  str))

def isList(object):
    return isinstance(object, list)

def isDictionary(object):
    return isinstance(object, dict)

def isTuple(object):
    return isinstance(object, tuple)

if (__name__ == "__main__"):
    o = (1, 2, 3)
    print(isTuple(o))
