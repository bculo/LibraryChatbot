import json
import os
import configparser
from ServerResponse import ServerResponse
import ReceiveDataManager


def get_config_file_path():
    current_older = os.path.dirname(os.path.abspath(__file__))
    init_file = os.path.join(current_older, 'config.txt')
    return init_file


class ChatbotServer:

    def __init__(self):
        # Read configuration file
        self.config = configparser.RawConfigParser()
        self.config.read(get_config_file_path())

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

    def extract_received_data(self, received_data):
        self.book_category = ReceiveDataManager.fetch_book_category(received_data)
        self.language = ReceiveDataManager.fetch_language(received_data)
        self.user_name = ReceiveDataManager.fetch_user_name(received_data)

    def get_config(self):
        return self.config

    def get_port(self):
        return self.port
