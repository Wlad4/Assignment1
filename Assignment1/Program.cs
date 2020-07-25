using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using OfficeOpenXml;
using System.IO;
using Newtonsoft.Json;

namespace Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var path = @"C:\Таблица_резервуаров.xlsx";

            var factories = ReadExcel(path, 0, new List<Factory>());
            var units = ReadExcel(path, 1, new List<Unit>());
            var tanks = ReadExcel(path, 2, new List<Tank>());

            FillUp(factories, units);
            FillUp(units, tanks);

            Print(factories);
            Print(units);
            Print(tanks);

            LoadToJson("Factories.json", factories);
            LoadToJson("Units.json", units);
            LoadToJson("Tanks.json", tanks);

        }

        public static void LoadToJson(string path, List<Factory> factories)
        {
            var json = JsonConvert.SerializeObject(factories, Formatting.Indented);
            File.WriteAllText(path, json);
        }
        public static void LoadToJson(string path, List<Unit> units)
        {
            var json = JsonConvert.SerializeObject(units, Formatting.Indented);
            File.WriteAllText(path, json);
        }
        public static void LoadToJson(string path, List<Tank> tanks)
        {
            var json = JsonConvert.SerializeObject(tanks, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public static List<Factory> ReadExcel(string path, int sheetNumber, List<Factory> factories)
        {
            using (var excelPackage = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = excelPackage.Workbook.Worksheets[sheetNumber];
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

            }
            return factories;

        }
        public static List<Unit> ReadExcel(string path, int sheetNumber, List<Unit> units)
        {
            using (var excelPackage = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = excelPackage.Workbook.Worksheets[sheetNumber];
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

            }
            return units;

        }
        public static List<Tank> ReadExcel(string path, int sheetNumber, List<Tank> tanks)
        {
            using (var excelPackage = new ExcelPackage(new FileInfo(path)))
            {
                var worksheet = excelPackage.Workbook.Worksheets[sheetNumber];
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
                        Volume = decimal.Parse(rowCells[2]),
                        MaxVolume = decimal.Parse(rowCells[3]),
                        UnitId = int.Parse(rowCells[4])
                    });
                }

            }
            return tanks;

        }

        public static decimal GetTotalVolume(List<Tank> tanks)
        {
            decimal _sum = 0;
            foreach (var item in tanks)
            {
                _sum += item.Volume;
            }
            Console.WriteLine($"Общая сумма загрузки всех резервуаров: {_sum}");
            return _sum;
        }

        public static Factory Search(string name, List<Factory> factories)
        {
            var result = new List<Factory>();
            foreach (var factory in factories)
            {
                if (factory.Name == name)
                {
                    result.Add(factory);
                }
            }

            if (result.Count == 0)
            {
                Console.WriteLine("Error occured. Name not found");
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
        public static Unit Search(string name, List<Unit> units)
        {
            var result = new List<Unit>();
            foreach (var unit in units)
            {
                if (unit.Name == name)
                {
                    result.Add(unit);
                }
            }

            if (result.Count == 0)
            {
                Console.WriteLine("Error occured. Name not found"); 
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
        public static Tank Search(string name, List<Tank> tanks)
        {
            var result = new List<Tank>();
            foreach (var tank in tanks)
            {
                if (tank.Name == name)
                {
                    result.Add(tank);
                }
            }

            if (result.Count == 0)
            {
                Console.WriteLine("Error occured. Name not found"); 
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

        public static void FillUp(List<Factory> factories, List<Unit> units)
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
        }
        public static void FillUp(List<Unit> units, List<Tank> tanks)
        {
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

        public static void Print(List<Factory> factories)
        {
            foreach (var item in factories)
            {
                Console.WriteLine($" Id = {item.Id}, Name = {item.Name}, Description = {item.Description}");
            }
        }
        public static void Print(List<Unit> units)
        {
            foreach (var item in units)
            {
                Console.WriteLine($" Id = {item.Id}, Name = {item.Name}, Factory = {item.Factory.Name}");
            }
        }
        public static void Print(List<Tank> tanks)
        {
            foreach (var item in tanks)
            {
                Console.WriteLine($" Id = {item.Id}, Name = {item.Name}, Volume = {item.Volume}" +
                    $"Max volume = {item.MaxVolume}, Unit = {item.Unit.Name}");
            }
        }
    }
}
