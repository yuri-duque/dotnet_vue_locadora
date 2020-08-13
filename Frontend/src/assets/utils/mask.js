function mascara_numero(text) {
    if (text) {
        text += "";
        return text.replace(/[^0-9.]/g, "").replace(/(\..*)\./g, "$1");
    }

    return null;
}

function mascara_data(data) {
    if (data) {
        data += ""
        if (data.length > 10) {
            data = data.substring(0, data.length - 1);
            return data;
        }

        data = mascara_numero(data);

        data = data.replace(/\D/g, "");
        data = data.replace(/(\d{2})(\d)/, "$1/$2");
        data = data.replace(/(\d{2})(\d)/, "$1/$2");
        return data;
    }

    return null;
}

function formatar_data(text) {
    if (text) {
        text += "";
        var data = text.split("/", 3);
        var dia = data[0];
        var mes = data[1];
        var ano = data[2];

        return ano + "-" + mes + "-" + dia;
    }

    return null;
}

function formatar_data_ao_contrario(data) {
    if (data) {
        data += ""
        var text = data + "";
        data = text.split("-", 3);
        var dia = data[2].split("T")[0];
        var mes = data[1];
        var ano = data[0];

        return dia + "/" + mes + "/" + ano;
    }

    return null;
}

function mascaraCPF(cpf) {
    cpf = mascara_numero(cpf);

    cpf = cpf.replace(/\D/g, "");
    cpf = cpf.replace(/(\d{3})(\d)/, "$1.$2");
    cpf = cpf.replace(/(\d{3})(\d)/, "$1.$2");
    cpf = cpf.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
    return cpf;
}

function removerMascaraCPF(cpf){
    cpf = cpf.replace(".", "");
    cpf = cpf.replace(".", "");
    cpf = cpf.replace("-", "");

    return cpf;
}

let timer = null;

function debounce(func, wait) {
    return function () {
        clearTimeout(timer);
        timer = setTimeout(func, wait);
    }
}

export { mascara_numero, mascara_data, formatar_data, formatar_data_ao_contrario, mascaraCPF, removerMascaraCPF, debounce }


