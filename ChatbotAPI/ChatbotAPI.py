from flask import Flask, request, jsonify
from ChatbotData import ChatbotData
import ReceiveDataManager
import DatabaseManager
import json

app = Flask(__name__)
chatbot_data = ChatbotData()


@app.route('/random', methods=['POST'])
def random_books():
    # Main API call
    # Return random 5 books from library
    received_data = json.loads(request.get_data().decode('utf-8'))
    print(received_data)

    response_text = get_random_books_response(received_data)
    # response_text = 'Random books from library: Teorija i primjena baza podataka'
    full_response = chatbot_data.generate_response(response_text, received_data)
    return full_response


@app.route('/randomFromCategory', methods=['POST'])
def search_library_by_category():
    # Search Endpoint
    # Return (random | top) 5 books from defined category
    received_data = json.loads(request.get_data().decode('utf-8'))
    print(received_data)

    response_text = get_books_by_category_response(received_data)
    # response_text = 'Books in category: Teorija i primjena baza podataka'
    full_response = chatbot_data.generate_response(response_text, received_data)
    return full_response


@app.route('/reservation', methods=['POST'])
def book_reservation():
    # Make a Book Reservation Endpoint
    # Return reservation response
    received_data = json.loads(request.get_data().decode('utf-8'))
    print(received_data)

    response_text = get_user_book_reservation_response(received_data)
    # response_text = 'User %s attempt to reserve a book %s' % (user_name, book)
    full_response = chatbot_data.generate_response(response_text, received_data)
    return full_response


@app.route('/userReservations', methods=['POST'])
def user_reservations():
    # List User Reservations Endpoint
    # Return reservation response
    received_data = json.loads(request.get_data().decode('utf-8'))
    print(received_data)

    response_text = get_list_of_book_reservations_by_user_response(received_data)
    # response_text = 'User user_name attempt to reserve a book book_name'
    full_response = chatbot_data.generate_response(response_text, received_data)
    return full_response


@app.route('/feedback', methods=['POST'])
def user_feedback():
    # User Feedback Endpoint
    received_data = json.loads(request.get_data().decode('utf-8'))
    print(received_data)

    # response_text = update_user_book_reservation_response(received_data)
    response_text = 'User user_name successfully or unsuccessfully delivered feedback'
    full_response = chatbot_data.generate_response(response_text, received_data)
    return full_response


def get_random_books_response(received_data):
    # Users response for fetching random books from library
    books_number = ReceiveDataManager.fetch_number(received_data)
    if books_number is None:
        books_number = 5
    random_books = DatabaseManager.get_n_random_books(books_number)
    response_text = 'Random %s books from library:\n' % books_number
    book_number = 1
    for book in random_books:
        response_text += str(book_number) + '. ' + book['title'] + '\n'
        book_number += 1
    return response_text


def get_books_by_category_response(received_data):
    # Users response for fetching random books by category from library
    book_category = ReceiveDataManager.fetch_book_category(received_data)
    books_number = ReceiveDataManager.fetch_number(received_data)
    if books_number is None:
        books_number = 5
    categorized_books = DatabaseManager.get_n_categorized_books(books_number, book_category)
    response_text = 'Random %s books from %s category:\n' % (books_number, book_category)
    book_number = 1
    for book in categorized_books:
        response_text += str(book_number) + '. ' + book['title'] + '\n'
        book_number += 1
    return response_text


def get_user_book_reservation_response(received_data):
    # Users response for making a book reservation
    user_name = ReceiveDataManager.fetch_user_name(received_data)
    book = ReceiveDataManager.fetch_book(received_data)
    server_response = DatabaseManager.update_user_book_reservation(user_name, book)
    response_text = 'User %s attempt to reserve a book %s: %s' % (user_name, book, server_response)
    return response_text


def get_list_of_book_reservations_by_user_response(received_data):
    # Users response for fetching previous book reservations
    user_name = ReceiveDataManager.fetch_user_name(received_data)
    user_books = DatabaseManager.get_user_book_reservations(user_name)
    response_text = 'List of reserved books by user %s:' % user_name
    book_number = 1
    for book in user_books:
        response_text += str(book_number) + '. ' + book['title'] + '\n'
        book_number += 1
    return response_text


def send_user_rating_for_book(received_data):
    # Register user book rating to database
    user_name = ReceiveDataManager.fetch_user_name(received_data)
    book = ReceiveDataManager.fetch_book(received_data)
    rating = ReceiveDataManager.fetch_number(received_data)
    DatabaseManager.send_user_rating(user_name, book, rating)
    # Handle errors


app.run(port=chatbot_data.get_port())


"""
Rezervacija knjige
Rezervirano/Posudeno - koje knjige je korisnik posudio ? 
Koje knjige je korisnik rezervirao ?
Rate a book
"""