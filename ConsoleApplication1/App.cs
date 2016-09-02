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

            CityVO cityVO; 
            for (int i = 0; i < parser.arrAllCitiesInfo.Length; i++)
            {
                cityVO = parser.arrAllCitiesInfo[i];
                dbConnect.Insert(cityVO);
            }

            List<string>[] list;
            list = dbConnect.Select("SELECT * FROM cities");

            Console.WriteLine("list " + list[1][0]);
            Console.WriteLine("list " + list[0]);
            //dbConnect.Insert();

            //Console.ReadKey();
        }
    }
}
