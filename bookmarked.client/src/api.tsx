import axios from "axios"
import {handleError} from "./Helpers/ErrorHandler.tsx";

const headers = {
    "Content-Type": 'application/json',
    "Authorization": import.meta.env.VITE_API_KEY
};

export const getBookByISBN = async (query: string) => {
    try {
        return await axios.get<ISBNBookResponse>(
            `https://api2.isbndb.com/book/${query}`, {headers}
        );
    } catch (error) {
        handleError(error);
    } 
}

export const searchByTitle = async (query: string) => {
    try {
        return await axios.get<BookTitleSearch>(
            `https://api2.isbndb.com/books/${query}?page=1&pageSize=20`, {headers}
        );
    } catch (error) {
       handleError(error); 
    }
}

export const getAuthorDetails = async (query: string) => {
    try {
        return await axios.get<AuthorBookDetails>(
            `https://api2.isbndb.com/author/${query}?page=1&pageSize=15`, {headers}
        );
    } catch (error) {
        handleError(error);
    }
}