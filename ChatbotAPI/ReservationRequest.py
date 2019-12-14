"""
    Custom Library Reservation Request
    Class can be transformed to Library API compatible JSON object
    Transforming example:
    JSON_Request = json.dumps(ReservationRequest_object.__dict__)
"""


class ReservationRequest(object):

    def __init__(self, Username, Book):
        self.Username = Username
        self.Book = Book

    def __str__(self):
        return "Request to Library API to reserve book %s to user %s." % (self.Book, self.Username)