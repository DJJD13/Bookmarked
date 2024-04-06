namespace Bookmarked.Server.Dtos.Book
{
    public class IsbnBook
    {
        public string publisher { get; set; }
        public string synopsis { get; set; }
        public string language { get; set; }
        public string image { get; set; }
        public string title_long { get; set; }
        public string edition { get; set; }
        public string dimensions { get; set; }
        public DimensionsStructured dimensions_structured { get; set; }
        public int pages { get; set; }
        public string date_published { get; set; }
        public List<string> authors { get; set; }
        public string title { get; set; }
        public string isbn13 { get; set; }
        public double msrp { get; set; }
        public string binding { get; set; }
        public Related related { get; set; }
        public string isbn { get; set; }
        public string isbn10 { get; set; }
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

    public class IsbnRoot
    {
        public IsbnBook book { get; set; }
    }

    public class Weight
    {
        public string unit { get; set; }
        public double value { get; set; }
    }

    public class Width
    {
        public string unit { get; set; }
        public int value { get; set; }
    }
}
