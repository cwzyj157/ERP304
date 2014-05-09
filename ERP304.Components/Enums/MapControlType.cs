using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ERP304.Components.Utility;

namespace ERP304.Components.Enums {
    public enum MapControlTypeAttachData {
        Name
    }

    public enum MapControlType {
        [AttachData(MapControlTypeAttachData.Name, "下拉控件基础组件")]
        AppDropDownBase,
        [AttachData(MapControlTypeAttachData.Name, "日期控件")]
        AppDateTime,
        [AttachData(MapControlTypeAttachData.Name, "日期控件II")]
        AppDateTimeII,
        [AttachData(MapControlTypeAttachData.Name, "下拉控件")]
        AppDropDownList,
        [AttachData(MapControlTypeAttachData.Name, "下拉树控件")]
        AppDropDownTree,
        [AttachData(MapControlTypeAttachData.Name, "下拉树控件II")]
        AppDropDownTreeII,
        [AttachData(MapControlTypeAttachData.Name, "动态列网格树控件")]
        AppDynamicGridTree,
        [AttachData(MapControlTypeAttachData.Name, "查询控件")]
        AppFind,
        [AttachData(MapControlTypeAttachData.Name, "二级界面表单控件")]
        AppForm,
        [AttachData(MapControlTypeAttachData.Name, "列表控件")]
        AppGrid,
        [AttachData(MapControlTypeAttachData.Name, "网格树控件")]
        AppGridTree,
        [AttachData(MapControlTypeAttachData.Name, "可编辑网格树控件")]
        AppGridTreeEdit,
        [AttachData(MapControlTypeAttachData.Name, "日志记录控件")]
        AppLog,
        [AttachData(MapControlTypeAttachData.Name, "导航标签控件")]
        AppNavBar,
        [AttachData(MapControlTypeAttachData.Name, "选择树控件，只允许单选")]
        AppTreeViewI,
        [AttachData(MapControlTypeAttachData.Name, "选择树控件，允许多选")]
        AppTreeViewII,
        [AttachData(MapControlTypeAttachData.Name, "上传文件控件")]
        AppUpFile,
        [AttachData(MapControlTypeAttachData.Name, "视图控件")]
        AppViewList,
        [AttachData(MapControlTypeAttachData.Name, "数据绑定列表控件")]
        Repeater,
        [AttachData(MapControlTypeAttachData.Name, "列表菜单控件")]
        AppGridMenu,
        [AttachData(MapControlTypeAttachData.Name, "二级界面菜单控件")]
        AppFormMenu,
        [AttachData(MapControlTypeAttachData.Name, "无效控件，SDK无介绍")]
        appGridl,
        [AttachData(MapControlTypeAttachData.Name, "可编辑的列表控件")]
        AppGridE,
        [AttachData(MapControlTypeAttachData.Name, "无效控件，SDK无介绍")]
        GridTree,
        [AttachData(MapControlTypeAttachData.Name, "资源加载控件")]
        Loader
    }
}
