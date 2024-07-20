using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SogetiService.Data
{
    
    

        public class Rootobject
        {
        [JsonPropertyName("$id")]
            public string id { get; set; }
            public object[] AvailCountries { get; set; }
            public Availservicearea[] AvailServiceAreas { get; set; }
            public Availcontenttype[] AvailContentTypes { get; set; }
            public Availsector[] AvailSectors { get; set; }
            public object AvailLocations { get; set; }
            public object PrimaryServiceArea { get; set; }
            public int TagCount { get; set; }
            public Tag[] Tags { get; set; }
            public string CountriesTitle { get; set; }
            public string ServiceAreaTitle { get; set; }
            public string ContentTypesTitle { get; set; }
            public string LocationTitle { get; set; }
            public string SectorsTitle { get; set; }
            public string ShowAllTitle { get; set; }
            public string FilterTitle { get; set; }
            public object Err { get; set; }
            public bool HideDate { get; set; }
            public object DateArray { get; set; }
            public string Level1Heading { get; set; }
            public string Level2Heading { get; set; }
        }

        public class Availservicearea
        {
        [JsonPropertyName("$id")]
        public string id { get; set; }
            public string Key { get; set; }
            public string Value { get; set; }
        }

        public class Availcontenttype
        {
        [JsonPropertyName("$id")]
        public string id { get; set; }
            public string Key { get; set; }
            public string Value { get; set; }
        }

        public class Availsector
        {
        [JsonPropertyName("$id")]
        public string id { get; set; }
            public string Key { get; set; }
            public string Value { get; set; }
        }
         public class Tag
         {
        [JsonPropertyName("$id")]
        public string id { get; set; }
            public object? Countries { get; set; }
            public string[] ServiceAreas { get; set; }
            public string[] ContentTypes { get; set; }
            public object Sectors { get; set; }
            public DateTime Date { get; set; }
            public string Intro { get; set; }
            public string ImageUrl { get; set; }
            public string ImageAlt { get; set; }
            public string Title { get; set; }
            public string PageUrl { get; set; }
        [JsonPropertyName("ID")]
        public int ID { get; set; }
            public string PrimaryServiceArea { get; set; }
            public object? Location { get; set; }
    }

}

