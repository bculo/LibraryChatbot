"""
    Custom Library Book Request
    Class can be transformed to Library API compatible JSON object
    Transforming example:
    JSON_Request = json.dumps(UserBookActionsRequest_object.__dict__)
"""


class Request(object):

    def __init__(self, Username, Book, Rating):
        self.Username = Username
        self.Book = Book
        self.Rating = Rating

    def __str__(self):
        return "Request to Library API to reserve, rate or list reservations for user %s." % self.Username