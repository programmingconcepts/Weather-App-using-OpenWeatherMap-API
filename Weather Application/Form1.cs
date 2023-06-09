using Newtonsoft.Json;
using System;
using System.Net;
using System.Windows.Forms;

namespace Weather_Application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string API = "Your API Key Here";

        private void btnGo_Click(object sender, EventArgs e)
        {
            getWeather();
        }

        public void getWeather()
        {
            using (WebClient web = new WebClient())
            {
                string url = String.Format(@"http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", TBCity.Text, API);
                var json = web.DownloadString(url);
                var results = JsonConvert.DeserializeObject<Weather_Info.root>(json);

                Weather_Info.root root = results;

                labCondition.Text = root.weather[0].main;

                labDetails.Text = root.weather[0].description;

                labTemp.Text = root.main.temp.ToString() + " K";
                labHumidity.Text = root.main.humidity.ToString();
                labSunrise.Text = getDateTime(root.sys.sunrise).ToShortTimeString();
                labSunset.Text = getDateTime(root.sys.sunset).ToShortTimeString();
                labWindSpeed.Text = root.wind.speed.ToString() + "m/s";

                picWeatherIcon.ImageLocation = "http://openweathermap.org/img/w/" + root.weather[0].icon + ".png";
            }
        }

        public DateTime getDateTime(double millisec)
        {
            DateTime day = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToLocalTime();
            day = day.AddMilliseconds(millisec).ToLocalTime();

            return day;
        }
    }
}
