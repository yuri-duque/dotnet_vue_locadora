import apiUtils from "./api_utils.js";

export default {
    getAll() {
        return apiUtils.get("cliente");
    },

    getOptionsSelect() {
        return apiUtils.get("cliente/options-select");
    }
};