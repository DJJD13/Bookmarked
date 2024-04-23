import axios from "axios";
import {handleError} from "../Helpers/ErrorHandler";
import {BookshelfGet, BookshelfPost, BookshelfPut} from "../Models/Bookshelf";

const api = "https://localhost:7170/api/bookshelf/";

export const bookshelfAddAPI = async (isbn: string) => {
    try {
        return await axios.post<BookshelfPost>(api + `?isbn=${isbn}`);
    } catch (error) {
        handleError(error);
    }
};

export const bookshelfDeleteAPI = async (isbn: string) => {
    try {
        return await axios.delete<BookshelfPost>(api + `?isbn=${isbn}`);
    } catch (error) {
        handleError(error);
    }
};

export const bookshelfGetAPI = async () => {
    try {
        return await axios.get<BookshelfGet[]>(api);
    } catch (error) {
        handleError(error);
    }
};

export const bookshelfUpdateStatusAPI = async (isbn: string, status: number, pagesRead: number) => {
    try {
        return await axios.put<BookshelfPut>(api + `?isbn=${isbn}&status=${status}&pagesRead=${pagesRead}`);
    } catch (error) {
        handleError(error);
    }
}