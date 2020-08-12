
import axios from "axios"
import config from "@/../config.js";
const urlApi = config.apiUrl;

axios.defaults.withCredentials = true;

export default {
    serializeData(data) {
        return JSON.stringify(data);
    },

    call() {
        var token = localStorage.getItem("login_token");

        if (token) {
            axios.defaults.headers.common['Authorization'] = 'Bearer ' + token;
        }

        return axios.create({})
    },

    get(path) {
        return this.call().get(`${urlApi}/${path}`);
    },

    post(path, data) {
        return this.call().post(`${urlApi}/${path}`, data);
    },

    put(path, data, id) {
        return this.call().put(`${urlApi}/${path}/${id}`, data);
    },

    delete(path) {
        return this.call().delete(`${urlApi}/${path}`);
    },
}