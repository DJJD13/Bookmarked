import axios from "axios";
import {handleError} from "../Helpers/ErrorHandler";
import {CommentGet, CommentPost} from "../Models/Comment";

const api = "https://localhost:7170/api/comment/";

export const commentPostAPI = async (title: string, content: string, isbn: string) => {
    try {
        return await axios.post<CommentPost>(api + `${isbn}`, {
            title: title,
            content: content
        });
    } catch (error) {
        handleError(error);
    }
}

export const commentGetAPI = async (isbn: string) => {
    try {
        return await axios.get<CommentGet[]>(api + `?isbn=${isbn}`);
    } catch (error) {
        handleError(error);
    }
}

