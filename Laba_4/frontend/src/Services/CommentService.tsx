import axios from "axios";
import { CommentGet, CommentPost } from "../Models/Comment.ts";
import { handleError } from "../Helpers/ErrorHandler.tsx";

const api = "https://localhost:7174/api/comment/";

export const commentPostAPI = async (
  title: string,
  content: string,
  symbol: string,
) => {
  try {
    const data = await axios.post<CommentPost>(api + `${symbol}`, {
      title: title,
      content: content,
    });
    return data;
  } catch (error) {
    handleError(error);
  }
};

export const commentGetAPI = async (symbol: string) => {
  try {
    const data = await axios.get<CommentGet[]>(api + `?symbol=${symbol}`);
    return data;
  } catch (error) {
    handleError(error);
  }
};
