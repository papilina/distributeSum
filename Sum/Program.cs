using System;
using System.Collections.Generic;
using System.Linq;

namespace Sum
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //исходные данные
            double sumForDistribution = 10000;
            string initialSumPosition = "1000;2000;3000;5000;8000;5000";

            List<double> sumList = initialSumPosition.Split(";").Select(x => double.Parse(x)).ToList();
            List<double> resultDistribution = new();

            Console.WriteLine("Введите тип распределения: ПРОП, ПЕРВ или ПОСЛ");
            string distributionType = Console.ReadLine();
            
            switch (distributionType.ToUpper())
            {
                case "ПРОП":
                    resultDistribution = DistributeProportionally(sumForDistribution, sumList);
                    break;
                case "ПЕРВ":
                    resultDistribution = DistributeInSequence(sumForDistribution, sumList);
                    break;
                case "ПОСЛ":
                    sumList.Reverse();
                    resultDistribution = DistributeInSequence(sumForDistribution, sumList);
                    resultDistribution.Reverse();
                    break;
                default:
                    Console.WriteLine("Неверный тип распределения");
                    break;
            }
            ShowResultDistribution(resultDistribution);
             
            Console.ReadKey();
        }
        static List<double> DistributeProportionally(double sumForDistribution, List<double> sumList)
        {
            double commonSum = sumList.Sum();
            double reminderSum = sumForDistribution;
            var resultList = new List<double>();
            for (int i = 0; i < sumList.Count - 1; i++)
            {
                double proportion = sumList[i] / commonSum;
                double resultSumItem = Math.Round(sumForDistribution * proportion, 2);
                reminderSum -= resultSumItem;
                resultList.Add(resultSumItem);
            }
            resultList.Add(reminderSum);
            
            return resultList;
        }

        static List<double> DistributeInSequence(double sumForDistribution, List<double> sumList)
        {
            var resultSum = new List<double>();
            var isNegative = false;

            foreach (double sumItem in sumList)
            {
                if (isNegative)
                {
                    resultSum.Add(0);
                    continue;
                }
                sumForDistribution -= sumItem;
                if (sumForDistribution > 0)
                {
                    resultSum.Add(sumItem);
                } 
                else 
                {
                    resultSum.Add(sumItem + sumForDistribution);
                    isNegative = true;
                }
            }
            return resultSum;
        }

        static void ShowResultDistribution(List<double> resultDistribution)
        {
            if (resultDistribution.Count > 0)
            {
                Console.WriteLine(string.Join(";", resultDistribution));
            }
        }
    }
}
