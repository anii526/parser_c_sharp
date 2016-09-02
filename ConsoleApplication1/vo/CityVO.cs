using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.vo
{
    class CityVO : IVO
    {
        private string indexCity;
        private string nameCity;
        private string imgSrcWeather;
        private string day;
        private string dayWeek;
        private string temperatureMin;
        private string temperatureMax;

        private WeatherVO[] weather;

        public string NameCity
        {
            get
            {
                return nameCity;
            }

            set
            {
                nameCity = value;
            }
        }

        public string ImgSrcWeather
        {
            get
            {
                return imgSrcWeather;
            }

            set
            {
                imgSrcWeather = value;
            }
        }

        public string Day
        {
            get
            {
                return day;
            }

            set
            {
                day = value;
            }
        }

        public string DayWeek
        {
            get
            {
                return dayWeek;
            }

            set
            {
                dayWeek = value;
            }
        }

        public string TemperatureMin
        {
            get
            {
                return temperatureMin;
            }

            set
            {
                temperatureMin = value;
            }
        }

        public string TemperatureMax
        {
            get
            {
                return temperatureMax;
            }

            set
            {
                temperatureMax = value;
            }
        }

        internal WeatherVO[] Weather
        {
            get
            {
                return weather;
            }

            set
            {
                weather = value;
            }
        }

        public string IndexCity
        {
            get
            {
                return indexCity;
            }

            set
            {
                indexCity = value;
            }
        }
    }
}
