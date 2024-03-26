interface Book {
    title: string;
    title_long: string;
    isbn: string;
    isbn13: string;
    dewey_decimal: string;
    binding: string;
    publisher: string;
    language: string;
    date_published: string;
    edition: string;
    pages: number;
    dimensions: string;
    overview: string;
    image: string;
    msrp: number;
    excerpt: string;
    synopsis: string;
    authors: string[];
    subjects: string[];
    reviews: string[];
    related: {
        type: string;
    };
    other_isbns: {
        isbn: string;
        binding: string;
    }[];
}

interface BookTitleSearch {
    total: number;
    books: Book[];
}