﻿using Program.vo;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Program
{
    class App
    {
        private DBConnect dbConnect;
        private Parser parser;
        private int interval;

        public App()
        {
            parser = new Parser();

            dbConnect = new DBConnect();

            //4 раза в сутки будет парситься, т.е. период 21600000 миллисекунд
            //2 минуты 120000 миллисекунд
            interval = 21600000;

            Timer timer = new Timer(TimerCallback, null, 0, interval);

            Console.ReadLine();
        }

        private void TimerCallback(Object o)
        {
            Console.WriteLine("Парсер начал свою работу " + DateTime.Now);
            Console.WriteLine("Потребуется немного времени для его работы");
            Init();
            Console.WriteLine("Парсинг свою работу закончил");
            Console.WriteLine("Следующий запуск ожидается: " + DateTime.Now.AddMilliseconds(interval));
            Console.WriteLine();
        }

        private void Init()
        {            
            parser.Start();

            List<string>[] list;
            // в Select надо передавать массив с именами параметров, которые необходимо получить с бд.
            // это делалось с целью хоть небольшой, но универсальности метода Select
            list = dbConnect.Select("SELECT IdCity FROM cities", new string[] { "IdCity" });

            CityVO cityVO;
            bool coincides = false;
            for (int i = 0; i < parser.arrAllCitiesInfo.Length; i++)
            {
                cityVO = parser.arrAllCitiesInfo[i];

                //этот массив необходим в данной реализации по причине возможной разницы в длинне массивов из бд и распарсенного
                for (int j = 0; j < list[0].Count; j++)
                {
                    //list[0] - содержит параметр который передается первым в Select, а именно IdCity
                    if (list[0][j] == cityVO.IndexCity)
                        coincides = true;
                }

                if (coincides == true)
                {
                    //Console.WriteLine("ID записи совпадает");
                    dbConnect.Update(cityVO);
                }
                else
                {
                    //Console.WriteLine("ID записи не совпадает");
                    dbConnect.Insert(cityVO);
                }
                coincides = false;
            }

            cityVO = null;

            //Console.WriteLine("Запись окончена");
        }
    }
}
