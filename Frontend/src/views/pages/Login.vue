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
                <div class="vx-card__title mb-8">
                  <h4 class="mb-4">Login</h4>
                  <p>Bora começar a gerenciar seu dinheiro?!</p>
                </div>
                <vs-input
                  icon="icon icon-user"
                  icon-pack="feather"
                  label-placeholder="Username"
                  v-model="username"
                  class="w-full no-icon-border"
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
                  :danger="errors.has('password')"
                  :danger-text="errors.first('password')"
                />

                <div class="flex flex-wrap justify-between my-5">
                  <vs-checkbox v-model="lembre_me" class="mb-3">Lembre-me</vs-checkbox>
                  <router-link to="#">Esqueceu sua senha?</router-link>
                </div>
                <vs-button
                  :to="{ name: 'cadastro-usuario'}"
                  type="border"
                  @click.prevent="cadastrar"
                >Cadastrar</vs-button>
                <vs-button class="float-right" @click.prevent="submitForm">Login</vs-button>
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
    username: {
      required: "O username é obrigatório!",
    },
    password: {
      required: "A senha é obrigatória!",
    },
  },
};

Validator.localize("pt", dict);

export default {
  data() {
    return {
      username: "",
      password: "",
      lembre_me: false,
    };
  },

  mounted() {
    if (this.$localStorage.get("login_is_remember_me")) {
      this.username = this.$localStorage.get("login_userName");
      this.password = this.$localStorage.get("login_password");
      this.lembre_me = this.$localStorage.get("login_is_remember_me");
    }
  },

  methods: {
    async submitForm() {
      var valido = 0;

      var result = await utils.validar(this.$validator);

      if (!result) valido++;

      if (valido == 0) {
        this.login();
      } else {
        this.$vs.notify({
          color: "danger",
          title: "Erro",
          text: "Algum dos campos está com erro, verifique e tente novamente",
        });
      }
    },

    async login() {
      this.remember_me();

      this.$vs.loading();

      const data = { username: this.username, password: this.password };

      api_user
        .login(data)
        .then((response) => {
          this.$vs.loading.close();
          console.log(response);

          this.$vs.notify({
            color: "success",
            title: "Login",
            text: "Login efetuado com sucesso!",
          });

          this.$router.push({ name: "home" });
        })
        .catch((error) => {
          var exception = utils.getError(error);

          this.$vs.loading.close();
          this.$vs.notify({
            color: "danger",
            title: "Erro ao logar",
            text: exception,
          });
        });
    },

    remember_me() {
      if (this.lembre_me) {
        this.$localStorage.set("login_is_remember_me", this.lembre_me);
        this.$localStorage.set("login_userName", this.username);
        this.$localStorage.set("login_password", this.password);
      } else {
        this.lembre_me = this.$localStorage.remove("login_is_remember_me");
        this.$localStorage.remove("login_userName");
        this.$localStorage.remove("login_password");
      }
    },
  },
};
</script>

<style lang="scss">
</style>
