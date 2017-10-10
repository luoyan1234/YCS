<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataStruct.aspx.cs" Inherits="YCS.Web.Tool.DataStruct" %>

<%@ Import Namespace="YCS.Common" %>
<!doctype html>
<html>
<head id="Head1" runat="server">
    <title>数据库表结构浏览工具</title>
    <style type="text/css">
        table {
            background-color: #D7D7E5;
        }

            table td {
                font-size: 12px;
                width: 10%;
            }

        .tr {
            background-color: #F0F0F0;
        }

        .td {
            background-color: #ffffff;
        }

        .over {
            background-color: #E1F5FF;
        }


        .tb {
            font-size: 12px;
            height: 30px;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }

        .nav a {
            margin: 5px;
        }
    </style>
</head>
<body>
    <div class="nav">
        导航:
        <select onchange="location.href='#'+this.options[this.selectedIndex].value">
            <asp:Repeater ID="repNavSql" runat="server">
                <ItemTemplate>
                    <option value="<%#Eval("Name") %>">
                        <%#Container.ItemIndex+1 %>.<%#Eval("Name") %>--<%#Eval("Description")%></option>
                </ItemTemplate>
            </asp:Repeater>
        </select>
    </div>
    <asp:Repeater ID="repTableSql" runat="server" OnItemDataBound="repTableSql_ItemDataBound">
        <ItemTemplate>
            <div class="tb">
                <%#Container.ItemIndex+1 %>.<%#Eval("Description")%>:t_<%#Eval("Name") %><a name="<%#Eval("Name") %>"></a>&nbsp;&nbsp;&nbsp;<a href="#">TOP</a>
            </div>
            <table cellspacing="1" cellpadding="5" border="0" width="100%">
                <tr class="tr">
                    <td>序号
                    </td>
                    <td>列名
                    </td>
                    <td>数据类型
                    </td>
                    <td>长度
                    </td>
                    <td>小数位
                    </td>
                    <td>标识
                    </td>
                    <td>主键
                    </td>
                    <td>允许空
                    </td>
                    <td>默认值
                    </td>
                    <td>说明
                    </td>
                </tr>
                <asp:Repeater ID="repCol" runat="server">
                    <ItemTemplate>
                        <tr class="td" onmouseover="this.className='over'" onmouseout="this.className='td'">
                            <td>
                                <%#Eval("ColumnSort")%>
                            </td>
                            <td>
                                <%#Eval("FieldName")%>
                            </td>
                            <td>
                                <%#Eval("DataType")%>
                            </td>
                            <td>
                                <%#Eval("Length")%>
                            </td>
                            <td>
                                <%#Eval("DecimalDigit")%>
                            </td>
                            <td>
                                <%#Eval("IsIdentity")%>
                            </td>
                            <td>
                                <%#Eval("IsPrimaryKey")%>
                            </td>
                            <td>
                                <%#Eval("IsNull")%>
                            </td>
                            <td>
                                <%#Eval("DefaultValue")%>
                            </td>
                            <td>
                                <%#Eval("ColumnDescription")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </ItemTemplate>
    </asp:Repeater>
</body>
</html>
