<template>
    <div>
        <el-table
            :data="tableData"
            border
            style="width: 100%">
            <el-table-column
            prop="title"
            label="名称">
            </el-table-column>
             <el-table-column
            prop="date"
            label="日期"
            width="230">
            </el-table-column>
             <el-table-column
            prop="id"
            label="详情"
            width="180">
                <template slot-scope="scope">
                    <el-button @click="view(scope.row)" type="text" size="small">查看</el-button>
                </template>
            </el-table-column>
        </el-table>
    </div>
</template>


<script>
import {getSahreList } from '@/api/todo'
export default {
    data(){
        return{
            tableData:[]
        }
    },
    methods:{
        async getList(){
            var res = await getSahreList();
            this.tableData = res.data;
        },
        view(row){
            this.$router.push({path: '/item/' + row.id});
        }
    },
    created(){
        this.getList();
    }
}
</script>