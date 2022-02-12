<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LineasFactura.aspx.cs" Inherits="GesFactura.LineasFactura" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="CSS/HojaEstilos.css" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Uso de Servicio Web - Cálculos factura de un artículo</h2>
        </div>
        <div id="labels" class="auto-style1">
            <asp:Label ID="lblCantidad" runat="server" Text="Cantidad"></asp:Label>
            <br />
            <asp:Label ID="lblPrecio" runat="server" Text="Precio"></asp:Label>
            <br />
            <asp:Label ID="lblDescuento1" runat="server" Text="Descuento (%)"></asp:Label>
            <br />
            <asp:Label ID="lblTipoIva" runat="server" Text="Tipo de IVA (%)"></asp:Label>
        </div>
        <div id="cajasTexto" class="auto-style2">
            <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtPrecio" runat="server"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtDescuento" runat="server"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtTipoIVA" runat="server"></asp:TextBox>
            <br />
        </div>
        <div id="central">
            <asp:Button ID="btnEnviar" runat="server" Text="Enviar" OnClick="btnEnviar_Click" />
            <br /><br /><br /><br /><br /><br /><br /><br />
            <asp:Label ID="lblResultado" runat="server" Text="Resultado de los cálculos"></asp:Label>
        </div>
        <div id="labels2">
            <asp:Label ID="lbl2Bruto" runat="server" Text="Bruto"></asp:Label>
            <asp:Label ID="lbl2Descuento" runat="server" Text="Descuento"></asp:Label>
            <asp:Label ID="lbl2BasImponible" runat="server" Text="Base imponible"></asp:Label>
            <asp:Label ID="lbl2IVA" runat="server" Text="IVA"></asp:Label>
            <asp:Label ID="lbl2Total" runat="server" Text="Total"></asp:Label>
        </div>
        <div id="labelsResul">
            <asp:Label ID="lblBruto" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblDescuento" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblBaseImponible" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblIva" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>