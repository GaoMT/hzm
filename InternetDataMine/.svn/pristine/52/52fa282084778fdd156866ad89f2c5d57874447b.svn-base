/*

*/

///删除
function del(index) {
    $.ajax({
        cache: false,
        method: 'POST',
        datatype: 'json',
        url: '../CompanyInfoManager/TransCompanyData?type=DelJBXX&ID=' + $('#JBXX').datagrid('getRows')['id'],
        success: function (data) {
            if (data == "True") {

                AddProcessLogGlobal("删除企业基本信息！");
                $.messager.alert('提示', '删除成功！');
            }
            else {
                $.messager.alert('提示', '删除失败！');
            }
        },
        error: function () {
            $.messager.alert('提示', '错误！');
        }
    })
    $('#JBXX').datagrid('reload');
}
///拼接URL
function contenturl() {
    var URL = "";
    URL += 'colliery_no|' + $('#colliery_no').textbox('getText');
    URL += ',col_name|' + $('#col_name').textbox('getText');
    URL += ',areano|' + $('#areano').textbox('getText');
    URL += ',postalcode|' + $('#postalcode').textbox('getText');
    URL += ',latitude|' + $('#latitude').textbox('getText');
    URL += ',longitude|' + $('#longitude').textbox('getText');
    URL += ',dep_no|' + $('#dep_no').textbox('getText');
    URL += ',col_type|' + $('#col_type').combobox('getValue');
    URL += ',built_cycle|' + $('#built_cycle').textbox('getText');
    URL += ',invest_date|' + $('#invest_date').datebox('getValue');
    URL += ',length_service|' + $('#length_service').textbox('getText');
    URL += ',standard_grade|' + $('#standard_grade').combobox('getValue');
    URL += ',lawyer|' + $('#lawyer').textbox('getText');
    URL += ',lawyer_phone|' + $('#lawyer_phone').textbox('getText');
    URL += ',manager|' + $('#manager').textbox('getText');
    URL += ',manager_phone|' + $('#manager_phone').textbox('getText');
    URL += ',dds_phone1|' + $('#dds_phone1').textbox('getText');
    URL += ',dds_phone2|' + $('#dds_phone2').textbox('getText');
    URL += ',ajz_code|' + $('#ajz_code').textbox('getText');
    URL += ',ajzz|' + $('#ajzz').textbox('getText');
    URL += ',ajzz_phone|' + $('#ajzz_phone').textbox('getText');
    URL += ',col_status|' + $('#col_status').combobox('getValue');
    URL += ',mine_type|' + $('#mine_type').combobox('getValue');
    URL += ',geology_reserves|' + $('#geology_reserves').textbox('getText');
    URL += ',mine_reserves|' + $('#mine_reserves').textbox('getText');
    URL += ',design_capacity|' + $('#design_capacity').textbox('getText');
    URL += ',check_capacity|' + $('#check_capacity').textbox('getText');
    URL += ',mining_area|' + $('#mining_area').textbox('getText');
    URL += ',year_w|' + $('#year_w').textbox('getText');

    URL += ',six_system|' + $('#year_w').textbox('getText');
    URL += ',ws_system|' + $('#ws_system').textbox('getText');
    URL += ',wtsb|' + $('#wtsb').textbox('getText');
    URL += ',wsccl|' + $('#wsccl').textbox('getText');
    URL += ',wslyl|' + $('#wslyl').textbox('getText');
    URL += ',coal_type|' + $('#coal_type').combobox('getValue');
    URL += ',coal_seam|' + $('#coal_seam').textbox('getText');
    URL += ',jfckcqk|' + $('#jfckcqk').textbox('getText');
    URL += ',tunnel_mode|' + $('#tunnel_mode').combobox('getValue');
    URL += ',coal_mine|' + $('#coal_mine').combobox('getValue');
    URL += ',mode_of_ship|' + $('#mode_of_ship').combobox('getValue');
    URL += ',walk_system|' + $('#walk_system').textbox('getText');
    URL += ',power_type|' + $('#power_type').combobox('getValue');
    URL += ',gas_brust|' + $('#gas_brust').combobox('getValue');
    URL += ',gas_level|' + $('#gas_level').combobox('getValue');
    URL += ',coal_seam_space|' + $('#coal_seam_space').textbox('getText');
    URL += ',coal_seam_angle|' + $('#coal_seam_angle').textbox('getText');
    URL += ',self_iginte|' + $('#self_iginte').combobox('getValue');
    URL += ',explosion_type|' + $('#explosion_type').combobox('getValue');
    URL += ',rock_behavio|' + $('#rock_behavio').textbox('getText');
    URL += ',lw_shoring_type|' + $('#lw_shoring_type').combobox('getValue');
    URL += ',me_shoring_type|' + $('#me_shoring_type').combobox('getValue');
    URL += ',hydrology_survey|' + $('#hydrology_survey').combobox('getValue');
    URL += ',tffs|' + $('#tffs').textbox('getText');
    URL += ',jjgzm_cou|' + $('#jjgzm_cou').textbox('getText');
    URL += ',jjgzm_name|' + $('#jjgzm_name').textbox('getText');
    URL += ',cmgzm_cou|' + $('#cmgzm_cou').textbox('getText');
    URL += ',cmgzm_name|' + $('#cmgzm_name').textbox('getText');
    URL += ',certificate_no1|' + $('#certificate_no1').textbox('getText');
    URL += ',award_dep|' + $('#award_dep').textbox('getText');
    URL += ',award_date|' + $('#award_date').datebox('getValue');
    URL += ',dead_line6|' + $('#dead_line6').datebox('getValue');
    URL += ',certificate_no2|' + $('#certificate_no2').textbox('getText');
    URL += ',deadline1|' + $('#deadline1').datebox('getValue');
    URL += ',certificate_no3|' + $('#certificate_no3').textbox('getText');
    URL += ',deadline2|' + $('#deadline2').datebox('getValue');
    URL += ',certificate_no6|' + $('#certificate_no6').textbox('getText');
    URL += ',dead_line5|' + $('#dead_line5').datebox('getValue');
    URL += ',undermanager|' + $('#undermanager').textbox('getText');
    URL += ',certificate_no5|' + $('#certificate_no5').textbox('getText');
    URL += ',deadline4|' + $('#deadline4').datebox('getValue');
    URL += ',kcfa_dep|' + $('#kcfa_dep').textbox('getText');
    URL += ',kcfa_no|' + $('#kcfa_no').textbox('getText');
    URL += ',aqss_dep|' + $('#aqss_dep').textbox('getText');
    URL += ',aqss_no|' + $('#aqss_no').textbox('getText');
    URL += ',tzje|' + $('#tzje').textbox('getText');
    URL += ',jsgq|' + $('#jsgq').textbox('getText');
    URL += ',new_remark|' + $('#new_remark').textbox('getText');
    URL += ',sg_dep|' + $('#sg_dep').textbox('getText');
    URL += ',sg_zz|' + $('#sg_zz').textbox('getText');
    URL += ',jl_dep|' + $('#jl_dep').textbox('getText');
    URL += ',jl_zz|' + $('#jl_zz').textbox('getText');
    URL += ',jfdqk|' + $('#jfdqk').textbox('getText');
    URL += ',kqmdqk|' + $('#kqmdqk').textbox('getText');
    URL += ',accident_type|' + $('#accident_type').combobox('getValue');
    URL += ',have_casualty|' + $('#have_casualty').combobox('getValue');
    URL += ',casualty|' + $('#casualty').textbox('getText');
    return URL;
}

//添加按钮单击
function AddInfo() {
    Clearcache();
    $("#Adddlg").dialog("open");
}
//保存基本信息
function SaveInfo() {


  

    var myurl = '../CompanyInfoManager/TransCompanyData?type=';
    if ($('#SaveorAdd')[0].value == "1") {
        myurl += 'AddJBXX';
    }
    if ($('#SaveorAdd')[0].value == "0") {
        myurl += 'AlterJBXX&ID=' + $('#id').textbox('getText');
    }
    $("#ff").form('submit', {
        url: myurl + '&Info=' + encodeURI(contenturl()),
        onSubmit: function(param) {
            return $(this).form('enableValidation').form('validate')
        },
        success: function(data) {
            if (data == "True") {
                AddProcessLogGlobal("保存了企业基本信息！");

                $.messager.alert('提示', '保存成功！',"info");
                $('#Adddlg').dialog('close');
                $('#JBXX').datagrid('reload');
            } else {
                if ($('#SaveorAdd')[0].value == "1")
                {
                    $.messager.alert('提示', '数据保存失败，已经存在相同的企业编号了，无法再添加相同的企业编号！', "error");
                }
                else {
                    $.messager.alert('提示', '保存失败,！', "error");
                }
              
            }
        },
        error: function(data) {
            $.messager.alert('提示', '错误！');
        }
    });

}
//清空弹窗内信息
function Clearcache() {
    //$('#colliery_no').textbox("setText", "");
    $('#col_name').textbox("setText", "");
    $('#areano').textbox("setText", "");
    $('#postalcode').textbox("setText", "");
    $('#latitude').textbox("setText", "");
    $('#longitude').textbox("setText", "");
    $('#dep_no').textbox("setText", "");
    $('#col_type').combobox('setValue', '');
    $('#built_cycle').textbox("setText", "");
    $('#invest_date').datebox('setValue', '');
    $('#length_service').textbox("setText", "");
    $('#standard_grade').combobox('setValue', '');
    $('#lawyer').textbox("setText", "");
    $('#lawyer_phone').textbox("setText", "");
    $('#manager').textbox("setText", "");
    $('#manager_phone').textbox("setText", "");
    $('#dds_phone1').textbox("setText", "");
    $('#dds_phone2').textbox("setText", "");
    $('#ajz_code').textbox("setText", "");
    $('#ajzz').textbox("setText", "");
    $('#ajzz_phone').textbox("setText", "");
    $('#col_status').combobox('setValue', '');
    $('#mine_type').combobox('setValue', '');
    $('#geology_reserves').textbox("setText", "");
    $('#mine_reserves').textbox("setText", "");
    $('#design_capacity').textbox("setText", "");
    $('#check_capacity').textbox("setText", "");
    $('#mining_area').textbox("setText", "");
    $('#year_w').textbox("setText", "");

    $('#six_system').textbox("setText", "");
    $('#ws_system').textbox("setText", "");
    $('#wtsb').textbox("setText", "");
    $('#wsccl').textbox("setText", "");
    $('#wslyl').textbox("setText", "");
    $('#coal_type').combobox('setValue', '');
    $('#coal_seam').textbox("setText", "");
    $('#jfckcqk').textbox("setText", "");
    $('#tunnel_mode').combobox('setValue', '');
    $('#coal_mine').combobox('setValue', '');
    $('#mode_of_ship').combobox('setValue', '');
    $('#walk_system').textbox("setText", "");
    $('#power_type').combobox('setValue', '');
    $('#gas_brust').combobox('setValue', '');
    $('#gas_level').combobox('setValue', '');
    $('#coal_seam_space').textbox("setText", "");
    $('#coal_seam_angle').textbox("setText", "");
    $('#self_iginte').combobox('setValue', '');
    $('#explosion_type').combobox('setValue', '');
    $('#rock_behavio').textbox("setText", "");
    $('#lw_shoring_type').combobox('setValue', '');
    $('#me_shoring_type').combobox('setValue', '');
    $('#hydrology_survey').combobox('setValue', '');
    $('#tffs').textbox("setText", "");
    $('#jjgzm_cou').textbox("setText", "");
    $('#jjgzm_name').textbox("setText", "");
    $('#cmgzm_cou').textbox("setText", "");
    $('#cmgzm_name').textbox("setText", "");
    $('#certificate_no1').textbox("setText", "");
    $('#award_dep').textbox("setText", "");
    $('#award_date').datebox('setValue', '');
    $('#dead_line6').datebox('setValue', '');
    $('#certificate_no2').textbox("setText", "");
    $('#deadline1').datebox('setValue');
    $('#certificate_no3').textbox("setText", "");
    $('#deadline2').datebox('setValue');
    $('#certificate_no6').textbox("setText", "");
    $('#dead_line5').datebox('setValue');
    $('#undermanager').textbox("setText", "");
    $('#certificate_no5').textbox("setText", "");
    $('#deadline4').datebox('setValue');
    $('#kcfa_dep').textbox("setText", "");
    $('#kcfa_no').textbox("setText", "");
    $('#aqss_dep').textbox("setText", "");
    $('#aqss_no').textbox("setText", "");
    $('#tzje').textbox("setText", "");
    $('#jsgq').textbox("setText", "");
    $('#new_remark').textbox("setText", "");
    $('#sg_dep').textbox("setText", "");
    $('#sg_zz').textbox("setText", "");
    $('#jl_dep').textbox("setText", "");
    $('#jl_zz').textbox("setText", "");
    $('#jfdqk').textbox("setText", "");
    $('#kqmdqk').textbox("setText", "");
    $('#accident_type').combobox('setValue', '');
    $('#have_casualty').combobox('setValue', '');
    $('#casualty').textbox("setText", "");
}
///加载combobox
$('#col_type').ready(function () {
    $('#col_type').combobox({
        url: '../CompanyInfoManager/TransCompanyData?type=col_type',
        valueField: 'id',
        textField: 'text'
    })
    $('#standard_grade').combobox({
        url: '../CompanyInfoManager/TransCompanyData?type=standard_grade',
        valueField: 'id',
        textField: 'text'
    })
    $('#col_status').combobox({
        url: '../CompanyInfoManager/TransCompanyData?type=col_status',
        valueField: 'id',
        textField: 'text'
    })
    $('#mine_type').combobox({
        url: '../CompanyInfoManager/TransCompanyData?type=mine_type',
        valueField: 'id',
        textField: 'text'
    })
    $('#coal_type').combobox({
        url: '../CompanyInfoManager/TransCompanyData?type=coal_type',
        valueField: 'id',
        textField: 'text'
    })
    $('#tunnel_mode').combobox({
        url: '../CompanyInfoManager/TransCompanyData?type=tunnel_mode',
        valueField: 'id',
        textField: 'text'
    })
    $('#coal_mine').combobox({
        url: '../CompanyInfoManager/TransCompanyData?type=coal_mine',
        valueField: 'id',
        textField: 'text'
    })
    $('#mode_of_ship').combobox({
        url: '../CompanyInfoManager/TransCompanyData?type=mode_of_ship',
        valueField: 'id',
        textField: 'text'
    })
    $('#walk_system').combobox({
        url: '../CompanyInfoManager/TransCompanyData?type=Isorno',
        valueField: 'id',
        textField: 'text'
    })
    $('#power_type').combobox({
        url: '../CompanyInfoManager/TransCompanyData?type=power_type',
        valueField: 'id',
        textField: 'text'
    })
    $('#gas_brust').combobox({
        url: '../CompanyInfoManager/TransCompanyData?type=Isorno',
        valueField: 'id',
        textField: 'text'
    })
    $('#gas_level').combobox({
        url: '../CompanyInfoManager/TransCompanyData?type=gas_level',
        valueField: 'id',
        textField: 'text'
    })
    $('#self_iginte').combobox({
        url: '../CompanyInfoManager/TransCompanyData?type=self_iginte',
        valueField: 'id',
        textField: 'text'
    })
    $('#explosion_type').combobox({
        url: '../CompanyInfoManager/TransCompanyData?type=Isorno',
        valueField: 'id',
        textField: 'text'
    })
    $('#rock_behavio').combobox({
        url: '../CompanyInfoManager/TransCompanyData?type=rock_behavio',
        valueField: 'id',
        textField: 'text'
    })
    $('#lw_shoring_type').combobox({
        url: '../CompanyInfoManager/TransCompanyData?type=lw_shoring_type',
        valueField: 'id',
        textField: 'text'
    })
    $('#me_shoring_type').combobox({
        url: '../CompanyInfoManager/TransCompanyData?type=me_shoring_type',
        valueField: 'id',
        textField: 'text'
    })
    $('#hydrology_survey').combobox({
        url: '../CompanyInfoManager/TransCompanyData?type=Isorno',
        valueField: 'id',
        textField: 'text'
    })
    $('#accident_type').combobox({
        url: '../CompanyInfoManager/TransCompanyData?type=accident_type',
        valueField: 'id',
        textField: 'text'
    })
    $('#have_casualty').combobox({
        url: '../CompanyInfoManager/TransCompanyData?type=Isorno',
        valueField: 'id',
        textField: 'text'
    })
})


//查询URL中的参数
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}