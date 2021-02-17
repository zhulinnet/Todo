<template>
  <div class="register">
    <el-page-header @back="goBack" content="注册"> </el-page-header>
    <el-form
      ref="regForm"
      :model="regForm"
      :rules="regRules"
      auto-complete="on"
      label-position="left"
      size="big"
    >
      <el-form-item label="用户名" prop="account">
        <el-input
          ref="account"
          v-model="regForm.account"
          placeholder="请输入用户名"
          name="account"
          type="text"
          tabindex="1"
          auto-complete="on"
        >
        </el-input>
      </el-form-item>
      <el-form-item label="昵称" prop="name">
        <el-input
          ref="name"
          v-model="regForm.name"
          placeholder="请输入昵称"
          name="name"
          type="text"
          tabindex="1"
          auto-complete="on"
        >
        </el-input>
      </el-form-item>
      <el-form-item label="邮箱" prop="email">
        <el-input
          ref="email"
          v-model="regForm.email"
          placeholder="请输入邮箱"
          name="email"
          type="text"
          tabindex="1"
          auto-complete="on"
        >
        </el-input>
      </el-form-item>
      <el-form-item label="密码" prop="password">
        <el-input
          ref="password"
          v-model="regForm.password"
          placeholder="请输入密码"
          name="password"
          type="password"
          tabindex="1"
          auto-complete="on"
        >
        </el-input>
      </el-form-item>
      <el-form-item style="width:100%;">
        <el-button
          type="primary"
          style="width:100%;"
          size="big"
          @click.native.prevent="register"
          >注 册</el-button
        >
      </el-form-item>
    </el-form>
  </div>
</template>
<script>
import { register } from "@/api/user";
export default {
  data() {
    return {
      regForm: {
        account: "",
        name: "",
        email: "",
        password: "",
      },
      regRules: {
        account: [
          { required: true, trigger: "blur", message: "请输入用户名！" },
        ],
        name: [{ required: true, trigger: "blur", message: "请输入昵称！" }],
        email: [{ required: true, trigger: "blur", message: "请输入邮箱！" }],
        password: [
          { required: true, trigger: "blur", message: "请输入登录密码！" },
        ],
      },
    };
  },
  methods: {
    goBack() {
      this.$router.push("/login");
    },
    async register() {
      let isValid = false;
      this.$refs.regForm.validate(async (valid) => {
        isValid = valid;
      });
      if (isValid) {
        const res = await register(this.regForm);
        if (res.status == 200) {
          this.$message.success("注册成功！");
          this.$router.push("/login");
        }
      }
    },
  },
};
</script>
<style scoped>
.register {
  margin-top: 20px;
}
</style>
