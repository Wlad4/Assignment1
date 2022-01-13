{% extends "base.tpl" %}

{% block content %}
<p>Частота символов в введеном тексте</p>
<p></p>
<table>
    <tr>
        {% for key, val in frequency.items() %}
        <th>{{key}}, %</th>
        {% endfor %}
    </tr>
    <tr>
        {% for key, val in frequency.items() %}
        <td>{{val}}</td>
        {% endfor %}
    </tr>
</table>

{% endblock %}