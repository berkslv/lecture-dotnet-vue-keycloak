import axios from "./base.api";

export const getWeather = async () => {
    const response = await axios.get(`/WeatherForecast`);
    return response.data;
}