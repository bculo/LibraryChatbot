"""
Python script for contacting library database through library API
"""
import requests
import json

API_URL = "https://c989350e.ngrok.io/api"


def get_n_random_books(books_number):
    r = requests.get(API_URL + "/book/randombooks?number=%s" % books_number)
    return r.json()


def get_n_categorized_books(books_number, category):
    r = requests.get(API_URL + "/book/categorybooks?number=%s&category=%s" % (books_number, category))
    return r.json()


def get_personalized_books():
    r = requests.get(API_URL + "/recommend")
    return r.json()


def update_user_book_reservation():
    r = requests.get(API_URL + "/reservation")
    return r.json()
