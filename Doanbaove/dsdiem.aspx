<%@ Page Title="" Language="C#" MasterPageFile="~/trangchu.Master" AutoEventWireup="true" CodeBehind="dsdiem.aspx.cs" Inherits="Doanbaove.dsdiem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dn" style="margin-left: 50px;">
        <h4 style="color: #3a60d0; font-weight: bold;">Nhập mã Sinh viên</h4>
        <table>
            <tr>
                <td>
                    <asp:TextBox ID="txt_masv" runat="server" CssClass="w3-input w3-small  w3-border" Width="100px" Height="35"></asp:TextBox></td>
                <td>
                    <asp:Button ID="Button1" runat="server" CssClass="w3-btn w3-blue" Text="OK" class="btn btn-success" Height="35" OnClick="Button1_Click" /></td>
            </tr>
        </table>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_masv" ErrorMessage="RequiredFieldValidator" ForeColor="Red">Chưa nhập mã sinh viên.</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text=""></asp:Label><br />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Vui lòng nhập đúng mã sinh viên" ControlToValidate="txt_masv" ForeColor="Red" ValidationExpression="\d{10}">Đây không phải Mã Sinh Viên ( 10 số)</asp:RegularExpressionValidator>

    </div>
</asp:Content>
