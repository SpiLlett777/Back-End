import axios from "axios";
import { handleError } from "../Helpers/ErrorHandler.tsx";
import { UserProfileToken } from "../Models/User.ts";

const api = "https://localhost:7174/api/";

export const loginApi = async (username: string, password: string) => {
  try {
    const data = await axios.post<UserProfileToken>(api + "account/login", {
      username: username,
      password: password,
    });
    return data;
  } catch (error) {
    handleError(error);
  }
};

export const registerApi = async (
  email: string,
  username: string,
  password: string,
) => {
  try {
    const data = await axios.post<UserProfileToken>(api + "account/register", {
      email: email,
      username: username,
      password: password,
    });
    return data;
  } catch (error) {
    handleError(error);
  }
};
