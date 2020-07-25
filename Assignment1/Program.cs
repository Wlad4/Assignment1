using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.OleDb;
using System.Data;
using OfficeOpenXml;
using System.IO;

namespace Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            string path = @"C:\Таблица_резервуаров.xlsx";
            var factories = new List<Factory>();
            var units = new List<Unit>();
            var tanks = new List<Tank>();

            factories = ReadExcel(path, 0, factories);
            units = ReadExcel(path, 1, units);
            tanks = ReadExcel(path, 2, tanks);


            foreach (var item in tanks)
            {
                Console.WriteLine(item.Name);
            }



            //var factories = new List<Factory>
            //{
            //new Factory { Id = 1, Name = "МНПЗ", Description = "Московский нефтеперерабатывающий завод" },
            //new Factory { Id = 2, Name = "ОНПЗ", Description = "Омский нефтеперерабатывающий завод" }
            //};

            //var units = new List<Unit>
            //{
            //new Unit { Id = 1, Name = "ГФУ-1", FactoryId = 1 },
            //new Unit { Id = 2, Name = "ГФУ-2", FactoryId = 1 },
            //new Unit { Id = 3, Name = "АВТ-6", FactoryId = 2 }
            //};

            //var tanks = new List<Tank>
            //{ new Tank { Id = 1, Name = "Резервуар 1", Volume = 1500M, MaxVolume = 2000M, UnitId = 1 },
            //new Tank { Id = 2, Name = "Резервуар 2", Volume = 2500M, MaxVolume = 3000M, UnitId = 1 },
            //new Tank { Id = 3, Name = "Дополнительный резервуар 24", Volume = 3000M, MaxVolume = 3000M, UnitId = 2 },
            //new Tank { Id = 4, Name = "Резервуар 35", Volume = 3000M, MaxVolume = 3000M, UnitId = 2 },
            //new Tank { Id = 5, Name = "Резервуар 47", Volume = 4000M, MaxVolume = 5000M, UnitId = 2 },
            //new Tank { Id = 6, Name = "Резервуар 256", Volume = 500M, MaxVolume = 500M, UnitId = 3 }
            //};

            //FillUp(factories, units);
            //FillUp(units, tanks);

            //GetList(factories);
            //GetList(units);
            //GetList(tanks);

            //var totalValue = GetTotalVolume(tanks);

            //Console.WriteLine("");
            //Console.WriteLine("");
            //Console.WriteLine("");


            //Console.WriteLine("Search: ");
            //string selection = Console.ReadLine();

            //var factory1 = SearchByCode(selection, factories);
            //var factory2 = SearchByLINQ(selection, factories);
            //var unit1 = SearchByLINQ(selection, units);
            //var unit2 = SearchByLINQ(selection, units);
            //var tank1 = SearchByLINQ(selection, tanks);
            //var tank2 = SearchByLINQ(selection, tanks);

            
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

        public static Factory SearchByCode(string name, List<Factory> factories)
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
                Console.WriteLine("Error occured. Name not found"); /////////////////////////////////
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
        public static Unit SearchByCode(string name, List<Unit> units)
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
                Console.WriteLine("Error occured. Name not found"); /////////////////////////////////
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
        public static Tank SearchByCode(string name, List<Tank> tanks)
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
                Console.WriteLine("Error occured. Name not found"); /////////////////////////////////
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

        public static Factory SearchByLINQ(string name, List<Factory> factories)
        {
            var result = (from f in factories
                          where f.Name == name
                          select f).ToList();
            if (result.Count == 0)
            {
                Console.WriteLine("Error occured. Name not found"); /////////////////////////////////
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
        public static Unit SearchByLINQ(string name, List<Unit> units)
        {
            var result = (from u in units
                          where u.Name == name
                          select u).ToList();
            if (result.Count == 0)
            {
                Console.WriteLine("Error occured. Name not found"); /////////////////////////////////
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
        public static Tank SearchByLINQ(string name, List<Tank> tanks)
        {
            var result = (from t in tanks
                          where t.Name == name
                          select t).ToList();
            if (result.Count == 0)
            {
                Console.WriteLine("Error occured. Name not found"); /////////////////////////////////
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

        public static void GetList(List<Factory> factories)
        {
            foreach (var item in factories)
            {
                Console.WriteLine($" Id = {item.Id}, Name = {item.Name}, Description = {item.Description}");
            }
        }
        public static void GetList(List<Unit> units)
        {
            foreach (var item in units)
            {
                Console.WriteLine($" Id = {item.Id}, Name = {item.Name}, Factory = {item.Factory.Name}");
            }
        }
        public static void GetList(List<Tank> tanks)
        {
            foreach (var item in tanks)
            {
                Console.WriteLine($" Id = {item.Id}, Name = {item.Name}, Volume = {item.Volume}" +
                    $"Max volume = {item.MaxVolume}, Unit = {item.Unit.Name}");
            }
        }
    }
   
    
    
    public class Factory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Unit> Units { get; set; } = new List<Unit>();
    }

    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FactoryId { get; set; }
        public Factory Factory { get; set; }
        public List<Tank> Tanks { get; set; } = new List<Tank>();
    }

    public class Tank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Volume { get; set; }
        public decimal MaxVolume { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; }

    }


}
