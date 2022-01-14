#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    form = new Form;
    form->show();

    connect(this, &MainWindow::SendToForm, form, &Form::GetFromMain);
    connect(form, &Form::SendToMain, this, &MainWindow::GetFromForm);
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::on_pushButton_clicked()
{
    emit SendToForm(ui->lineEdit->text());
}

void MainWindow::GetFromForm(QString message)
{
    ui->label->setText(message);
}

