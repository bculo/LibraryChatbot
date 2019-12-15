"""
    Custom Search Library Request
    Class can be transformed to Library API compatible JSON object
    Transforming example:
    JSON_Request = json.dumps(SearchLibraryRequest_object.__dict__)
"""


class SearchLibraryRequest(object):

    def __init__(self, Number, Category):
        self.Number = Number
        self.Category = Category

    def __str__(self):
        return "Request to Library API to fetch %s books from %s category." % (self.Number, self.Category)