import re
from flask import render_template, request


class WebService:

    def __init__(self):
        self.input_string = ''
        self.frequency = {}

    def show_form(self):
        return render_template('main.tpl', input_string=self.input_string)

    def get_table(self):
        self.input_string = request.form.get('InputString')
        only_alpha = re.findall('[a-zA-Zа-яА-я]', self.input_string)
        for char in only_alpha:
            if char in self.frequency:
                self.frequency[char] += 1
            else:
                self.frequency[char] = 1
        for key, val in self.frequency.items():
            self.frequency[key] = round(val / len(only_alpha) * 100, 2)
        return render_template('table.tpl', frequency=self.frequency)
