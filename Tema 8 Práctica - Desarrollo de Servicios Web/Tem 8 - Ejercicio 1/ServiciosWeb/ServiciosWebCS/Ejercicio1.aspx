<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ejercicio1.aspx.cs" Inherits="ServiciosWebCS.Ejercicio1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="/CSS/HojaDeEstilo.css" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3>CONSUMIR UN SERVICIO WEB YA EXISTENTE</h3>
            <h2>Titulaciones Oficiales en la Universidad de Alicante</h2>
        </div>
        <br />
        <div>
            <p>
                Curso académico (formato aaaa-aa)
                <asp:TextBox ID="txtCurso" runat="server"></asp:TextBox>
                <asp:Button ID="btnObtenerInformacion" runat="server" Text="Obtener información" OnClick="btnObtenerInformacion_Click" />
            </p>
        </div>
        <div id="bloque2">
            <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label><br />
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        </div>
    </form>
</body>
</html>
