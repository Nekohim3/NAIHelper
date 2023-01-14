using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAIHelper.ViewModels;
using Newtonsoft.Json;
using ReactiveUI;

namespace NAIHelper.Utils
{
    [JsonObject]
    public class Settings : ViewModelBase
    {
        #region Nai web api

        #region NaiHelper side

        private string _apiAddress;
        public string ApiAddress
        {
            get => _apiAddress;
            set => this.RaiseAndSetIfChanged(ref _apiAddress, value);
        }

        private string _apiPort;
        public string ApiPort
        {
            get => _apiPort;
            set => this.RaiseAndSetIfChanged(ref _apiPort, value);
        }

        #endregion

        #region NaiApi side

        private string _databaseHost;
        public string DatabaseHost
        {
            get => _databaseHost;
            set => this.RaiseAndSetIfChanged(ref _databaseHost, value);
        }

        private string _databasePort;
        public string DatabasePort
        {
            get => _databasePort;
            set => this.RaiseAndSetIfChanged(ref _databasePort, value);
        }

        private string _databaseName;
        public string DatabaseName
        {
            get => _databaseName;
            set => this.RaiseAndSetIfChanged(ref _databaseName, value);
        }

        private string _databaseUsername;
        public string DatabaseUsername
        {
            get => _databaseUsername;
            set => this.RaiseAndSetIfChanged(ref _databaseUsername, value);
        }

        private string _databasePassword;
        public string DatabasePassword
        {
            get => _databasePassword;
            set => this.RaiseAndSetIfChanged(ref _databasePassword, value);
        }

        #endregion

        #endregion

        #region Booru

        private string _danbooruMainBaseAddress;
        public string DanbooruMainBaseAddress
        {
            get => _danbooruMainBaseAddress;
            set => this.RaiseAndSetIfChanged(ref _danbooruMainBaseAddress, value);
        }

        private string _danbooruCdnBaseAddress;
        public string DanbooruCdnBaseAddress
        {
            get => _danbooruCdnBaseAddress;
            set => this.RaiseAndSetIfChanged(ref _danbooruCdnBaseAddress, value);
        }

        private string _gelbooruMainBaseAddress;
        public string GelbooruMainBaseAddress
        {
            get => _gelbooruMainBaseAddress;
            set => this.RaiseAndSetIfChanged(ref _gelbooruMainBaseAddress, value);
        }

        private string _gelbooruCdnBaseAddress;
        public string GelbooruCdnBaseAddress
        {
            get => _gelbooruCdnBaseAddress;
            set => this.RaiseAndSetIfChanged(ref _gelbooruCdnBaseAddress, value);
        }

        private string _danbooruApiKey;
        public string DanbooruApiKey
        {
            get => _danbooruApiKey;
            set => this.RaiseAndSetIfChanged(ref _danbooruApiKey, value);
        }

        private string _danbooruUsername;
        public string DanbooruUsername
        {
            get => _danbooruUsername;
            set => this.RaiseAndSetIfChanged(ref _danbooruUsername, value);
        }

        private string _gelbooruApiKey;
        public string GelbooruApiKey
        {
            get => _gelbooruApiKey;
            set => this.RaiseAndSetIfChanged(ref _gelbooruApiKey, value);
        }

        #endregion

        public void RestoreDefaults()
        {
            ApiAddress              = "http://localhost";
            ApiPort                 = "5022";

            DatabaseHost            = "localhost";
            DatabasePort            = "5432";
            DatabaseName            = "NovelAIHelper";
            DatabaseUsername        = "postgres";
            DatabasePassword        = "KuroNeko2112@";

            DanbooruMainBaseAddress = "https://danbooru.donmai.us";
            DanbooruCdnBaseAddress  = "https://cdn.donmai.us";
            DanbooruUsername        = "kutzumi";
            DanbooruApiKey          = "RxPfRonMmTUDDrkXQrUyVBcv";

            GelbooruMainBaseAddress = "https://gelbooru.com";
            GelbooruCdnBaseAddress  = "https://img3.gelbooru.com";
            GelbooruApiKey          = "api_key=390b13ee5570f153c03480e803e08a5545a8827f148594f80336880b02451c77&user_id=1166412";
        }
    }
}
