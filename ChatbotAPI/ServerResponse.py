class ServerResponse(object):
    """
    Custom Server Response
    """

    def __init__(self, status, text_type, content, language, name):
        self.status = status
        self.__set_replies(text_type, content)
        self.__set_conversation(language, name)

    def __str__(self):
        return self.name

    def __set_replies(self, text_type, content):
        self.replies = [{
            'type': text_type,
            'content': content
        }]

    def __set_conversation(self, language, name):
        self.conversation = {
            'language': language,
            'name': name
        }

