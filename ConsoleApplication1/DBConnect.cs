using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Program.vo;

namespace Program
{
    class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public DBConnect()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "localhost";
            database = "gismeteo";
            //database = "world";
            uid = "root";
            password = "root";
            string connectionString;
            connectionString = 
                "SERVER=" + server + ";" + 
                "DATABASE=" + database + ";" +
                "UID=" + uid + ";" + 
                "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }


        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Невозможно соединиться с сервером.");
                        break;

                    case 1045:
                        Console.WriteLine("Неверное имя пользователя / пароль");
                        break;
                }
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // вставка данных
        public void Insert(CityVO cityVO)
        {
            //Console.WriteLine("INSERT INTO cities (IdCity, NameCity, ImgSrcWeather, Day, DayWeek, TemperatureMin, TemperatureMax  ) VALUES( " + cityVO.IndexCity + "," + cityVO.NameCity + "," + cityVO.ImgSrcWeather + "," + cityVO.Day + "," + cityVO.DayWeek + "," + cityVO.TemperatureMin + "," + cityVO.TemperatureMax + ")");

            string query = "INSERT INTO cities (IdCity, NameCity, ImgSrcWeather, Day, DayWeek, TemperatureMin, TemperatureMax  ) VALUES( @IndexCity,@NameCity,@ImgSrcWeather,@Day,@DayWeek,@TemperatureMin,@TemperatureMax)";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@IndexCity", cityVO.IndexCity);
                cmd.Parameters.AddWithValue("@NameCity", cityVO.NameCity);
                cmd.Parameters.AddWithValue("@ImgSrcWeather", cityVO.ImgSrcWeather);
                cmd.Parameters.AddWithValue("@Day", cityVO.Day);
                cmd.Parameters.AddWithValue("@DayWeek", cityVO.DayWeek);
                cmd.Parameters.AddWithValue("@TemperatureMin", cityVO.TemperatureMin);
                cmd.Parameters.AddWithValue("@TemperatureMax", cityVO.TemperatureMax);
                // выполнить с возвратом данных 
                //Object o = cmd.ExecuteScalar();
                //Console.WriteLine(cmd.Parameters["@IdCity"].Value);

                // не возвращать данные 
                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }

        //обновление данных
        public void Update()
        {
            

            string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

            if (this.OpenConnection() == true)
            {
                /*//create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();*/
                MySqlCommand cmd = new MySqlCommand(query, connection);

                // не возвращать данные 
                cmd.ExecuteNonQuery();

                // выполнить с возвратом данных 
                //cmd.ExecuteScalar()

                this.CloseConnection();
            }
        }

        //выборка данных
        public List<string>[] Select(string _query)
        {
            string query = _query;

            List<string>[] list = new List<string>[7];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
            list[4] = new List<string>();
            list[5] = new List<string>();
            list[6] = new List<string>();

            if (this.OpenConnection() == true)
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            list[0].Add(dataReader["IdCity"] + "");
                            list[1].Add(dataReader["NameCity"] + "");
                            list[2].Add(dataReader["ImgSrcWeather"] + "");
                            list[3].Add(dataReader["Day"] + "");
                            list[4].Add(dataReader["DayWeek"] + "");
                            list[5].Add(dataReader["TemperatureMin"] + "");
                            list[6].Add(dataReader["TemperatureMax"] + "");

                            /*list[0].Add(dataReader["Code"] + "");
                            list[1].Add(dataReader["Name"] + "");
                            list[2].Add(dataReader["Continent"] + "");
                            list[3].Add(dataReader["Region"] + "");*/
                        }

                        dataReader.Close();
                    }
                }

                this.CloseConnection();

                return list;
            }
            else
            {
                return list;
            }
        }

        //Count statement
        public int Count()
        {
            string query = "SELECT Count(*) FROM tableinfo";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }
    }
}
