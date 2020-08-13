<template>
  <vs-card>
    <div slot="header">
      <h3>Cadastro de cliente</h3>
    </div>

    <div>
      <vs-row>
        <vs-col vs-lg="6" vs-sm="12" class="px-2 pt-2">
          <vs-input
            label="Nome"
            v-model="nome"
            class="w-full"
            name="nome"
            v-validate="'required'"
            :danger="errors.has('nome')"
            :danger-text="errors.first('nome')"
          />
        </vs-col>

        <vs-col vs-lg="6" vs-sm="12" class="px-2 pt-2">
          <vs-input
            label="CPF"
            v-model="cpf"
            class="w-full"
            name="cpf"
            v-validate="'required'"
            :danger="this.erro_cpf || errors.has('cpf')"
            :danger-text="this.erro_cpf ? this.erro_cpf : errors.first('cpf')"
          />
        </vs-col>
      </vs-row>

      <vs-row>
        <vs-col vs-lg="6" vs-sm="12" class="px-2 pt-2">
          <vs-input
            label="Data de nascimento"
            v-model="dataNascimento"
            class="w-full"
            name="dataNascimento"
            v-validate="'required'"
            :danger="errors.has('dataNascimento')"
            :danger-text="errors.first('dataNascimento')"
          />
        </vs-col>
      </vs-row>

      <vs-row vs-type="flex" vs-justify="flex-end" class="mt-6 pr-2">
        <vs-button v-if="!id" @click="validar" class="font-semibold">Cadastrar</vs-button>

        <vs-button v-if="id" @click="validar" class="font-semibold" type="filled">Editar</vs-button>
      </vs-row>
    </div>
  </vs-card>
</template>

<script>
import api_cliente from "@/api/api_cliente";

import { Validator } from "vee-validate";
import utils from "@/assets/utils";
import {
  mascara_data,
  formatar_data,
  formatar_data_ao_contrario,
  mascaraCPF,
  removerMascaraCPF
} from "@/assets/utils/mask";

const dict = {
  custom: {
    nome: {
      required: "O nome é obrigatório!",
    },
    cpf: {
      required: "O CPF é obrigatório!",
    },
    dataNascimento: {
      required: "A data de nascimento é obrigatória!",
    },
  },
};

Validator.localize("pt", dict);

export default {
  data() {
    return {
      id: null,
      nome: "",
      cpf: "",
      dataNascimento: "",

      erro_cpf: null,
    };
  },

  created() {
    this.id = this.$route.query.id;

    if (this.id) {
      this.preencherCamposEdit();
    }
  },

  methods: {
    async validar() {
      this.erro_cpf = null;

      var valido = 0;

      var result = await utils.validar(this.$validator);

      if (!result) valido++;

      if (this.cpf && this.cpf.length != 14) {
        valido++;
        this.erro_cpf = "O CPF informado é inválido!";
      }

      if (valido == 0) {
        this.submit();
      } else {
        this.$vs.notify({
          color: "danger",
          title: "Erro",
          text: "Algum dos campos está com erro, verifique e tente novamente",
        });
      }
    },

    submit() {
      this.$vs.loading();

      const data = {
        nome: this.nome,
        cpf: removerMascaraCPF(this.cpf),
        dataNascimento: formatar_data(this.dataNascimento),
      };

      if (!this.id) {
        // SALVAR
        api_cliente
          .salvar(data)
          .then(() => {
            this.$vs.loading.close();

            this.$vs.notify({
              color: "success",
              title: "Login",
              text: "Cliente cadastrado com sucesso!",
            });

            this.$router.push({ name: "cliente-list" });
          })
          .catch((error) => {
            var exception = utils.getError(error);

            this.$vs.loading.close();
            this.$vs.notify({
              color: "danger",
              title: "Erro ao cadastrar cliente",
              text: exception,
            });
          });
      } else {
        // EDITAR
        data.id = parseInt(this.id);

        api_cliente
          .editar(this.id, data)
          .then(() => {
            this.$vs.loading.close();

            this.$vs.notify({
              color: "success",
              title: "Login",
              text: "Cliente editado com sucesso!",
            });

            this.$router.push({ name: "cliente-list" });
          })
          .catch((error) => {
            var exception = utils.getError(error);

            this.$vs.loading.close();
            this.$vs.notify({
              color: "danger",
              title: "Erro ao editar cliente",
              text: exception,
            });
          });
      }
    },

    preencherCamposEdit() {
      this.$vs.loading();

      api_cliente
        .getById(this.id)
        .then((response) => {
          this.$vs.loading.close();

          if (response.data) {
            this.nome = response.data.nome;
            this.cpf = response.data.cpf;

            this.dataNascimento = formatar_data_ao_contrario(response.data.dataNascimento);
          } else {
            this.$vs.notify({
              color: "danger",
              title: "Nenhum cliente encontrado",
            });
          }
        })
        .catch((error) => {
          var exception = utils.getError(error);

          this.$vs.loading.close();
          this.$vs.notify({
            color: "danger",
            title: "Erro ao carregar dados do cliente",
            text: exception,
          });
        });
    },

    formatarData(dataString) {
      if (!dataString) return null;

      var data = new Date(dataString);

      return data.toLocaleDateString("pt-BR");
    },

    mascaraCPF(cpf) {
      cpf = cpf.replace(/\D/g, "");
      cpf = cpf.replace(/(\d{3})(\d)/, "$1.$2");
      cpf = cpf.replace(/(\d{3})(\d)/, "$1.$2");
      cpf = cpf.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
      return cpf;
    },
  },

  watch: {
    dataNascimento() {
      var data = mascara_data(this.dataNascimento);
      if (this.dataNascimento != data) {
        this.dataNascimento = data;
      }
    },

    cpf(){
      var cpf = mascaraCPF(this.cpf);
      if (this.cpf != cpf) {
        this.cpf = cpf;
      }
    }
  }
};
</script>

<style scoped>
</style>