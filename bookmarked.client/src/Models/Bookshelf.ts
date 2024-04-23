export type BookshelfGet = {
    id: number;
    isbn: string;
    title: string;
    coverImage: string;
    readingStatus: number;
    pagesRead: number;
    totalPages: number;
}

export type BookshelfPost = {
    isbn: string;
}

export type BookshelfPut = {
    isbn: string;
    readingStatus: number;
}