new Vue({
    el: '#app-refund-table',
    data: function () {
        return {
            pageIndex: 1,
            pageSize: 13,
            totalCount: 50,
            loadingContent: '<span>加载中...</span>',
            errorContent: '<a href="javascript:void(0);">刷新重试</a>',
            isLoading: true, // 是否正在加载中
            tableData: [],
            columns: [
                { field: 'Id', title: '标识', width: 40, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class', isResize: true },
                { field: 'OrderNo', title: '订单号', width: 180, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class', isResize: true },
                { field: 'Mobile', title: '手机号', width: 120, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class', isResize: true },
                {
                    field: 'ProcessMode', title: '退货方式', width: 70, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class',
                    formatter: function (rowData, rowIndex, pagingIndex, field) {
                        var pmode = rowData.ProcessMode;
                        return pmode == "退货" ? '<span style="color:red;font-weight: bold;">' + pmode + '</span>' : '<span style="color:green;font-weight: bold;">' + pmode + '</span>';
                    }, isResize: true
                },
                { field: 'Message', title: '原因', width: 100, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class', isResize: true },
                { field: 'Account', title: '退款账户', width: 100, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class', isResize: true },

                {
                    field: 'CreateDate', title: '申请时间', width: 120, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class',
                    formatter: function (rowData, rowIndex, pagingIndex, field) {
                        if (rowData.CreateDate == '' || rowData.CreateDate == '0001-01-01T00:00:00')
                            return '';

                        return rowData.CreateDate.replace('T', ' ');
                    }, isResize: true
                },
                {
                    field: 'Status', title: '状态', width: 40, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class',
                    formatter: function (rowData, rowIndex, pagingIndex, field) {
                        if (rowData.Status == 0)
                            return '<span style="color:red;font-weight: bold;">待处理</span>';
                        else if (rowData.Status == 1)
                            return '<span style="color:green;font-weight: bold;">已处理</span>';
                    }, isResize: true
                },
                { field: 'Id', title: '操作', width: 200, titleAlign: 'center', columnAlign: 'center', componentName: 'table-operation', titleCellClassName: 'v-table-title-class', isResize: true }
            ]
        }
    },
    methods: {
        request() {
            this.isLoading = true;
            let mySelf = this;
            $.ajax({
                type: "post",
                url: "/Refund/Search",
                data: {
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
                <button type="button" class="btn btn-sm btn-primary" @click.stop.prevent="shangRow(rowData)">同意</button>
                <button type="button" class="btn btn-sm btn-primary" @click.stop.prevent="editRow(rowData)">不同意</button>
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
            if (!confirm("确定把该订单修改为同意吗？"))
                return;

            $.ajax({
                type: "post",
                url: "/Refund/ModifyRefund",
                data: {
                    "id": rowDatam.Id,
                    "type": 1
                },
                dataType: "json",
                success: function (data) {
                    alert(data.Message);
                    if (data.Code != 0)
                        return;

                    location.href = "/Sell/Refund/Index"
                },
                error: function (jqXHR) {
                    console.log('请求失败，请重试！');
                }
            });
        },
        editRow(rowDatam) {
            if (!confirm("确定把该订单修改为不同意吗？"))
                return;

            $.ajax({
                type: "post",
                url: "/Refund/ModifyRefund",
                data: {
                    "id": rowDatam.Id,
                    "type": 0
                },
                dataType: "json",
                success: function (data) {
                    alert(data.Message);
                    if (data.Code != 0)
                        return;

                    location.href = "/Sell/Refund/Index"
                },
                error: function (jqXHR) {
                    console.log('请求失败，请重试！');
                }
            });
        }
    }
})