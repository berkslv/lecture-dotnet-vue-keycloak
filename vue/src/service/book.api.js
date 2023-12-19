import axios from "./base.api";

export const getBooks = async () => {
  const response = await axios.get(`/book`);
  return response.data;
};

export const createBook = async (book) => {
  const response = await axios.post(`/book`, book);
  return response.data;
};

export const updateBook = async (id, book) => {
  const response = await axios.put(`/book/${id}`, book);
  return response.data;
};

export const deleteBook = async (id) => {
  const response = await axios.delete(`/book/${id}`);
  return response.data;
};
