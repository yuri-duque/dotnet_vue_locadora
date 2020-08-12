<template>
  <div
    class="h-screen flex w-full bg-img vx-row no-gutter items-center justify-center"
    id="page-login"
  >
    <div class="vx-col sm:m-0 m-4">
      <vx-card>
        <div slot="no-body" class="full-page-bg-color">
          <div class="vx-row no-gutter justify-center items-center">
            <div class="vx-col sm:w-full md:w-full d-theme-dark-bg">
              <div class="p-8">
                <div class="vx-card__title flex flex-col items-center mb-8">
                  <h4 class="mb-4">Cadastrar</h4>
                  <p>Se cadastre para poder logar no sistema.</p>
                </div>
                <vs-input
                  type="mail"
                  icon="icon icon-mail"
                  icon-pack="feather"
                  label-placeholder="E-mail"
                  v-model="mail"
                  class="w-full no-icon-border"
                  name="mail"
                  v-validate="'required'"
                  :danger="errors.has('mail')"
                  :danger-text="errors.first('mail')"
                />

                <vs-input
                  icon="icon icon-user"
                  icon-pack="feather"
                  label-placeholder="Username"
                  v-model="username"
                  class="w-full mt-6 no-icon-border"
                  name="username"
                  v-validate="'required'"
                  :danger="errors.has('username')"
                  :danger-text="errors.first('username')"
                />

                <vs-input
                  type="password"
                  icon="icon icon-lock"
                  icon-pack="feather"
                  label-placeholder="Senha"
                  v-model="password"
                  class="w-full mt-6 no-icon-border"
                  name="password"
                  v-validate="'required'"
                  :danger="errors.has('password') || this.error_confirmPassword"
                  :danger-text="this.error_confirmPassword ? this.error_confirmPassword : errors.first('password')"
                />

                <vs-input
                  type="password"
                  icon="icon icon-lock"
                  icon-pack="feather"
                  label-placeholder="Confirmar senha"
                  v-model="confirmPassword"
                  class="w-full mt-6 no-icon-border"
                  name="confirmPassword"
                  v-validate="'required'"
                  :danger="errors.has('confirmPassword') || this.error_confirmPassword"
                  :danger-text="this.error_confirmPassword ? this.error_confirmPassword : errors.first('confirmPassword')"
                />

                <vs-row class="mt-8">
                  <vs-col vs-lg="6" class="p-0">
                    <vs-button :to="{ name: 'login'}" type="border">Voltar</vs-button>
                  </vs-col>
                  <vs-col vs-lg="6" class="p-0">
                    <vs-button class="float-right" @click.prevent="submitForm">Cadastrar</vs-button>
                  </vs-col>
                </vs-row>
              </div>
            </div>
          </div>
        </div>
      </vx-card>
    </div>
  </div>
</template>

<script>
import api_user from "@/api/api_user";

import { Validator } from "vee-validate";
import utils from "@/assets/utils";

const dict = {
  custom: {
    mail: {
      required: "O e-mail é obrigatório!",
    },

    username: {
      required: "O username é obrigatório!",
    },
    password: {
      required: "A senha é obrigatória!",
    },
    confirmPassword: {
      required: "A comfirmação de senha é obrigatória!",
    },
  },
};

Validator.localize("pt", dict);

export default {
  data() {
    return {
      mail: "",
      username: "",
      password: "",
      confirmPassword: "",

      error_confirmPassword: null,
    };
  },

  methods: {
    async submitForm() {
      var valido = 0;

      var result = await utils.validar(this.$validator);
      if (!result) valido++;

      if (this.password !== this.confirmPassword) {
        valido++;
        this.error_confirmPassword = "As senhas não conhecidem";
      }

      if (valido == 0) {
        this.cadastrar();
      } else {
        this.$vs.notify({
          color: "danger",
          title: "Erro",
          text: "Algum dos campos está com erro, verifique e tente novamente",
        });
      }
    },

    cadastrar() {
      this.$vs.loading();

      const user = {
        username: this.username,
        password: this.password,
        confirmPassword: this.confirmPassword,
        email: this.mail,
      };

      api_user
        .save(user)
        .then(() => {
          this.$vs.loading.close();
          this.$vs.notify({
            color: "success",
            title: "Cadastro de usuário",
            text: "Usuário cadastrado com sucesso!",
          });

          this.$router.push({ name: "login" });
        })
        .catch((error) => {
          var exception = utils.getError(error);

          this.$vs.loading.close();
          this.$vs.notify({
            color: "danger",
            title: "Erro ao cadastrar usuário",
            text: exception,
          });
        });
    },
  },
};
</script>
