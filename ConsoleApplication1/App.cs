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
            //parser = new Parser();
            //parser.Start();
            
            dbConnect = new DBConnect();

            List<string>[] list;
            list = dbConnect.Select("SELECT Code,Name,Continent,Region FROM country");

            Console.WriteLine("list " + list[0]);

            Console.ReadKey();
        }
    }
}
