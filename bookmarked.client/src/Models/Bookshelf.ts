export type BookshelfGet = {
    id: number;
    isbn: string;
    title: string;
    author: string;
    price: number;
    pages: number;
    synposis: string;
    msrp: number;
    datePublished: string;
    coverImage: string;
    comments: any;
}

export type BookshelfPost = {
    isbn: string;
}