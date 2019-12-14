"""
    Custom Server Response
    Class can be transformed to SAP Conversational AI compatible JSON object
    Transforming example:
    JSON_Response = json.dumps(ServerResponse_object.__dict__)
"""


class ServerResponse(object):

    def __init__(self, status, text_type, content, language, name):
        self.status = status
        self.__set_replies(text_type, content)
        self.__set_conversation(language, name)

    def __str__(self):
        return "Server response to user %s: %s" % (self.conversation['memory']['name'], self.replies[0]['content'])

    def __set_replies(self, text_type, content):
        self.replies = [{
            'type': text_type,
            'content': content
        }]

    def __set_conversation(self, language, name):
        self.conversation = {
            'memory': {
                'token': 'userToken',
                'name': name
            }
        }
