"""
Python script for contacting library database through library API
"""
import requests
import json
from LibraryRequest import LibraryRequest
from ReservationRequest import ReservationRequest

API_URL = "https://c989350e.ngrok.io/api"
headers = {'Content-type': 'application/json', 'Accept': 'text/plain'}


def get_n_random_books_post(books_number):
    request = LibraryRequest(books_number, "")
    JSON_Request = json.dumps(request.__dict__)
    r = requests.post(API_URL + "/book/randombooks", data=JSON_Request, headers=headers)
    return r.json()


def get_n_random_books(books_number):
    r = requests.get(API_URL + "/book/randombooks?number=%s" % books_number)
    return r.json()


def get_n_categorized_books(books_number, category):
    r = requests.get(API_URL + "/book/categorybooks?number=%s&category=%s" % (books_number, category))
    return r.json()


def get_personalized_books():
    r = requests.get(API_URL + "/recommend")
    return r.json()


def update_user_book_reservation(username, book):
    request = ReservationRequest(username, book)
    JSON_Request = json.dumps(request.__dict__)
    print(JSON_Request)
    r = requests.post(API_URL + "/book/reservation", data=JSON_Request, headers=headers)
    if r.status_code == 200:
        print("Yes")
    else:
        print("No")
    return r.json()
