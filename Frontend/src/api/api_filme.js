import apiUtils from "./api_utils.js";

export default {
    getAll() {
        return apiUtils.get("filme");
    },

    getOptionsSelect() {
        return apiUtils.get("filme/options-select");
    }
};