using System;

namespace Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = "Таблица_резервуаров.xlsx";

            var data = new DataManager();

            data.Read(path);

            Factory factory = data.FindFactoryByName("МНПЗ"); // Примеры поиска
            Unit unit = data.FindUnitByName("АВТ-6");

            Factory f = data.FindFactoryByUnit(null);

            var volume = data.GetTotalVolume(); 

            data.PrintFactories(); // Печать полученных данных
            data.PrintUnits();
            data.PrintTanks();

            data.LoadToJsonFactories("Factories.json"); // Выгрузка объектов в json файл
            data.LoadToJsonUnits("Units.json");
            data.LoadToJsonTanks("Tanks.json");
        }  
    }
}

