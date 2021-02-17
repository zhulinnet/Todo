<template>
  <div>
    <el-row style="margin-bottom:10px;text-align:left">
      <el-button-group>
        <el-button type="primary" icon="el-icon-edit" @click="onAdd"
          >新增清单</el-button
        >
      </el-button-group>
    </el-row>
    <el-table :data="tableData" border style="width: 100%">
      <el-table-column prop="title" label="名称"> </el-table-column>
      <el-table-column prop="date" label="日期" width="230"> </el-table-column>
      <el-table-column label="操作" width="180">
        <template slot-scope="scope">
          <el-button @click="view(scope.row)" type="text" size="small"
            >查看</el-button
          >
          <el-button @click="share(scope.row)" type="text" size="small"
            >分享</el-button
          >
        </template>
      </el-table-column>
    </el-table>

    <el-dialog title="添加清单" :visible.sync="isAdd" width="35%">
      <el-form ref="addForm" :model="addForm" :rules="addRules">
        <el-form-item label="清单名称" prop="title">
          <el-input v-model="addForm.title" autocomplete="off"></el-input>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="isAdd = false">取 消</el-button>
        <el-button type="primary" @click="onAddSubmit">确 定</el-button>
      </div>
    </el-dialog>
    <el-dialog title="分享清单" :visible.sync="isShare" width="35%">
      <el-form ref="shareForm" :model="shareForm" :rules="shareRules">
        <el-form-item label="用户账号或邮箱" prop="account">
          <el-input
            v-model="shareForm.listid"
            autocomplete="off"
            v-show="false"
          ></el-input>
          <el-input v-model="shareForm.account" autocomplete="off"></el-input>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="isShare = false">取 消</el-button>
        <el-button type="primary" @click="onShareSubmit">确 定</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import { add, getList, addShare } from "@/api/todo";
export default {
  data() {
    return {
      tableData: [],
      isAdd: false,
      isShare: false,
      addForm: {
        title: "",
      },
      shareForm: {
        listid: "",
        account: "",
      },
      addRules: {
        title: [
          { required: true, trigger: "blur", message: "请输入清单名称！" },
        ],
      },
      shareRules: {
        account: [
          {
            required: true,
            trigger: "blur",
            message: "请输入用户账号或邮箱！",
          },
        ],
      },
    };
  },
  methods: {
    async getList() {
      var res = await getList();
      this.tableData = res.data;
    },
    onAdd() {
      this.isAdd = true;
    },
    view(row) {
      this.$router.push({ path: "/item/" + row.id });
    },
    share(row) {
      this.shareForm.listid = row.id;
      this.isShare = true;
    },
    async onAddSubmit() {
      let isValid = false;
      this.$refs.addForm.validate(async (valid) => {
        isValid = valid;
      });
      if (isValid) {
        const res = await add(this.addForm);
        if (res.status == 200) {
          this.isAdd = false;
          this.getList();
        }
      }
    },
    async onShareSubmit() {
      let isValid = false;
      this.$refs.shareForm.validate(async (valid) => {
        isValid = valid;
      });
      if (isValid) {
        const res = await addShare(this.shareForm);
        if (res.status == 200) {
          this.isShare = false;
          this.$message.success("分享成功！");
          this.getList();
        }
      }
    },
  },
  created() {
    this.getList();
  },
};
</script>
