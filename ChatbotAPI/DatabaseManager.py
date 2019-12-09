"""
Python script for contacting library database through library API
"""

import requests


def get_random_books():
    r = requests.get("API URL")
    return r.json()


def get_categorized_books():
    r = requests.get("API URL")
    return r.json()


def get_personalized_books():
    r = requests.get("API URL")
    return r.json()


def update_user_book_reservation():
    r = requests.get("API URL")
    return r.json()
