from flask import Flask, request, jsonify
from ChatbotServer import ChatbotServer
import ReceiveDataManager
import json
import requests

app = Flask(__name__)
chatbot_server = ChatbotServer()


@app.route('/', methods=['POST'])
def main_route():
    received_data = json.loads(request.get_data().decode('utf-8'))
    book_category = ReceiveDataManager.fetch_book_category(received_data)

    response_text = 'Books in %s: Teorija i primjena baza podataka' % book_category
    return chatbot_server.generate_response(response_text, received_data)


@app.route('/search', methods=['POST'])
def search_library():
    received_data = json.loads(request.get_data().decode('utf-8'))
    book_category = chatbot_server.fetch_book_category(received_data)

    print("JSON Received data")
    print(received_data)

    response_text = 'Books in %s: Teorija i primjena baza podataka' % book_category
    return chatbot_server.generate_response(response_text)


@app.route('/recommend', methods=['POST'])
def recommend_book():
    return jsonify(
        status=200,
        replies=[{
            'type': 'text',
            'content': 'Unexpected error!'
        }]
    )

@app.route('/login', methods=['POST'])
def user_login():
    return jsonify(
        status=200,
        replies=[{
            'type': 'text',
            'content': 'Unexpected error!'
        }]
    )

@app.route('/reservation', methods=['POST'])
def book_reservation():
    return jsonify(
        status=200,
        replies=[{
            'type': 'text',
            'content': 'Unexpected error!'
        }]
    )


app.run(port=chatbot_server.get_port())
