interface Book {
    publisher: string;
    language: string;
    image: string;
    title_long: string;
    edition: string;
    dimensions: string;
    dimensions_structured: {
        length: {
            unit: string;
            value: number;
        };
        width: {
            unit: string;
            value: number;
        };
        weight: {
            unit: string;
            value: number;
        };
        height: {
            unit: string;
            value: number;
        };
    };
    pages: number;
    date_published: string;
    authors: string[]; 
    title: string;
    isbn13: string;
    msrp: number;
    binding: string;
    isbn: string;
    isbn10: string;
}

interface ISBNBookResponse {
    book: Book;
}

interface BookTitleSearch {
    total: number;
    books: Book[];
}