<template>
  <div class="login">
    <el-form
      ref="loginForm"
      :model="loginForm"
      :rules="loginRules"
      auto-complete="on"
      label-position="left"
      size="big"
      class="login-form"
    >
      <el-form-item style="text-align:center">
        <div style="font-size:30px;font-weight:bold"></div>
      </el-form-item>
      <el-form-item prop="account">
        <el-input
          ref="account"
          v-model="loginForm.account"
          placeholder="请输入用用户名"
          name="account"
          type="text"
          tabindex="1"
          auto-complete="on"
        >
          <i slot="prefix" class="el-icon-user-solid"></i>
        </el-input>
      </el-form-item>
      <el-form-item prop="password">
        <el-input
          ref="password"
          v-model="loginForm.password"
          placeholder="请输入登录密码"
          name="password"
          type="password"
          tabindex="2"
          auto-complete="on"
        >
          <i slot="prefix" class="el-icon-lock"></i>
        </el-input>
      </el-form-item>
      <el-form-item style="width:100%;">
        <el-button
          type="primary"
          style="width:100%;"
          size="big"
          @click.native.prevent="login"
          >登 录</el-button
        >
        <el-link href="#/register/">没有账号？注册一个新账号</el-link>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
import { login } from "@/api/user";
import tokenhelper from "@/util/tokenhelper";
export default {
  data() {
    return {
      loginForm: {
        account: "",
        password: "",
      },
      loginRules: {
        account: [
          { required: true, trigger: "blur", message: "请输入用户名！" },
        ],
        password: [
          { required: true, trigger: "blur", message: "请输入登录密码！" },
        ],
      },
    };
  },
  methods: {
    async login() {
      let isValid = false;
      this.$refs.loginForm.validate(async (valid) => {
        isValid = valid;
      });
      if (isValid) {
        const res = await login(this.loginForm);
        if (res.status == 200) {
          tokenhelper.setToken(res.data.token);
          localStorage.setItem("realName", res.data.name);
          this.$router.push("/index");
        }
      }
    },
  },
};
</script>
<style scoped>
.login {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100%;
}
.login-form {
  border-radius: 6px;
  background: #ffffff;
  width: 385px;
  padding: 25px 25px 5px 25px;
}
.login-form .el-input {
  height: 38px;
}
.login-form .input-icon {
  height: 39px;
  width: 14px;
  margin-left: 2px;
}
.login-tip {
  font-size: 13px;
  text-align: center;
  color: #bfbfbf;
}
.login-code {
  width: 33%;
  display: inline-block;
  height: 38px;
  float: right;
}
.login-code img {
  cursor: pointer;
  vertical-align: middle;
}
</style>
