<template>
<div>
<el-tree
    :data="data2"
    show-checkbox
    node-key="id"
    ref="tree"
    :default-expand-all="true"
    :props="defaultProps">
</el-tree>
<div style="display: flex;
    justify-content: flex-end;
    padding-top: 10px;">
  <el-button :loading="loding" type="primary"  @click="submit()">提交</el-button>
</div>

</div>
    
</template>
<script>
import {FileApi} from '../api.js'
  export default {
    props:['formInfo'],
    data() {
      return {
        data2: [],
        form:this.formInfo.data,
        loding:false,
        defaultCheckKeys:[],
        defaultProps: {
          children: 'children',
          label: 'label'
        }
      }
    },
    watch:{
        formInfo: {
            handler(val, olaval) {
            this.form = val.data;
            this.loadSelectMenuId(this.form.id)
            },
            deep: true
        }
    },
    mounted(){
      this.loadMenuTree()
      this.loadSelectMenuId(this.form.id)
    },
    methods:{
      loadMenuTree(){
        FileApi.loadMenuTree().then(res=>{
          this.data2 = res.data
        })
      },
      loadSelectMenuId(val){
        FileApi.loadSelectMenuId(val).then(res=>{
          this.getIds(res.data)
        })
      },
      getIds(data){
        let checkKeys=[]
        data.map(item=>{
          checkKeys.push(item.id);
          if(item.children.length != 0){
            item.children.map(value=>{
              checkKeys.push(value.id)
            })
          }
        })
        this.defaultCheckKeys=checkKeys
        this.$refs.tree.setCheckedKeys(checkKeys)
      },
      submit(){
        const params={
          positionId:this.form.id,
          menusId:this.$refs.tree.getCheckedKeys()
        }
        this.loding = true
        FileApi.SaveSelectMenu(params).then(res=>{
          const type1 = res.data.isSuccess?'success':'error'
          this.$message({
            type:type1,
            message:res.data.message
          })
          this.close()
        })
      },
      close(){
        this.loding = false
        this.$emit('close',false)
      }
    }
  };
</script>

//  <style lang="scss">
// .el-tree-node__children{
//   display: flex;
// }
// </style>


