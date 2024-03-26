import axios from "axios"

const headers = {
    "Content-Type": 'application/json',
    "Authorization": import.meta.env.VITE_API_KEY
};

export const searchByISBN = async (query: string) => {
    try {
        const data = await axios.get<Book>(
            `https://api2.isbndb.com/book/${query}`, { headers }
        );
        return data;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log("error message ", error.message);
            return error.message;
        } else {
            console.log("Unexpected error: ", error);
            return "An unexpected Error has occurred";
        }
    }
}

export const searchByTitle = async (query: string) => {
    try {
        const data = await axios.get<BookTitleSearch>(
            `https://api2.isbndb.com/books/${query}?page=1&pageSize=20`, { headers }
        );
        return data;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log("error message ", error.message);
            return error.message;
        } else {
            console.log("Unexpected error: ", error);
            return "An unexpected Error has occurred";
        }
    }
}