<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetClass.aspx.cs" Inherits="YCS.Web.Tool.GetClass" ValidateRequest="false" %>

<!doctype html>
<html>
<head runat="server">
    <title>代码生成工具v4.0</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%" border="0" cellspacing="1" cellpadding="3">
                <tr>
                    <td align="right">项目名称：
                    </td>
                    <td>
                        <asp:TextBox ID="txtProjectName" runat="server" Text="YCS"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">区域名称：
                    </td>
                    <td>
                        <asp:TextBox ID="txtAreaName" runat="server" Text="Admin"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">表名：
                    </td>
                    <td>
                        <asp:DropDownList ID="drpTableName" runat="server" OnSelectedIndexChanged="drpTableName_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="17%" align="right">类名：
                    </td>
                    <td width="83%">
                        <asp:TextBox ID="txtClassName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">类说明：
                    </td>
                    <td>
                        <asp:TextBox ID="txtClassDesc" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">类对象实例名:
                    </td>
                    <td>
                        <asp:TextBox ID="txtObjName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">创建人：
                    </td>
                    <td>
                        <asp:TextBox ID="txtAuthor" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">&nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="生成代码" CommandName="Save" OnCommand="btnSave_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="right">&nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnFactory" runat="server" Text="生成工厂类" CommandName="Factory" OnCommand="btnSave_Click" />
                        <asp:Button ID="btnContext" runat="server" Text="生成上下文类" CommandName="Context" OnCommand="btnSave_Click" />
                        <asp:Button ID="btnModel" runat="server" Text="生成所有Model/DAL/BLL" CommandName="Model" OnCommand="btnSave_Click" />
                        <asp:Button ID="btnControllers" runat="server" Text="生成所有Controllers" CommandName="Controllers" OnCommand="btnSave_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="right">&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="errMsg" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
