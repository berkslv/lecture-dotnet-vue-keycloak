import axios from "axios";
import keycloak from "../main";

const http = axios.create({
  baseURL: "http://localhost:5050/api",
  headers: {
    "Content-Type": "application/json",
  },
});

http.interceptors.request.use(
  async (config) => {
    config.headers = {
      Authorization: `Bearer ${keycloak.token}`,
      Accept: "application/json",
    };
    return config;
  },
  (error) => {
    Promise.reject(error);
  }
);

export default http;
