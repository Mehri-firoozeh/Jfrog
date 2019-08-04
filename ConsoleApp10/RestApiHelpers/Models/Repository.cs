using System;
using System.Collections.Generic;

namespace ConsoleApp10.RestApiHelpers.Models
{
    public class Result
    {
        public string repo { get; set; }
        public string path { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int size { get; set; }
        public DateTime created { get; set; }
        public string created_by { get; set; }
        public DateTime modified { get; set; }
        public string modified_by { get; set; }
        public DateTime updated { get; set; }
    }

    public class Range
    {
        public int start_pos { get; set; }
        public int end_pos { get; set; }
        public int total { get; set; }
    }

    public class Repsitory
    {
        public List<Result> results { get; set; }
        public Range range { get; set; }
    }



}
