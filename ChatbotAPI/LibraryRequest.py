"""
    Custom Library Request
    Class can be transformed to Library API compatible JSON object
    Transforming example:
    JSON_Request = json.dumps(LibraryRequest_object.__dict__)
"""


class LibraryRequest(object):

    def __init__(self, Number, Category):
        self.Number = Number
        self.Category = Category

    def __str__(self):
        return "Request to Library API to fetch %s books from %s category." % (self.Number, self.Category)