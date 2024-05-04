import axios from "axios";
import {handleError} from "../Helpers/ErrorHandler";
import {UserProfileToken} from "../Models/User";

const api = import.meta.env.VITE_API_BASE_URL;

export const loginAPI = async (username: string, password: string) => {
    try {
        return await axios.post<UserProfileToken>(api + "account/login", {
            username: username,
            password: password
        });
    } catch (error) {
        handleError(error);
    }
};

export const registerAPI = async (email: string, username: string, password: string) => {
    try {
        return await axios.post<UserProfileToken>(api + "account/register", {
            email: email,
            username: username,
            password: password
        });
    } catch (error) {
        handleError(error);
    }
};