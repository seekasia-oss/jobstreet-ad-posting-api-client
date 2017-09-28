﻿using System;

namespace JobStreet.AdPostingApi.Client.Models
{
    [Serializable]
    public class AdvertisementError
    {
        public string Field { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }
    }
}