using System;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using AngleSharp.Parser.Html;
using AngleSharp.Dom.Html;
using Program.vo;

namespace Program
{
    class Parser
    {
        public CityVO[] arrAllCitiesInfo;

        public void Start()
        {
            //работа с сетью
            Console.WriteLine("Началась загрузка страницы https://www.gismeteo.ru/");

            //debug
            string response = "";
            response = getRequest("https://www.gismeteo.ru/");


            Console.WriteLine("Загрузка страницы закончилась");
            Console.WriteLine("Начинается парсинг главной страницы, цель => получить id городов");

            //debug
            //информация по брянску
            //string[] idsCities = new string[] { "4258" };
            string[] idsCities = getCitiesIDs(response);

            response = string.Empty;


            Console.WriteLine("ID городов получены всего их : " + idsCities.Length);
            Console.WriteLine("Начинаю загружать информацию о каждом из этих городов по списку");


            string[] htmlInfoOfCities = new string[idsCities.Length];

            for (int i = 0; i < htmlInfoOfCities.Length; i++)
            {
                htmlInfoOfCities[i] = getRequest("https://www.gismeteo.ru/city/daily/" + idsCities[i]);
                Console.WriteLine("Загрузилась информация о " + (i + 1) + " городе.");
            }

            Console.WriteLine("Информация о каждом из этих городов по списку успешно загружена");

            arrAllCitiesInfo = new CityVO[htmlInfoOfCities.Length];

            HtmlParser parser = new HtmlParser();

            for (int j = 0; j < htmlInfoOfCities.Length; j++)
            {
                // мб выглядит не ок, но я не хочу создавать много разных экземпляров HtmlParser лучше я буду передавать 1
                arrAllCitiesInfo[j] = getCityVO(parser, htmlInfoOfCities[j], idsCities[j]);
            }

            for (int i = 0; i < htmlInfoOfCities.Length; i++)
            {
                htmlInfoOfCities[i] = null;
            }

            for (int i = 0; i < idsCities.Length; i++)
            {
                // мб выглядит не ок, но я не хочу создавать много разных экземпляров HtmlParser лучше я буду передавать 1
                idsCities[i] = null;
            }

            htmlInfoOfCities = null;
            parser = null;

        }

        private string getRequest(string url)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                //debug чтобы быстрее работало
                //httpWebRequest.Timeout = 1000;
                using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (Stream stream = httpWebResponse.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // получаем статус исключения
                WebExceptionStatus status = ex.Status;

                if (status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)ex.Response;
                    Console.WriteLine("Статусный код ошибки: {0} - {1}",
                            (int)httpResponse.StatusCode, httpResponse.StatusCode);
                }

                return String.Empty;
            }
        }

        private string[] getCitiesIDs(string htmlData)
        {
            MatchCollection matches = Regex.Matches(htmlData, "\\/city\\/daily\\/([0-9]*)"); // /city/daily/[0-9]

            string[] allIDCity = new string[matches.Count];

            for (int i = 0; i < matches.Count; i++)
                allIDCity[i] = matches[i].Groups[1].Value;

            return allIDCity.Distinct().ToArray();
        }

        private CityVO getCityVO(HtmlParser parser, string dataHtml, string indexCity)
        {
            IHtmlDocument doc = parser.Parse(dataHtml);

            WeatherVO weatherVO;
            CityVO cityVO = new CityVO();

            cityVO.IndexCity = indexCity;
            cityVO.NameCity = doc.QuerySelector("#weather > div.fcontent > div.section.higher > h2").InnerHtml;

            cityVO.ImgSrcWeather = doc.QuerySelector("#tab_wdaily2 > img").GetAttribute("src");
            cityVO.Day = doc.QuerySelector("#tab_wdaily2 > dl > dd").InnerHtml;
            cityVO.DayWeek = doc.QuerySelector("#tab_wdaily2 > dl > dt").InnerHtml;

            cityVO.TemperatureMin = doc.QuerySelector("#tab_wdaily2 > div > span.value.m_temp.c").InnerHtml;
            cityVO.TemperatureMax = doc.QuerySelector("#tab_wdaily2 > div > em > span.value.m_temp.c").InnerHtml;

            WeatherVO[] weather = new WeatherVO[4];

            for (int i = 0; i < 4; i++)
            {
                weatherVO = new WeatherVO();
                weatherVO.TimeOfDay = doc.QuerySelector("#tbwdaily2 > tr:nth-child(" + (i + 1) + ") > th:nth-child(1)").InnerHtml.ToString().Trim();
                weatherVO.ImgSrcWeather = doc.QuerySelector("#tbwdaily2 > tr:nth-child(" + (i + 1) + ") > td:nth-child(2) > img:nth-child(1)").GetAttribute("src");
                weatherVO.FeatureWeather = doc.QuerySelector("#tbwdaily2 > tr:nth-child(" + (i + 1) + ") > td:nth-child(3)").InnerHtml;
                weatherVO.TemperatureAir = doc.QuerySelector("#tbwdaily2 > tr:nth-child(" + (i + 1) + ") > td:nth-child(4) > span:nth-child(1)").InnerHtml;
                weatherVO.Pressure = doc.QuerySelector("#tbwdaily2 > tr:nth-child(" + (i + 1) + ") > td:nth-child(5) > span:nth-child(1)").InnerHtml;
                weatherVO.DirectionWind = doc.QuerySelector("#tbwdaily2 > tr:nth-child(" + (i + 1) + ") > td:nth-child(6) > dl:nth-child(1) > dt:nth-child(1)").InnerHtml;
                weatherVO.SpeedWind = doc.QuerySelector("#tbwdaily2 > tr:nth-child(" + (i + 1) + ") > td:nth-child(6) > dl:nth-child(1) > dd:nth-child(2) > span:nth-child(1)").InnerHtml;
                weatherVO.Humidity = doc.QuerySelector("#tbwdaily2 > tr:nth-child(" + (i + 1) + ") > td:nth-child(7)").InnerHtml;
                weatherVO.TemperatureFeels = doc.QuerySelector("#tbwdaily2 > tr:nth-child(" + (i + 1) + ") > td:nth-child(8) > span:nth-child(1)").InnerHtml;
                weather[i] = weatherVO;
            }
            cityVO.Weather = weather.ToArray();

            for (int i = 0; i < 4; i++)
            {
                weather[i] = null;
            }
            weather = null;

            doc.Dispose();
            doc = null;

            return cityVO;
        }

        public Parser()
        {
        }
    }
}
