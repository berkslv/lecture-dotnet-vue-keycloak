import axios from "axios";

const http = axios.create({
  baseURL: "http://localhost:5050/",
  headers: {
    "Content-Type": "application/json",
  },
});

http.interceptors.request.use(
  async (config) => {
    const token = localStorage.getItem("vue-token");
    config.headers = {
      Authorization: `Bearer ${token}`,
      Accept: "application/json",
    };
    return config;
  },
  (error) => {
    Promise.reject(error);
  }
);

export default http;
