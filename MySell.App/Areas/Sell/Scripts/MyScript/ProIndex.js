new Vue({
    el: '#app-product-table',
    data: function () {
        return {
            name: '',
            pageIndex: 1,
            pageSize: 13,
            totalCount: 50,
            loadingContent: '<span>加载中...</span>',
            errorContent: '<a href="javascript:void(0);">刷新重试</a>',
            isLoading: true, // 是否正在加载中
            tableData: [],
            columns: [
                { field: 'Id', title: '标识', width: 40, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class', isResize: true },
                { field: 'Title', title: '标题', width: 180, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class', isResize: true },
                { field: 'PicSrc', title: '主图片', width: 120, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class', isResize: true },
                {
                    field: 'Price', title: '原价', width: 70, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class',
                    formatter: function (rowData, rowIndex, pagingIndex, field) {
                        return '<span style="color:green;font-weight: bold;">' + rowData.Price + '</span>';
                    }, isResize: true
                },
                {
                    field: 'Discount', title: '折扣', width: 40, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class',
                    formatter: function (rowData, rowIndex, pagingIndex, field) {
                        return rowData.Discount + '折';
                    },
                    isResize: true
                },
                {
                    field: 'DPrice', title: '折扣价', width: 70, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class',
                    formatter: function (rowData, rowIndex, pagingIndex, field) {
                        return '<span style="color:red;font-weight: bold;">' + rowData.DPrice + '</span>';
                    },
                    isResize: true
                },
                { field: 'ProductSpec', title: '规格', width: 80, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class', isResize: true },
                { field: 'Sold', title: '已售数', width: 60, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class', isResize: true },
                { field: 'Sort', title: '排序', width: 40, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class', isResize: true },
                {
                    field: 'IsShow', title: '上架', width: 40, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class',
                    formatter: function (rowData, rowIndex, pagingIndex, field) {
                        return rowData.IsShow ? '<span style="color:red;font-weight: bold;">上架</span>' : '<span style="color:green;font-weight: bold;">下架</span>';
                    }, isResize: true
                },
                { field: 'Id', title: '操作', width: 230, titleAlign: 'center', columnAlign: 'center', componentName: 'table-operation', titleCellClassName: 'v-table-title-class', isResize: true }
            ]
        }
    },
    methods: {
        request() {
            this.isLoading = true;
            let mySelf = this;
            $.ajax({
                type: "post",
                url: "/Product/Search",
                data: {
                    "name": mySelf.name,
                    "pageIndex": mySelf.pageIndex,
                    "pageSize": mySelf.pageSize
                },
                dataType: "json",
                success: function (data) {
                    mySelf.isLoading = false;
                    mySelf.tableData = data.data;
                    mySelf.totalCount = data.recordsTotal;
                },
                error: function (jqXHR) {
                    console.log('');
                }
            });
        },
        pageChange(pageIndex) {
            this.pageIndex = pageIndex;
            this.request();
            console.log(pageIndex)
        },
        pageSizeChange(pageSize) {

            this.pageIndex = 1;
            this.pageSize = pageSize;
            this.request();
        }
    },
    created() {
        this.request();
    }
})

// 自定义列组件
Vue.component('table-operation', {
    template: `<span>
                <button type="button" class="btn btn-sm btn-primary" @click.stop.prevent="shangRow(rowData)">上/下架</button>
                <button type="button" class="btn btn-sm btn-primary" @click.stop.prevent="editRow(rowData)">编辑</button>
                <button type="button" class="btn btn-sm btn-primary" @click.stop.prevent="priceRow(rowData)">规格生效</button>
                <button type="button" class="btn btn-sm btn-primary" @click.stop.prevent="delRow(rowData)">删除</button>
                <button type="button" class="btn btn-sm btn-primary" @click.stop.prevent="showRow(rowData)">预览</button>
              </span>`,
    props: {
        rowData: {
            type: Object
        },
        field: {
            type: String
        },
        index: {
            type: Number
        }
    },
    methods: {
        shangRow(rowDatam) {
            $.ajax({
                type: "post",
                url: "/Product/UpdateIsShow",
                data: {
                    "id": rowDatam.Id
                },
                dataType: "json",
                success: function (data) {
                    alert(data.Message);
                    if (data.Code != 0)
                        return;
                    location.href = "/Sell/Product/Index"
                },
                error: function (jqXHR) {
                    console.log('请求失败，请重试！');
                }
            });

        },
        editRow(rowDatam) {
            location.href = "/Sell/Product/Edit?id=" + rowDatam.Id;
        },
        delRow(rowDatam) {
            if (!confirm("确认删除该产品吗？"))
                return;

            $.ajax({
                type: "post",
                url: "/Product/DeletePro",
                data: {
                    "id": rowDatam.Id
                },
                dataType: "json",
                success: function (data) {
                    alert(data.Message);
                    if (data.Code != 0)
                        return;
                    location.href = "/Sell/Product/Index"
                },
                error: function (jqXHR) {
                    console.log('请求失败，请重试！');
                }
            });
        },
        showRow(rowDatam) {
            location.href = "/Mall/Index/" + rowDatam.Id;
        },
        priceRow(rowDatam) {
            $.ajax({
                type: "post",
                url: "/Product/ResetPrice",
                data: {
                    "id": rowDatam.Id
                },
                dataType: "json",
                success: function (data) {
                    alert(data.Message);
                },
                error: function (jqXHR) {
                    console.log('请求失败，请重试！');
                }
            });
        }
    }
})