using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

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
            //database = "gismeteo";
            database = "world";
            uid = "root";
            password = "root";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

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
                        Console.WriteLine("���������� ����������� � ��������.");
                        break;

                    case 1045:
                        Console.WriteLine("�������� ��� ������������ / ������");
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

        // ������� ������
        public void Insert()
        {
            string query = "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                // ��������� � ��������� ������ 
                //cmd.ExecuteScalar()

                // �� ���������� ������ 
                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }

        //���������� ������
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

                // �� ���������� ������ 
                cmd.ExecuteNonQuery();

                // ��������� � ��������� ������ 
                //cmd.ExecuteScalar()

                this.CloseConnection();
            }
        }

        //������� ������
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
                            /*list[0].Add(dataReader["IdCity"] + "");
                            list[1].Add(dataReader["NameCity"] + "");
                            list[2].Add(dataReader["ImgSrcWeather"] + "");
                            list[3].Add(dataReader["Day"] + "");
                            list[4].Add(dataReader["DayWeek"] + "");
                            list[5].Add(dataReader["TemperatureMin"] + "");
                            list[6].Add(dataReader["TemperatureMax"] + "");*/

                            list[0].Add(dataReader["Code"] + "");
                            list[1].Add(dataReader["Name"] + "");
                            list[2].Add(dataReader["Continent"] + "");
                            list[3].Add(dataReader["Region"] + "");
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
