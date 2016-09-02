using Newtonsoft.Json;
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
            //string json;
            /*for (int i = 0; i < parser.arrAllCitiesInfo.Length; i++)
            {
                cityVO = parser.arrAllCitiesInfo[i];
                //json = JsonConvert.SerializeObject(cityVO.Weather);
                //Console.WriteLine(json);
                dbConnect.Insert(cityVO);
                //WeatherVO[] weather = JsonConvert.DeserializeObject<WeatherVO[]>(json);
                //Console.WriteLine(json);
            }*/

            List<string>[] list;
            list = dbConnect.Select("SELECT * FROM cities");

            Console.WriteLine("list " + list[1][0]);
            WeatherVO[] weather = JsonConvert.DeserializeObject<WeatherVO[]>(list[7][0]);
            Console.WriteLine("list " + list[0]);
            //dbConnect.Insert();
            //*/
            Console.WriteLine("Запись окончена");
            Console.ReadKey();
        }
    }
}
