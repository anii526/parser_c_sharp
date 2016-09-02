using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.vo
{
    class WeatherVO : IVO
    {
        private string timeOfDay;
        private string imgSrcWeather;
        private string featureWeather;
        private string temperatureAir;
        private string pressure;
        private string directionWind;
        private string speedWind;
        private string humidity;
        private string temperatureFeels;

        public string TimeOfDay
        {
            get
            {
                return timeOfDay;
            }

            set
            {
                timeOfDay = value;
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

        public string FeatureWeather
        {
            get
            {
                return featureWeather;
            }

            set
            {
                featureWeather = value;
            }
        }

        public string TemperatureAir
        {
            get
            {
                return temperatureAir;
            }

            set
            {
                temperatureAir = value;
            }
        }

        public string Pressure
        {
            get
            {
                return pressure;
            }

            set
            {
                pressure = value;
            }
        }

        public string DirectionWind
        {
            get
            {
                return directionWind;
            }

            set
            {
                directionWind = value;
            }
        }

        public string SpeedWind
        {
            get
            {
                return speedWind;
            }

            set
            {
                speedWind = value;
            }
        }

        public string Humidity
        {
            get
            {
                return humidity;
            }

            set
            {
                humidity = value;
            }
        }

        public string TemperatureFeels
        {
            get
            {
                return temperatureFeels;
            }

            set
            {
                temperatureFeels = value;
            }
        }
    }
}
