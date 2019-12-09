import json
import os
import configparser
from ServerResponse import ServerResponse


class ChatbotServer:

    def __init__(self):
        # Read configuration file
        self.config = configparser.RawConfigParser()
        self.config.read(self.get_config_file_path())

        # Set main server attributes
        self.main_data = dict(self.config.items('Main_Configuration'))
        self.port = self.main_data['port']
        self.response = 'Initial response'

        self.book_category = 'None'
        self.user_name = 'Default username'
        self.language = 'en'

    def generate_response(self, response_text, received_data):
        self.extract_received_data(received_data)
        object_response = ServerResponse(200, 'text', response_text, self.language, self.user_name)
        response = json.dumps(object_response.__dict__)
        return response

    def get_config_file_path(self):
        current_older = os.path.dirname(os.path.abspath(__file__))
        init_file = os.path.join(current_older, 'config.txt')
        return init_file

    def extract_received_data(self, received_data):
        self.fetch_book_category(received_data)
        self.fetch_language(received_data)
        self.fetch_user_name(received_data)

    def fetch_book_category(self, data):
        if 'book-categories' not in data['nlp']['entities']:
            self.book_category = 'Book category not detected'
        else:
            self.book_category = data['nlp']['entities']['book-categories'][0]['value']
        return self.book_category  # Move to Data Manager!!

    def fetch_user_name(self, data):
        if 'userName' not in data['conversation']['participant_data']:
            self.user_name = "Username not detected!"
        else:
            self.user_name = data['conversation']['participant_data']['userName']

    def fetch_language(self, data):
        self.language = data['nlp']['language']

    def get_config(self):
        return self.config

    def get_port(self):
        return self.port
