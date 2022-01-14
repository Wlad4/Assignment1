#include "form.h"
#include "ui_form.h"

Form::Form(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::Form)
{
    ui->setupUi(this);
}

Form::~Form()
{
    delete ui;
}

void Form::GetFromMain(QString message)
{
    ui->label->setText(message);
}

void Form::on_pushButton_clicked()
{
    emit SendToMain(ui->lineEdit->text());
}

