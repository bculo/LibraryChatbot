import json
import os
import configparser
from Data import ReceiveDataManager
from Models import ServerResponse


def get_config_file_path():
    # Config file in API or Data?
    current_older = os.path.dirname(os.path.abspath(__file__))
    init_file = os.path.join(current_older, 'config.txt')
    return init_file


class Main:
    # Chatbot data class
    # For future storage of important data

    def __init__(self):
        # Read configuration file
        self.config = configparser.RawConfigParser()
        self.config.read(get_config_file_path())

        # Set main attributes
        self.main_data = dict(self.config.items('Main_Configuration'))
        self.port = self.main_data['port']

        # Set Chatbot data
        self.current_user_name = 'Default username'
        self.current_language = 'en'
        self.received_messages = []

    def generate_response(self, response_text, received_data):
        # Generate response JSON object for chatbot (SAP Conversational AI Compatible)
        # response_text -> finished response that will be displayed to the user
        # received_data -> data received from chatbot (SAP Conversational AI predefined JSON structure)
        self.extract_received_data(received_data)
        self.received_messages.append(received_data)
        object_response = ServerResponse.Response(200, 'text', response_text, self.current_language, self.current_user_name)
        response = json.dumps(object_response.__dict__)
        return response

    def print_messages(self):
        for message in self.received_messages:
            print("Message:")
            print(message)

    def extract_received_data(self, received_data):
        self.current_language = ReceiveDataManager.fetch_language(received_data)
        self.current_user_name = ReceiveDataManager.fetch_user_name(received_data)

    def get_config(self):
        return self.config

    def get_port(self):
        return self.port
