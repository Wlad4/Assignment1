from flask import Flask, g

from WebService import WebService

app = Flask(__name__)


def get_service():
    if 'WebService' not in g:
        g.web_service = WebService()
    return g.web_service


@app.route("/")
def input_text():
    return get_service().show_form()


@app.route("/table", methods=['POST'])
def get_table():
    return get_service().get_table()


if __name__ == "__main__":
    app.run(debug=True)
