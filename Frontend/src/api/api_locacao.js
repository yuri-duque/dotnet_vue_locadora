import apiUtils from "./api_utils.js";

export default {
    getAll() {
        return apiUtils.get("locacao");
    },
};