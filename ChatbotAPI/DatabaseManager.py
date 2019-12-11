"""
Python script for contacting library database through library API
"""
import requests

API_URL = "https://d34a2137.ngrok.io/api"


def get_random_books():
    r = requests.get(API_URL + "/book/randombooks")
    return r.json()


def get_categorized_books():
    r = requests.get(API_URL + "/search")
    return r.json()


def get_personalized_books():
    r = requests.get(API_URL + "/recommend")
    return r.json()


def update_user_book_reservation():
    r = requests.get(API_URL + "/reservation")
    return r.json()
