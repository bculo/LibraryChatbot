"""
    Python script for manipulating received JSON file from Chatbot (SAP Conversational AI)
"""


def fetch_book_category(data):
    if 'book-categories' not in data['nlp']['entities']:
        return 'Book category not detected'
    else:
        return data['nlp']['entities']['book-categories'][0]['value']  # value -> full value | raw -> user typed value


def fetch_user_name(data):
    if 'userName' not in data['conversation']['participant_data']:
        return "Username not detected!"
    else:
        return data['conversation']['participant_data']['userName']


def fetch_language(data):
    if 'language' not in data['nlp']:
        return "Language not detected!"
    else:
        return data['nlp']['language']  # data['nlp']['language'] | data['conversation']['language']


def fetch_location(data):
    if 'location' not in data['conversation']['memory']:
        return "Language not detected!"
    else:
        return data['conversation']['memory']['location']['formatted']  # formatted | raw


def fetch_book(data):
    if 'books' not in data['nlp']['entities']:
        return 'Book not detected!'
    else:
        return data['nlp']['entities']['books'][0]['value']


def fetch_user_token(data):
    if 'token' not in data['conversation']['memory']:
        return None
    else:
        return data['conversation']['memory']['token']

