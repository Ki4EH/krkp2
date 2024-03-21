using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        static void task1()
        {
            using (var db = new RealEstateDbContext())
            {
                double minPrice = 0;
                double maxPrice = 1000000000;

                string districtName = "Кировский";
                Console.WriteLine("Задание 1:");
                var realEstateQuery = from realestate in db.RealEstateObject
                                      where realestate.District.DistrictName == districtName
                                      && realestate.Price >= minPrice && realestate.Price <= maxPrice
                                      orderby realestate.Price descending
                                      select new
                                      {
                                          realestate.Address,
                                          realestate.Area,
                                          realestate.Floor
                                      };

                Console.WriteLine($"Найденные объекты недвижимости в районе {districtName} со стоимостью от {minPrice:C} до {maxPrice:C}, отсортированные по убыванию стоимости:");

                foreach (var item in realEstateQuery)
                {
                    Console.WriteLine($"Адрес: {item.Address}, Площадь: {item.Area}, Этаж: {item.Floor}");
                }
            }
        }

        static void task2()
        {
            using (var db = new RealEstateDbContext())
            {
                int num = 2;
                Console.WriteLine("Задание 2:");
                var relAgents = db.Sale.Where(s => s.RealEstateObject.RoomCount == num).Select(s => new { Surname = s.Realtor.LastName, Name = s.Realtor.FirstName, Patronymic = s.Realtor.MiddleName });
                foreach (var item in relAgents)
                {
                    Console.WriteLine($"Фамилия: {item.Surname}, Имя: {item.Name}, Отчество: {item.Patronymic}");
                };
            }
        }

        static void task3()
        {
            using (var db = new RealEstateDbContext())
            {
                Console.WriteLine("Задание 3:");
                int num = 2;
                string region = "Кировский";
                var totalCost = (from estate in db.RealEstateObject
                                 where estate.RoomCount == num && estate.District.DistrictName == region
                                 join sale in db.Sale on estate.ObjectCode equals sale.ObjectCode
                                 select sale.Price).Sum();
                Console.WriteLine($"Общая стоимость {num} кв в районе {region} составила {totalCost}");
            };

        }

        static void task4()
        {
            using (var db = new RealEstateDbContext())
            {
                Console.WriteLine("Задание 4:");
                string realtorName = "Фамилия1";
                var ans = db.Sale
                    .Where(sale => sale.Realtor.LastName == realtorName)
                    .Select(s => s.RealEstateObject.Price);
                decimal maxC = realtorName.Max();
                decimal minC = realtorName.Min();
                Console.WriteLine($"Максимальная стоимость объекта у риелтора {realtorName} - {maxC}");
                Console.WriteLine($"Минимальная стоимость объекта у риелтора {realtorName} - {minC}");
            };
        }

        static void task5()
        {
            using (var db = new RealEstateDbContext())
            {

                Console.WriteLine("Задание 5:");
                string realtorName = "Фамилия2";
                string propertyType = "Апартаменты";
                string criterionName = "Экология";

                var averageRating = db.Evaluation
                .Join(db.Sale,
                      rating => rating.ObjectCode,
                      sale => sale.ObjectCode,
                      (rating, sale) => new { Rating = rating, Sale = sale })
                .Where(rs => rs.Sale.Realtor.LastName == realtorName &&
                             rs.Rating.RealEstateObject.Type.TypeName == propertyType &&
                             rs.Rating.EvaluationCriterion.CriterionName == criterionName)
                .Select(rs => rs.Rating.Score)
                .Average();
                Console.WriteLine($"Средняя оценка по критерию {criterionName}, риэлтором {realtorName}, составляет {averageRating}");
            };
        }

        static void task6()
        {
            using (var db = new RealEstateDbContext())
            {
                Console.WriteLine("Задание 6:");
                int floor = 2;
                string estTip = "Квартира";
                var estCnt = db.RealEstateObject
                .Where(c => c.Floor == floor && c.Type.TypeName == estTip)
                .Select(r => new { Region = r.District.DistrictName })
                .GroupBy(info => info.Region)
                .Select(s => new { Region = s.Key, cnt = s.Count() });

                foreach (var d in estCnt)
                {
                    Console.WriteLine($"Район {d.Region} имеет {d.cnt} квартир на втором этаже");
                };
            };
        }

        static void task7()
        {
            using (var db = new RealEstateDbContext()) {
                Console.WriteLine("Задание 7:");

                string buildType = "Квартира";
                using (var context = new RealEstateDbContext())
                {
                    var kvCnt = db.Sale
                        .Where(sale => sale.RealEstateObject.Type.TypeName == buildType)
                        .GroupBy(s => s.Realtor.LastName)
                        .Select(c => new { Agent = c.Key, cnt = c.Count() });

                    foreach (var d in kvCnt)
                    {
                        Console.WriteLine($"Агент {d.Agent} продал {d.cnt} квартир");
                    }
                }
            };
        };

        static void task8()
        {
            using (var db = new RealEstateDbContext()) { 
                Console.WriteLine("Задание 8:");
                var expObj = db.RealEstateObject
                .GroupBy(r => r.District.DistrictName)
                .Select(g => new
                {
                    Region = g.Key,
                    res = g.OrderByDescending(c => c.Price).Take(3)
                });

                foreach (var r in expObj)
                {
                    Console.WriteLine($"Самые дорогие объекты района {r.Region}: ");
                    foreach (var ans in r.res)
                    {
                        Console.WriteLine($"Адрес: {ans.Address}, стоимость: {ans.Price}, этаж {ans.Floor}");
                    }
                };
            };
        };

        static void task9() { Console.WriteLine("Задание 9:");
        
            using (var db = new RealEstateDbContext()) {

                var ans = db.Sale
                .GroupBy(s => new { Agent = s.Realtor.LastName, Year = s.SaleDate.Year })
                .Where(g => g.Count() > 1)
                .Select(g => new { Group = g.Key, Count = g.Count() });

                foreach (var a in ans)
                {
                    Console.WriteLine($"Риэлтор {a.Group.Agent} продал {a.Count} объектов в {a.Group.Year} году");
                }
            };

        };

        static void task10()
        {
            using (var db = new RealEstateDbContext()) {

                Console.WriteLine("Задание 10:");
                var ch = db.RealEstateObject
                .GroupBy(e => new { Year = e.ListingDate.Year })
                .Where(g => g.Count() >= 2 && g.Count() <= 3)
                .Select(g => new { Group = g.Key, Count = g.Count() });

                foreach (var a in ch)
                {
                    Console.WriteLine($"В {a.Group.Year} году было размещено {a.Count} объявления.");
                }
            };
        }

        static void task11()
        {
            using (var db = new RealEstateDbContext())
            {
                Console.WriteLine("Задание 11:");
                var rel = db.Sale
                .Join(db.RealEstateObject,
                      sale => sale.ObjectCode,
                      estate => estate.ObjectCode,
                      (sale, estate) => new { Sale = sale, Estate = estate })
                .Where(cr => Math.Max(cr.Sale.Price, cr.Estate.Price) / Math.Min(cr.Sale.Price, cr.Estate.Price) <= 1.2)
                .Select(d => new { Estate = d.Estate, Region = d.Estate.District.DistrictName });

                foreach (var ans in rel)
                {
                    Console.WriteLine($"Адрес: {ans.Estate.Address}, район: {ans.Region}");
                }
            };
        };

        static void task12()
        {
            using (var db = new RealEstateDbContext()) { 
                Console.WriteLine("Задание 12:");
                var averageCostPerSquareMeterByDistrict = db.RealEstateObject
                    .GroupBy(e => e.DistrictCode)
                    .Select(group => new
                       {
                        DistrictCode = group.Key,
                        AverageCostPerSquareMeter = group.Select(e => e.Price / e.Area).Average(),
                        Estates = group.Where(e => e.Price / e.Area < group.Average(g => g.Price / g.Area))
                    .Select(estate => new
                    {
                      Address = estate.Address,
                      CostPerSquareMeter = estate.Price / estate.Area
                   }).ToList()}).ToList();

                foreach (var district in averageCostPerSquareMeterByDistrict)
                {
                    Console.WriteLine($"Средняя цена за метр {district.DistrictCode}: {district.AverageCostPerSquareMeter}");
                    foreach (var estate in district.Estates)
                    {
                        Console.WriteLine(estate.Address);
                    }
                }


            };
        };

        static void task13()
        {
            using (var db = new RealEstateDbContext()) { Console.WriteLine("Задание 13:");
                int year = 2024;
                var rel = db.Realtor
                .Where(realtor => !db.Sale.Any(sale => sale.Realtor.RealtorCode == realtor.RealtorCode && sale.SaleDate.Year == year))
                .Select(realtor => new
                {
                    FullName = $"{realtor.LastName} {realtor.FirstName} {realtor.MiddleName}"
                })
                .ToList();

                foreach (var realtor in rel)
                {
                    Console.WriteLine($"Риэлтор: {realtor.FullName}");
                }
            };
        };

        static void task14()
        {
            using (var db = new RealEstateDbContext()) {

                Console.WriteLine("Задание 14:");

                int currentYear = DateTime.Now.Year;
                int previousYear = currentYear - 1;

                var salesByRegionCurrentYear = db.Sale
                 .Where(sale => sale.SaleDate.Year == currentYear)
                 .Join(db.RealEstateObject,
                       sale => sale.ObjectCode,
                       estate => estate.ObjectCode,
                       (sale, estate) => new { Sale = sale, Estate = estate })
                 .GroupBy(x => x.Estate.District.DistrictName)
                 .Select(group => new
                 {
                     Region = group.Key,
                     SalesCountCurrentYear = group.Count()
                 })
                 .ToDictionary(region => region.Region!, region => region.SalesCountCurrentYear);

                var salesByRegionPreviousYear = db.Sale
                    .Where(sale => sale.SaleDate.Year == previousYear)
                    .Join(db.RealEstateObject,
                          sale => sale.ObjectCode,
                          estate => estate.ObjectCode,
                          (sale, estate) => new { Sale = sale, Estate = estate })
                    .GroupBy(x => x.Estate.District.DistrictName)
                    .Select(group => new
                    {
                        Region = group.Key,
                        SalesCountPreviousYear = group.Count()
                    })
                    .ToDictionary(region => region.Region!, region => region.SalesCountPreviousYear);

                foreach (var region in salesByRegionCurrentYear.Keys)
                {
                    int currentYearSales = salesByRegionCurrentYear.ContainsKey(region) ? salesByRegionCurrentYear[region] : 0;
                    int previousYearSales = salesByRegionPreviousYear.ContainsKey(region) ? salesByRegionPreviousYear[region] : 0;

                    double percentChange = previousYearSales != 0 ? (currentYearSales - previousYearSales) / (double)previousYearSales * 100 : 0;

                    Console.WriteLine($"Район: {region}");
                    Console.WriteLine($"Продаж в текущем году: {currentYearSales}");
                    Console.WriteLine($"Продаж в предыдущем году: {previousYearSales}");
                    Console.WriteLine($"Процент изменения: {percentChange}%");
                }
            };
        };

        static string GetEquivalentText(double averageRating)
        {
            if (averageRating >= 9)
            {
                return "превосходно";
            }
            else if (averageRating >= 8)
            {
                return "очень хорошо";
            }
            else if (averageRating >= 7)
            {
                return "хорошо";
            }
            else if (averageRating >= 6)
            {
                return "удовлетворительно";
            }
            else
            {
                return "неудовлетворительно";
            }
        }

        static void task15() { Console.WriteLine("Задание 15:"); 
        
            using (var db = new RealEstateDbContext()) {

                int estateId = 30;
                var averageRatingByCriterion = db.Evaluation
                .Where(evaluation => evaluation.RealEstateObject.ObjectCode == estateId)
                .GroupBy(evaluation => evaluation.EvaluationCriterion.CriterionName)
                .Select(group => new
                {
                    Criterion = group.Key,
                    AverageRating = group.Average(evaluation => evaluation.Score)
                });

                Console.WriteLine("Критерий\tСредняя оценка\tТекст");

                foreach (var rating in averageRatingByCriterion)
                {
                    string equivalentText = GetEquivalentText(rating.AverageRating);
                    Console.WriteLine($"{rating.Criterion}\t{rating.AverageRating:F1} из 10\t{equivalentText}");
                }

            };

        };

        static void test()
        {
            using (var db = new RealEstateDbContext())
            {
                string lastName = "Фамилия2";
                var ans = db.Sale
                .GroupBy(s => new { Agent = s.Realtor.LastName})
                .Where(g => g.Count() > 0)
                .Select(g => new { Group = g.Key, Count = g.Count() });

                foreach (var pair in ans) 
                    Console.WriteLine($"Агент: {pair.Group.Agent} продал {pair.Count}");
                };

            };



        test();  

    }
}