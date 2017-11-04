import importlib


def import_string(dotted_path):
    """
    Import a dotted module path and return the attribute/class designated by the
    last name in the path. Raise ImportError if the import failed.
    """
    try:
        module_path, class_name = dotted_path.rsplit('.', 1)
    except ValueError:
        raise ValueError("%s doesn't look like a module path" % dotted_path)
    else:
        module = importlib.import_module(module_path)

        try:
            return getattr(module, class_name)
        except AttributeError:
            raise AttributeError('Module "%s" does not define a "%s" attribute/class' %
                                 (module_path, class_name))
