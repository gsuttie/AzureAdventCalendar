using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AzureAdventCalendarWeb.Models
{
    public class RootObject
    {
        [JsonProperty(PropertyName = "question")]
        public string Question { get; set; }

        [Display(Name = "Answer")]
        [JsonProperty(PropertyName = "answers")]
        public List<Answer> answers { get; set; }
    }

    public class Answer
    {
        [JsonProperty(PropertyName = "questions")]
        public List<string> questions { get; set; }
        [Display(Name = "Answer")]
        [JsonProperty(PropertyName = "answer")]
        public string answer { get; set; }
    }
}
