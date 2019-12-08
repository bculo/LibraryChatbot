from flask import Flask, request, jsonify
import json
import requests

app = Flask(__name__)
port = '5000'


def fetch_book_category(data):
    return data['nlp']['entities']['book-categories'][0]['value']


def generate_response(response_text):
    response = jsonify(
        status=200,
        replies=[{
            'type': 'text',
            'content': response_text
        }],
        conversation={
            'language': 'en',
            'name': 'Bob'
        }
    )
    return response


@app.route('/', methods=['POST'])
def index():
    received_data = json.loads(request.get_data().decode('utf-8'))
    book_category = fetch_book_category(received_data)

    print("JSON Received data")
    print(received_data)

    response_text = 'Books in %s: Teorija i primjena baza podataka' % book_category
    return generate_response(response_text)


@app.route('/errors', methods=['POST'])
def errors():
    return jsonify(
        status=200,
        replies=[{
            'type': 'text',
            'content': 'Unexpected error!'
        }]
    )


app.run(port=port)
