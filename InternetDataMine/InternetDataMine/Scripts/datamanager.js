var shine_datamanager = new DataManager();

jQuery.extend
({
    // paramName 参数名称
    // paramObjs 参数集合
    shine_datamanage: function (paramName, paramObjs) {
        switch (paramName) {
            case "DMParam":
                if (paramObjs != null) {
                    shine_datamanager.ParamAddRange(paramObjs);
                }
                break;
                
            case "DMStart":
                shine_datamanager.Start(paramObjs);
                break;

            default:
                break;
        }
        
    }
});

/// <reference path="../../Login.html" />
/// <reference path="../../Login.html" />
/// <reference path="../../Login.html" />
// 数据参数初始化方法
function DMParam(method, url, datas, callBack) {
    this.method = method;       // ajax, post, get;
    this.url = url;             // 需要提交的目标 URL
    this.datas = datas;         // 需要提交的参数集合
    this.callBack = callBack;   // 回调函数
}

// 数据管理者
function DataManager() {
    this.DMParams = [];       // 参数集合
    this.CompleteCalls = [];         // 结束执行
    this.dataProcRuning = false;    // 数据处理程序运行中
}

// 添加参数
DataManager.prototype.ParamAdd = function (obj, index) {
    if (index == null) {
        this.DMParams[this.DMParams.length] = obj;
    }
    else {
        if (index > this.DMParams.length) index = this.DMParams.length;
        for (var i = this.DMParams.length; i > index; i--) {
            this.DMParams[i] = this.DMParams[i - 1];
        }
        this.DMParams[index] = obj;
    }
};

// 添加参数集合
DataManager.prototype.ParamAddRange = function (objs) {
    if (objs == null) return;
    
    if (objs.length == null) {
        this.ParamAdd(objs);
        return;
    }

    for (var i = 0; i < objs.length; i++) {
        this.ParamAdd(objs[i]);
    }
};

// 开始数据操作
DataManager.prototype.Start = function (onLoadEnd) {
    if (onLoadEnd != null && typeof (onLoadEnd) == "string") {
        this.CompleteCalls[this.CompleteCalls.length] = onLoadEnd;
    }
    if (this.DMParams.length == 0) return;
    if (this.dataProcRuning) return;    // 已经在运行, 不重复执行
    this.dataProcRuning = true;         // 标记数据处理程序开始运行
    if (onLoadEnd != null && typeof (onLoadEnd) == "string") {
        this.DataProc();
    } else {
        this.DataProc(onLoadEnd);
    }
};

// 处理集合中的一个数据逻辑
DataManager.prototype.DataProc = function (onLoadEnd) {
    if (this.DMParams.length == 0) {
        this.dataProcRuning = false;     // 标记数据处理程序结束运行
        if (onLoadEnd != null) {
            if (typeof(onLoadEnd) == "string")
            {
                eval(onLoadEnd);
                return;
            }
            else {
                onLoadEnd();
            }
        }

        if (this.CompleteCalls != null && this.CompleteCalls.length > 0) {
            for (var i = 0; i < this.CompleteCalls.length; i++) {
                eval(this.CompleteCalls[i]);
                this.CompleteCallsRemoveFirst();
            }
        }
        return;
    }

    var dm = this;
    var dataProc = this.DMParams[0];
    if (dataProc.method == "post") {
        $.post(dataProc.url, dataProc.datas,
            function (data, status) {
                data = jQuery.parseJSON(data);
                if (data.datas == null) return;
                data = data.datas;
                
                if (data.LoginErr != null) {
                    switch (data.LoginErr.Code) {
                        case 1000:
                            dm.DMParams = [];   // 释放所有需要提取的数据
                            break;
                        case 1010:
                            dm.DMParams = [];   // 释放所有需要提取的数据
                            break;
                        case 1032:
                            dm.DMParams = [];   // 释放所有需要提取的数据
                            $.messager.alert("提示信息", "对不起, 你没有使用此功能的权限.");
                            dm.dataProcRuning = false;
                            return;
                    }

                    // 如果页面来源非法, 且身份认证未能通过, 转至登录页面
                    if (document.referrer == "") {
                        dm.dataProcRuning = false;
                        location.href = "../../Login.html";
                    }

                    // 弹出登录对话框
                    ShowLogin();
                    dm.dataProcRuning = false;
                    return;
                }

                dm.DMParamRemoveFirst();            // 移除第一个元素

                if (dataProc.callBack != null) {
                    dataProc.callBack(data, status);
                }
                
                dm.DataProc(onLoadEnd);              // 循环调用数据处理，直到处理完所有数据
            })//, "json")
            .error(function (event, request, settings) {
                //alert('[ ' + event.status + ' ] data load error.');
                dm.DMParamRemoveFirst();            // 移除第一个元素
                dm.DataProc(onLoadEnd);              // 循环调用数据处理，直到处理完所有数据
            }
        );
        }
};

    // 移除第一个对象
    DataManager.prototype.DMParamRemoveFirst = function () {
        if (this.DMParams.length > 0) {
            var tmpParam = this.DMParams[0];
            for (var i = 0; i < this.DMParams.length - 1; i++) {
                this.DMParams[i] = this.DMParams[i + 1];
            }
            this.DMParams[this.DMParams.length - 1] = tmpParam;
            this.DMParams.pop();
        }
    };

    // 移除第一个对象
    DataManager.prototype.CompleteCallsRemoveFirst = function () {
        if (this.CompleteCalls.length > 0) {
            var tmpParam = this.CompleteCalls[0];
            for (var i = 0; i < this.CompleteCalls.length - 1; i++) {
                this.CompleteCalls[i] = this.CompleteCalls[i + 1];
            }
            this.CompleteCalls[this.CompleteCalls.length - 1] = tmpParam;
            this.CompleteCalls.pop();
        }
    };

    // 数据至界面
    DataManager.prototype.DataToUI = function (dom, data, defVal) {
        if (dom == null) return;
        if (data == null) return;

        for (var i = 0; i < dom.length; i++) {
            if (defVal != null) $(dom[i]).text(defVal);

            for (var dataKey in data) {
                if ($(dom[i]).attr("name") == dataKey) {
                    if ($(dom[i]).is("input")) {
                        $(dom[i]).attr("value", data[dataKey]);
                    }
                    else {
                        $(dom[i]).text(data[dataKey]);
                    }
                    //dom[i].innerText = data[dataKey];
                }
            }
        }
    };

    // 数据至界面
    DataManager.prototype.DataToUI_Numberspinner = function (dom, data, defVal) {
        if (dom == null) return;
        if (data == null) return;

        for (var i = 0; i < dom.length; i++) {
            if (defVal != null) $(dom[i]).text(defVal);

            for (var dataKey in data) {
                if ($(dom[i]).attr("numberboxname") == dataKey) {
                    $(dom[i]).numberspinner('setValue', data[dataKey])
                }
            }
        }
    };


    // 数据至界面
    DataManager.prototype.DataTo_DataGrid = function (dom, data, cols, showRowNO, toolBar, fitColumns)
    {
        if (dom == null) return;
        if (data == null) return;

        if (showRowNO == null) showRowNO = false;
        if (fitColumns == null) fitColumns = true;

        dom.datagrid({
            columns: cols, data: data, fitColumns: fitColumns, nowrap: false, pagination: false, singleSelect: true, rownumbers: showRowNO, toolbar: toolBar, height: 250
        });
    };

    // 数据至界面
    DataManager.prototype.DataTo_TreeGrid = function (dom, data, cols, id, pid, isExpandAll, toolBar) {
        if (dom == null) return;
        if (data == null) return;

        dom.treegrid({
            columns: cols, data: data, animate: false, fitColumns: true, nowrap: false, pagination: false,
            selectOnCheck: false, selectOnCheck: true, idField: id, treeField: pid, toolbar: toolBar, height: 250
        });

        // 是否展开
        if (isExpandAll != null && isExpandAll) {
            dom.treegrid('expandAll');
        }
    };

    DataManager.prototype.Form = function (dmParam, divDom) {
        this.ParamAdd(dmParam); // 添加参数

        // 回调函数通用处理方法
        var callBack = function(data, status) {
            if (callBackParam != null) {
                callBackParam(data, status);
            }
        };

        // 替换掉原回调函数
        var callBackParam = dmParam.callBack;
        dmParam.callBack = callBack;

        divDom.window('open');

        this.Start();   // 执行数据访问
    };

    DataManager.prototype.Toolbar_LeftDay = function (callBack) {

        var date = new Date();
        return {
            id: 'btnLeftDay',
            text: '上一天 ',
            iconCls: 'icon-left',
            iconAlign: 'left',
            handler: callBack
        };
    };

    DataManager.prototype.Toolbar_RightDay = function (callBack) {

        return {
        id: 'btnRightDay',
        text: ' 下一天 ',
        iconCls: 'icon-right',
        iconAlign: 'right',
        handler: callBack
    };
};

    function ShowLogin() {
        $("#win").panel({
            href: "Login.html?login=unreload",
            onLoad: function () {
                LoadAccountPwd();
            }
        });
        $("#win").panel('open');
    }