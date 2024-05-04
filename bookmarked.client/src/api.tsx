import axios from "axios"
import {handleError} from "./Helpers/ErrorHandler.tsx";

const api =  import.meta.env.VITE_API_BASE_URL + "book/";

export const getBookByISBN = async (query: string) => {
    try {
        return await axios.get<Book>(
            api + `bookbyisbn?isbn=${query}`
        );
    } catch (error) {
        handleError(error);
    } 
}

export const searchByTitle = async (query: string) => {
    try {
        return await axios.get<BookTitleSearch>(
            api + `booksbytitle?title=${query}`
        );
    } catch (error) {
       handleError(error); 
    }
}

export const getAuthorDetails = async (query: string) => {
    try {
        return await axios.get<AuthorBookDetails>(
            api + `booksbyauthor?name=${query}`
        );
    } catch (error) {
        handleError(error);
    }
}