<template>
  <div style="width:1140px; margin:0 auto">
    <el-container>
      <el-header style="background-color:#545c64" height="auto">
        <el-row>
          <el-col :span="10" class="left-menu">
            <el-menu
              router
              mode="horizontal"
              default-active="index"
              background-color="#545c64"
              text-color="#fff"
              active-text-color="#ffd04b"
            >
              <el-menu-item index="/index">我的清单</el-menu-item>
              <el-menu-item index="/share">分享给我</el-menu-item>
            </el-menu>
          </el-col>
          <el-col :span="4" class="right-menu">
            <el-dropdown trigger="click">
              <div class="right-menu-item">
                <span>{{ name }}</span>
              </div>
              <template #dropdown>
                <el-dropdown-menu
                  :visible-arrow="false"
                  style="margin-top: 2px;width:160px;"
                >
                  <el-dropdown-item
                    icon="el-icon-switch-button"
                    @click.native="logout"
                    >退出登录</el-dropdown-item
                  >
                </el-dropdown-menu>
              </template>
            </el-dropdown>
          </el-col>
        </el-row>
      </el-header>
      <el-main>
        <el-scrollbar>
          <router-view />
        </el-scrollbar>
      </el-main>
      <el-footer class="footer" style="padding-top:20px">
        <div class="pull-right hidden-xs"><b>Version</b> 1.0.0</div>
        <strong>Copyright &copy; 2021 <a href="">个人待办系统</a>.</strong> All
        rights reserved.
      </el-footer>
    </el-container>
  </div>
</template>

<script>
import tokenhelper from "@/util/tokenhelper";
export default {
  data() {
    return {
      name: "",
    };
  },
  methods: {
    logout() {
      tokenhelper.deleteToken();
      this.$router.push("/login");
    },
  },
  mounted() {
    this.name = localStorage.getItem("realName");
  },
};
</script>

<style>
.el-container .el-header {
  padding: 0px;
}

.el-container .el-header {
  padding: 0px;
}
.el-container .el-header .right-menu {
  text-align: right;
  padding-right: 5px;
  float: right;
  width: auto;
  overflow: hidden;
  height: 100%;
  line-height: 100%;
}
.el-container .el-header .right-menu .right-menu-item {
  padding: 0px 12px;
  cursor: pointer;
  transition: all 0.3s;
  line-height: 50px;
  color: #ffffff;
}

.footer {
  background: #545c64;
  padding: 15px;
  color: #ffffff;
  border-top: 1px solid #d2d6de;
  transition: transform 0.3s ease-in-out, margin 0.3s ease-in-out;
  z-index: 820;
}

.pull-right {
  float: right !important;
}
</style>
