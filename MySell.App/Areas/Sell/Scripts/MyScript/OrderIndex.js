new Vue({
    el: '#app-order-table',
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
                { field: 'Id', title: '标识', width: 20, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class', isResize: true },
                { field: 'OrderNo', title: '订单号', width: 100, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class', isResize: true },
                { field: 'ProName', title: '产品', width: 200, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class', isResize: true },
                { field: 'Qty', title: '数量', width: 30, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class', isResize: true },
                { field: 'Amount', title: '金额', width: 80, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class', isResize: true },
                { field: 'UserName', title: '收货人', width: 30, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class', isResize: true },
                { field: 'Mobile', title: '手机号', width: 70, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class', isResize: true },
                { field: 'Address', title: '收货地址', width: 280, titleAlign: 'center', columnAlign: 'left', titleCellClassName: 'v-table-title-class', isResize: true },
                { field: 'LeavMessage', title: '留言', width: 100, titleAlign: 'center', columnAlign: 'left', titleCellClassName: 'v-table-title-class', isResize: true },
                {
                    field: 'CreateDate', title: '下单时间', width: 120, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class',
                    formatter: function (rowData, rowIndex, pagingIndex, field) {
                        if (rowData.CreateDate == '' || rowData.CreateDate == '0001-01-01T00:00:00')
                            return '';

                        return rowData.CreateDate.replace('T', ' ');
                    }, isResize: true
                },
                {
                    field: 'Status', title: '状态', width: 30, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class',
                    formatter: function (rowData, rowIndex, pagingIndex, field) {
                        if (rowData.Status == 0)
                            return '<span style="color:red;font-weight: bold;">待处理</span>';
                        else if (rowData.Status == 1)
                            return '<span style="color:green;font-weight: bold;">已发货</span>';
                        else if (rowData.Status == 2)
                            return '<span style="color:gray;font-weight: bold;">已退货</span>';
                        else if (rowData.Status == 3)
                            return '<span style="color:yellow;font-weight: bold;">已换货</span>';
                        else if (rowData.Status == 4)
                            return '<span style="color:red;font-weight: bold;">换货申请</span>';
                        else if (rowData.Status == 5)
                            return '<span style="color:red;font-weight: bold;">退货申请</span>';
                    }, isResize: true
                },
                {
                    field: 'DeliverDate', title: '操作时间', width: 120, titleAlign: 'center', columnAlign: 'center', titleCellClassName: 'v-table-title-class',
                    formatter: function (rowData, rowIndex, pagingIndex, field) {
                        if (rowData.DeliverDate == '' || rowData.DeliverDate == '0001-01-01T00:00:00')
                            return '';

                        return rowData.DeliverDate.replace('T', ' ');
                    }, isResize: true
                },
                { field: 'Id', title: '操作', width: 100, titleAlign: 'center', columnAlign: 'center', componentName: 'table-operation', titleCellClassName: 'v-table-title-class', isResize: true }
            ]
        }
    },
    methods: {
        request() {
            this.isLoading = true;
            let mySelf = this;
            $.ajax({
                type: "post",
                url: "/Orders/Search",
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
                    console.log('请求服务器失败，请联系系统管理员');
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
                <button type="button" class="btn btn-sm btn-primary" @click.stop.prevent="faRow(rowData)">发货</button>
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
        faRow(rowDatam) {
            location.href = "/Sell/Orders/Add?orderId=" + rowDatam.Id;
        }
    }
})