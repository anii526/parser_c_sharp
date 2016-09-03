using Program.vo;
using System;
using System.Collections.Generic;

namespace Program
{
    class App
    {
        private DBConnect dbConnect;
        private Parser parser;

        public App()
        {
            Init();
        }

        private void Init()
        {
            parser = new Parser();
            parser.Start();

            dbConnect = new DBConnect();

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
                    Console.WriteLine("ID записи совпадает");
                    dbConnect.Update(cityVO);
                }
                else
                {
                    Console.WriteLine("ID записи не совпадает");
                    dbConnect.Insert(cityVO);
                }
                coincides = false;
            }

            cityVO = null;

            Console.WriteLine("Запись окончена");
            //Console.ReadKey();

        }
    }
}
