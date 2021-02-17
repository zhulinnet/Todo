<template>
  <div>
    <el-row style="margin-bottom:10px;text-align:left">
      <el-button-group>
        <el-button type="primary" icon="el-icon-edit" @click="onAdd"
          >新增待办</el-button
        >
      </el-button-group>
    </el-row>
    <el-table :data="tableData" border style="width: 100%">
      <el-table-column prop="title" label="名称"> </el-table-column>
      <el-table-column prop="createTime" label="日期" width="230">
      </el-table-column>
      <el-table-column label="操作" width="180">
        <template slot-scope="scope">
          <el-button
            v-if="!scope.row.isDone"
            size="mini"
            @click="onComplete(scope.row)"
            >完成</el-button
          >
          <el-button v-else size="mini">已完成</el-button>
        </template>
      </el-table-column>
    </el-table>

    <el-dialog title="添加待办" :visible.sync="isAdd" width="35%">
      <el-form ref="addForm" :model="addForm" :rules="addRules">
        <el-form-item label="待办名称" prop="title">
          <el-input v-model="addForm.title" autocomplete="off"></el-input>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="isAdd = false">取 消</el-button>
        <el-button type="primary" @click="onAddSubmit">确 定</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import { add, getList, complete } from "@/api/item";
export default {
  data() {
    return {
      tableData: [],
      isAdd: false,
      addForm: {
        title: "",
      },
      addRules: {
        title: [
          { required: true, trigger: "blur", message: "请输入待办名称！" },
        ],
      },
    };
  },
  methods: {
    async getList() {
      var res = await getList(this.$route.params.id);
      this.tableData = res.data;
    },
    onAdd() {
      this.isAdd = true;
    },
    async onAddSubmit() {
      let isValid = false;
      this.$refs.addForm.validate(async (valid) => {
        isValid = valid;
      });
      if (isValid) {
        const res = await add(this.$route.params.id, this.addForm);
        if (res.status == 200) {
          this.isAdd = false;
          this.getList();
        }
      }
    },
    async onComplete(row) {
      const res = await complete(this.$route.params.id, row.id);
      if (res.status == 200) {
        this.getList();
      }
    },
  },
  created() {
    this.getList();
  },
};
</script>
