using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using OfficeOpenXml;
using System.IO;
using Newtonsoft.Json;

namespace Assignment1
{
    public class DataManager
    {
        private List<Factory> factories;
        private List<Unit> units;
        private List<Tank> tanks;

        public void Read(string path)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Лицензия EPPluse 

            if (!File.Exists(path))
            {
                Console.WriteLine("File not found");
                Console.ReadLine();
                return;
            }

            using (var excelPackage = new ExcelPackage(new FileInfo(path)))
            {
                if (excelPackage.Workbook.Worksheets.Count > 2)
                {
                    factories = ReadFactories(excelPackage.Workbook.Worksheets[0]); // Чтение из Excel
                    units = ReadUnits(excelPackage.Workbook.Worksheets[1]);
                    tanks = ReadTanks(excelPackage.Workbook.Worksheets[2]);
                }
            }

            // Заполнение свойства Factory класса Unit через свойство FactoryId 
            // и свойства Unit класса Tank через свойство UnitId
            FillUp();
        }

        //Выгрузка объектов в json файл
        public void LoadToJsonFactories(string path)
        {
            if (path == null)
                return;
            var json = JsonConvert.SerializeObject(factories, Formatting.Indented);
            File.WriteAllText(path, json);
        }
        public void LoadToJsonUnits(string path)
        {
            if (path == null)
                return;
            var json = JsonConvert.SerializeObject(units, Formatting.Indented);
            File.WriteAllText(path, json);
        }
        public void LoadToJsonTanks(string path)
        {
            if (path == null)
                return;
            var json = JsonConvert.SerializeObject(tanks, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        //Чтение из Excel
        private List<Factory> ReadFactories(ExcelWorksheet worksheet)
        {
            var factories = new List<Factory>();

            var rowsNumber = worksheet.Dimension.End.Row;
            var columnsNumber = worksheet.Dimension.End.Column;
            for (int currentRow = 2; currentRow <= rowsNumber; currentRow++)
            {
                var rowCells = worksheet.Cells[currentRow, 1, currentRow, columnsNumber]
                                        .Select(c => c.Value == null ? string.Empty : c.Value.ToString())
                                        .ToArray();
                factories.Add(new Factory
                {
                    Id = int.Parse(rowCells[0]),
                    Name = rowCells[1],
                    Description = rowCells[2]
                });
            }
            return factories;
        }
        private List<Unit> ReadUnits(ExcelWorksheet worksheet)
        {
            var units = new List<Unit>();
            var rowsNumber = worksheet.Dimension.End.Row;
            var columnsNumber = worksheet.Dimension.End.Column;
            for (int currentRow = 2; currentRow <= rowsNumber; currentRow++)
            {
                var rowCells = worksheet.Cells[currentRow, 1, currentRow, columnsNumber]
                                        .Select(c => c.Value == null ? string.Empty : c.Value.ToString())
                                        .ToArray();
                units.Add(new Unit
                {
                    Id = int.Parse(rowCells[0]),
                    Name = rowCells[1],
                    FactoryId = int.Parse(rowCells[2])
                });
            }
            return units;
        }
        private List<Tank> ReadTanks(ExcelWorksheet worksheet)
        {
            var tanks = new List<Tank>();
            var rowsNumber = worksheet.Dimension.End.Row;
            var columnsNumber = worksheet.Dimension.End.Column;
            for (int currentRow = 2; currentRow <= rowsNumber; currentRow++)
            {
                var rowCells = worksheet.Cells[currentRow, 1, currentRow, columnsNumber]
                                        .Select(c => c.Value == null ? string.Empty : c.Value.ToString())
                                        .ToArray();
                tanks.Add(new Tank
                {
                    Id = int.Parse(rowCells[0]),
                    Name = rowCells[1],
                    Volume = double.Parse(rowCells[2]),
                    MaxVolume = double.Parse(rowCells[3]),
                    UnitId = int.Parse(rowCells[4])
                });
            }


            return tanks;

        }

        //Общая сумма загрузки всех резервуаров
        public double GetTotalVolume()
        {
            double sum = 0;
            foreach (var item in tanks)
            {
                sum += item.Volume;
            }
            Console.WriteLine($"Общая сумма загрузки всех резервуаров: {sum}");
            return sum;
        }

        //Поиск объекта по имени
        public Factory FindFactoryByName(string name)
        {
            var result = new List<Factory>();
            // Поиск элемента
            foreach (var factory in factories)
            {
                if (factory.Name == name)
                {
                    result.Add(factory);
                }
            }
            // Результат поиска
            if (result.Count == 0)
            {
                Console.WriteLine("Error occurred. Name not found");
            }
            else
            {
                foreach (var factory in result)
                {
                    Console.WriteLine($"Factory: Id = {factory.Id}, Name = {factory.Name}, Description = {factory.Description}");
                    Console.WriteLine("All units in this factory:");
                    foreach (var unit in factory.Units)
                    {
                        Console.WriteLine($"Units: Name = {unit.Name}");
                    }
                }
            }
            return result.FirstOrDefault();
        }
        public Unit FindUnitByName(string name)
        {
            var result = new List<Unit>();
            // Поиск элемента
            foreach (var unit in units)
            {
                if (unit.Name == name)
                {
                    result.Add(unit);
                }
            }
            // Результат поиска
            if (result.Count == 0)
            {
                Console.WriteLine("Error occurred. Name not found");
            }
            else
            {
                foreach (var unit in result)
                {
                    Console.WriteLine($"Unit: Id = {unit.Id}, Name = {unit.Name}, Factory = {unit.Factory}");
                    Console.WriteLine("All tanks in this unit:");
                    foreach (var tank in unit.Tanks)
                    {
                        Console.WriteLine($"Tanks: Name = {tank.Name}, Volume = {tank.Volume}, Max volume = {tank.MaxVolume}");
                    }
                }
            }
            return result.FirstOrDefault();
        }
        public Tank FindTankByName(string name)
        {
            var result = new List<Tank>();
            // Поиск элемента
            foreach (var tank in tanks)
            {
                if (tank.Name == name)
                {
                    result.Add(tank);
                }
            }
            // Результат поиска
            if (result.Count == 0)
            {
                Console.WriteLine("Error occurred. Name not found");
            }
            else
            {
                foreach (var tank in result)
                {
                    Console.WriteLine($"Tank: Id = {tank.Id}, Name = {tank.Name}, Volume = {tank.Volume}," +
                        $" Max volume = {tank.MaxVolume}  Unit = {tank.Unit}");
                }
            }
            return result.FirstOrDefault();
        }

        //Поиск Factory через Unit, у которого известен FactoryId
        public Factory FindFactoryByUnit(Unit unit)
        {
            if (unit == null)
                return null;
            foreach (var factory in factories)
            {
                if (factory.Id == unit.FactoryId)
                {
                    return factory;
                }
            }
            return null;
        }

        //Поиск Unit через известный Tank, у которого известен UnitId
        public Unit FindUnitByTank(Tank tank)
        {
            if (tank == null)
                return null;
            foreach (var unit in units)
            {
                if (unit.Id == tank.UnitId)
                {
                    return unit;
                }
            }
            return null;
        }

        // Заполнение свойства зависимых классов через свойства главных
        private void FillUp()
        {
            foreach (var unit in units)
            {
                foreach (var factory in factories)
                {
                    if (unit.FactoryId == factory.Id)
                    {
                        unit.Factory = factory;
                        factory.Units.Add(unit);
                    }
                }
            }

            foreach (var tank in tanks)
            {
                foreach (var unit in units)
                {
                    if (tank.UnitId == unit.Id)
                    {
                        tank.Unit = unit;
                        unit.Tanks.Add(tank);
                    }
                }
            }
        }

        //Печать
        public void PrintFactories()
        {
            foreach (var item in factories)
            {
                Console.WriteLine($" Id = {item.Id}, Name = {item.Name}, Description = {item.Description}");
            }
        }
        public void PrintUnits()
        {
            foreach (var item in units)
            {
                Console.WriteLine($" Id = {item.Id}, Name = {item.Name}, Factory = {item.Factory.Name}");
            }
        }
        public void PrintTanks()
        {
            foreach (var item in tanks)
            {
                Console.WriteLine($" Id = {item.Id}, Name = {item.Name}, Volume = {item.Volume}" +
                    $"Max volume = {item.MaxVolume}, Unit = {item.Unit.Name}");
            }
        }

    }
}
