"""
Python script for contacting library database through library API
"""
import requests
import json
import configparser
import os
from Models import SearchLibraryRequest
from Models import BookActionsRequest

API_URL = "Default URL"
headers = {}


def read_config_file():
    global API_URL
    global headers
    current_older = os.path.dirname(os.path.abspath(__file__))
    init_file = os.path.join(current_older, 'config.txt')
    config = configparser.RawConfigParser()
    config.read(init_file)
    main_data = dict(config.items('Main_Configuration'))
    API_URL = main_data['libraryapi']
    headers = main_data['libraryapiheaders']
    headers = json.loads(headers)


read_config_file()


def get_n_random_books_post(books_number):
    request = SearchLibraryRequest.Request(books_number, "")
    JSON_Request = json.dumps(request.__dict__)
    r = requests.post(API_URL + "/book/randombooks", data=JSON_Request, headers=headers)
    if r.status_code == 200:
        return r.json()
    else:
        return "Fail"


def get_n_random_books(books_number):
    r = requests.get(API_URL + "/book/randombooks?number=%s" % books_number)
    if r.status_code == 200:
        return r.json()
    else:
        return "Fail"


def get_n_categorized_books(books_number, category):
    r = requests.get(API_URL + "/book/categorybooks?number=%s&category=%s" % (books_number, category))
    if r.status_code == 200:
        return r.json()
    else:
        return "Fail"


def update_user_book_reservation(user_name, book):
    request = BookActionsRequest.Request(user_name, book, "")
    JSON_Request = json.dumps(request.__dict__)
    r = requests.post(API_URL + "/book/reservation", data=JSON_Request, headers=headers)
    if r.status_code == 200:
        return "Success"
    else:
        return "Fail"


def get_user_book_reservations(user_name):
    request = BookActionsRequest.Request(user_name, "", "")
    JSON_Request = json.dumps(request.__dict__)
    r = requests.post(API_URL + "/book/userreservations", data=JSON_Request, headers=headers)
    if r.status_code == 200:
        return r.json()
    else:
        return "Fail"


def send_user_rating(user_name, book, rating):
    request = BookActionsRequest.Request(user_name, book, rating)
    JSON_Request = json.dumps(request.__dict__)
    r = requests.post(API_URL + "/book/bookrating", data=JSON_Request, headers=headers)
    if r.status_code == 200:
        return "Success"
    else:
        return "Fail"

