{% extends "base.tpl" %}

{% block content %}
 <p>Введите текст для получения частоты всех символов:</p>

<form action = /table method=POST>
<br><input type=text name=InputString value={{input_string}}><br>
<br><input type=submit value="Ok"><br>
</form>

{% endblock %}