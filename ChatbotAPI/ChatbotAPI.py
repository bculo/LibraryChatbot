from flask import Flask, request, jsonify
from ChatbotData import ChatbotData
import ReceiveDataManager
import DatabaseManager
import json
import requests

app = Flask(__name__)
chatbot_data = ChatbotData()


@app.route('/', methods=['POST'])
def main_route():
    # Main API call
    # Return something
    received_data = json.loads(request.get_data().decode('utf-8'))

    # response_text = get_random_books_response()
    response_text = 'Random books from library: Teorija i primjena baza podataka'
    return chatbot_data.generate_response(response_text, received_data)


@app.route('/search', methods=['POST'])
def search_library():
    # Search Endpoint
    # Return random | top 10 books from defined category
    received_data = json.loads(request.get_data().decode('utf-8'))
    book_category = ReceiveDataManager.fetch_book_category(received_data)

    # response_text = get_categorized_books_response()
    response_text = 'Books in %s: Teorija i primjena baza podataka' % book_category
    return chatbot_data.generate_response(response_text, received_data)


@app.route('/recommend', methods=['POST'])
def recommend_book():
    # Recommend Book Endpoint
    # Return 10 books based on user preferences
    received_data = json.loads(request.get_data().decode('utf-8'))
    user_name = ReceiveDataManager.fetch_user_name(received_data)

    # response_text = get_personalized_books_response()
    response_text = 'Books based on preferences of %s: Teorija i primjena baza podataka' % user_name
    return chatbot_data.generate_response(response_text, received_data)


@app.route('/login', methods=['POST'])
def user_login():
    # Update
    return jsonify(
        status=200,
        replies=[{
            'type': 'text',
            'content': 'Unexpected error!'
        }]
    )


@app.route('/reservation', methods=['POST'])
def book_reservation():
    # Make a Book Reservation Endpoint
    # Return reservation response
    received_data = json.loads(request.get_data().decode('utf-8'))
    user_name = ReceiveDataManager.fetch_user_name(received_data)

    # response_text = update_user_book_reservation_response()
    response_text = 'User %s successfully or unsuccessfully made a reservation of Teorija i primjena baza podataka' % user_name
    return chatbot_data.generate_response(response_text, received_data)


def get_random_books_response():
    random_books = DatabaseManager.get_random_books()
    response_text = random_books.json()  # Update response
    return response_text


def get_categorized_books_response():
    categorized_books = DatabaseManager.get_categorized_books()
    response_text = categorized_books.json()  # Update response
    return response_text


def get_personalized_books_response():
    random_books = DatabaseManager.get_personalized_books()
    response_text = random_books.json()  # Update response
    return response_text


def update_user_book_reservation_response():
    random_books = DatabaseManager.update_user_book_reservation()
    response_text = random_books.json()  # Update response
    return response_text


app.run(port=chatbot_data.get_port())
