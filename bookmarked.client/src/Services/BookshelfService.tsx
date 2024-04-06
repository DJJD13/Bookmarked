import axios from "axios";
import { handleError } from "../Helpers/ErrorHandler";
import { BookshelfGet, BookshelfPost } from "../Models/Bookshelf";

const api = "https://localhost:7170/api/bookshelf/";

export const bookshelfAddAPI = async (isbn: string) => {
    try {
        const data = await axios.post<BookshelfPost>(api + `?isbn=${isbn}`);
        return data;
    } catch (error) {
        handleError(error);
    }
};

export const bookshelfDeleteAPI = async (isbn: string) => {
    try {
        const data = await axios.delete<BookshelfPost>(api + `?isbn=${isbn}`);
        return data;
    } catch (error) {
        handleError(error);
    }
};

export const bookshelfGetAPI = async () => {
    try {
        const data = await axios.get<BookshelfGet[]>(api);
        return data;
    } catch (error) {
        handleError(error);
    }
};
