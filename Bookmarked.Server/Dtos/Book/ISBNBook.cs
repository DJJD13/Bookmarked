namespace Bookmarked.Server.Dtos.Book
{
    public class IsbnBook
    {
        public string publisher { get; set; } = string.Empty;
        public string synopsis { get; set; } = string.Empty;
        public string language { get; set; } = string.Empty;
        public string image { get; set; } = string.Empty;
        public string title_long { get; set; } = string.Empty;
        public string edition { get; set; } = string.Empty;
        public string dimensions { get; set; } = string.Empty;
        public DimensionsStructured dimensions_structured { get; set; }
        public int pages { get; set; }
        public string date_published { get; set; } = string.Empty;
        public List<string> authors { get; set; } = [];
        public string title { get; set; } = string.Empty;
        public string isbn13 { get; set; } = string.Empty;
        public double msrp { get; set; }
        public string binding { get; set; } = string.Empty;
        public Related related { get; set; }
        public string isbn { get; set; } = string.Empty;
        public string isbn10 { get; set; } = string.Empty;
        public List<OtherIsbn> other_isbns { get; set; }
    }

    public class DimensionsStructured
    {
        public Length length { get; set; }
        public Width width { get; set; }
        public Weight weight { get; set; }
        public Height height { get; set; }
    }

    public class Height
    {
        public string unit { get; set; }
        public double value { get; set; }
    }

    public class Length
    {
        public string unit { get; set; }
        public double value { get; set; }
    }

    public class OtherIsbn
    {
        public string isbn { get; set; }
        public string binding { get; set; }
    }

    public class Related
    {
        public string ePub { get; set; }
    }

    
    
    public class Weight
    {
        public string unit { get; set; }
        public double value { get; set; }
    }

    public class Width
    {
        public string unit { get; set; }
        public double value { get; set; }
    }
    
    public class IsbnRoot
    {
        public IsbnBook book { get; set; }
    }
    
    public class IsbnBookSearch
    {
        public int total { get; set; }
        public List<IsbnBook> books { get; set; }
    }

    public class IsbnAuthorBooks
    {
        public string author { get; set; }
        public List<IsbnBook> books { get; set; }
    }
}
