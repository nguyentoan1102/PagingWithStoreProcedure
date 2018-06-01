<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PagingIndex.aspx.cs" Inherits="PaginInWebForms.PagingIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div>
        <form id="from1" runat="server">
            PageSize:
        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="PageSize_Changed">
            <asp:ListItem Text="10" Value="10" />
            <asp:ListItem Text="25" Value="25" />
            <asp:ListItem Text="50" Value="50" />
        </asp:DropDownList>
            <hr />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="CustomerId" DataField="CustomerId" />
                    <asp:BoundField HeaderText="ContactName" DataField="ContactName" />
                    <asp:BoundField HeaderText="CompanyName" DataField="CompanyName" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:Repeater ID="rptPager" runat="server">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="Page_Changed"></asp:LinkButton>
                </ItemTemplate>
            </asp:Repeater>
        </form>
    </div>
</body>
</html>