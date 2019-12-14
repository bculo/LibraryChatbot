from flask import Flask, request, jsonify
from ChatbotData import ChatbotData
import ReceiveDataManager
import DatabaseManager
import json

app = Flask(__name__)
chatbot_data = ChatbotData()


@app.route('/random', methods=['POST'])
def main_route():
    # Main API call
    # Return random 5 books from library
    received_data = json.loads(request.get_data().decode('utf-8'))
    print(received_data)

    response_text = get_random_books_response(received_data)
    # response_text = 'Random books from library: Teorija i primjena baza podataka'
    full_response = chatbot_data.generate_response(response_text, received_data)
    return full_response


@app.route('/randomFromCategory', methods=['POST'])
def search_library():
    # Search Endpoint
    # Return (random | top) 5 books from defined category
    received_data = json.loads(request.get_data().decode('utf-8'))

    response_text = get_categorized_books_response(received_data)
    # response_text = 'Books in %s: Teorija i primjena baza podataka' % book_category
    full_response = chatbot_data.generate_response(response_text, received_data)
    return full_response


@app.route('/recommend', methods=['POST'])
def recommend_book():
    # Recommend Book Endpoint
    # Return 5 books based on user preferences
    received_data = json.loads(request.get_data().decode('utf-8'))
    user_name = ReceiveDataManager.fetch_user_name(received_data)

    # response_text = get_personalized_books_response()
    response_text = 'Books based on preferences of %s: Teorija i primjena baza podataka' % user_name
    full_response = chatbot_data.generate_response(response_text, received_data)
    return full_response


@app.route('/login', methods=['POST'])
def user_login():
    # Implement login
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
    book = ReceiveDataManager.fetch_book(received_data)  # Finish
    print(book)

    # response_text = update_user_book_reservation_response()
    response_text = 'User %s successfully or unsuccessfully made a reservation of %s' % (user_name, book)
    full_response = chatbot_data.generate_response(response_text, received_data)
    return full_response


@app.route('/feedback', methods=['POST'])
def user_feedback():
    # User Feedback Endpoint
    received_data = json.loads(request.get_data().decode('utf-8'))
    user_name = ReceiveDataManager.fetch_user_name(received_data)
    print(received_data)

    # response_text = update_user_book_reservation_response()
    response_text = 'User %s successfully or unsuccessfully delivered feedback' % user_name
    full_response = chatbot_data.generate_response(response_text, received_data)
    return full_response


def get_random_books_response(received_data):
    books_number = ReceiveDataManager.fetch_books_number(received_data)
    if books_number is None:
        books_number = 5
    random_books = DatabaseManager.get_n_random_books(books_number)
    response_text = 'Random %s books from library:\n' % books_number
    book_number = 1
    for book in random_books:
        response_text += str(book_number) + '. ' + book['title'] + '\n'
        book_number += 1
    return response_text


def get_categorized_books_response(received_data):
    book_category = ReceiveDataManager.fetch_book_category(received_data)
    books_number = ReceiveDataManager.fetch_books_number(received_data)
    if books_number is None:
        books_number = 5
    categorized_books = DatabaseManager.get_categorized_books(books_number, book_category)
    response_text = 'Random %s books from %s category:\n' % (books_number, book_category)
    book_number = 1
    for book in categorized_books:
        response_text += str(book_number) + '. ' + book['title'] + '\n'
        book_number += 1
    return response_text


def get_personalized_books_response():
    personalized_books = DatabaseManager.get_personalized_books()
    response_text = 'Recommended 5 books based on your preferences:\n'
    book_number = 1
    for book in personalized_books:
        response_text += str(book_number) + '. ' + book['title'] + '\n'
        book_number += 1
    return response_text


def update_user_book_reservation_response():
    random_books = DatabaseManager.update_user_book_reservation()
    response_text = random_books.json()  # Update response
    return response_text


app.run(port=chatbot_data.get_port())
