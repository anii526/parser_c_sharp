﻿using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Program.vo;
using Newtonsoft.Json;

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

            string query = "INSERT INTO cities (IdCity, NameCity, ImgSrcWeather, Day, DayWeek, TemperatureMin, TemperatureMax, Weather  ) VALUES( @IdCity,@NameCity,@ImgSrcWeather,@Day,@DayWeek,@TemperatureMin,@TemperatureMax,@Weather)";

            if (this.OpenConnection() == true)
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@IdCity", cityVO.IndexCity);
                        cmd.Parameters.AddWithValue("@NameCity", cityVO.NameCity);
                        cmd.Parameters.AddWithValue("@ImgSrcWeather", cityVO.ImgSrcWeather);
                        cmd.Parameters.AddWithValue("@Day", cityVO.Day);
                        cmd.Parameters.AddWithValue("@DayWeek", cityVO.DayWeek);
                        cmd.Parameters.AddWithValue("@TemperatureMin", cityVO.TemperatureMin);
                        cmd.Parameters.AddWithValue("@TemperatureMax", cityVO.TemperatureMax);
                        cmd.Parameters.AddWithValue("@Weather", JsonConvert.SerializeObject(cityVO.Weather));
                        // выполнить с возвратом данных 
                        //Object o = cmd.ExecuteScalar();
                        //Console.WriteLine(cmd.Parameters["@IdCity"].Value);

                        // не возвращать данные 
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error: {0}", ex.ToString());
                }
                finally
                {
                    this.CloseConnection();
                }
            }
        }

        //обновление данных
        public void Update(CityVO cityVO)
        {
            string query = "UPDATE cities SET IdCity = @IdCity, NameCity = @NameCity, ImgSrcWeather = @ImgSrcWeather, Day = @Day, DayWeek = @DayWeek, TemperatureMin = @TemperatureMin, TemperatureMax = @TemperatureMax, Weather = @Weather WHERE IdCity= @IdCity";

            if (this.OpenConnection() == true)
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@IdCity", cityVO.IndexCity);
                        cmd.Parameters.AddWithValue("@NameCity", cityVO.NameCity);
                        cmd.Parameters.AddWithValue("@ImgSrcWeather", cityVO.ImgSrcWeather);
                        cmd.Parameters.AddWithValue("@Day", cityVO.Day);
                        cmd.Parameters.AddWithValue("@DayWeek", cityVO.DayWeek);
                        cmd.Parameters.AddWithValue("@TemperatureMin", cityVO.TemperatureMin);
                        cmd.Parameters.AddWithValue("@TemperatureMax", cityVO.TemperatureMax);
                        cmd.Parameters.AddWithValue("@Weather", JsonConvert.SerializeObject(cityVO.Weather));
                        // выполнить с возвратом данных 
                        //Object o = cmd.ExecuteScalar();
                        //Console.WriteLine(cmd.Parameters["@IdCity"].Value);

                        // не возвращать данные 
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error: {0}", ex.ToString());
                }
                finally
                {
                    this.CloseConnection();
                }
            }
        }

        //выборка данных
        public List<string>[] Select(string _query, string[] _nameParams)
        {
            string query = _query;
            string[] nameParams = _nameParams;

            List<string>[] list = new List<string>[_nameParams.Length];

            for (int i = 0; i < _nameParams.Length; i++)
            {
                list[i] = new List<string>();
            }

            if (this.OpenConnection() == true)
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                for (int i = 0; i < _nameParams.Length; i++)
                                {
                                    list[i].Add(dataReader[_nameParams[i]] + "");
                                }
                            }

                            dataReader.Close();
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error: {0}", ex.ToString());
                }
                finally
                {
                    this.CloseConnection();
                }

                return list;
            }
            else
            {
                return list;
            }
        }
    }
}
