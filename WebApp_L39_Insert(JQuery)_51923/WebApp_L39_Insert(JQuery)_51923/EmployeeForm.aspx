<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeForm.aspx.cs" Inherits="WebApp_L39_Insert_JQuery__51923.EmployeeForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="39.d.jquery.js"></script>
    <script  type="text/javascript">

        var IDD = 0;// kyo ki JS me page postbacck nahi hota iss liye viewstate ki reqrmnt nahi hai and waisi bhi JS me jyada option nahi hai VAR hi hai only id store karne ke liye
        $(document).ready(function () {// this function is used for running any function.method on page load.
            ShowData();
        });

        function clear() {
            $("#txtname").val("");
            $("#txtaddress").val("");
            $("#txtage").val("");
            $("#btnsave").val("Save");
            IDD = 0;
        }

        function ShowData() {
            $.ajax({
                url: 'EmployeeForm.aspx/GetData',
                type: 'post',
                contentType: 'application/json;charset:utf-8',
                dataType: 'json',
                data: "{}",
                success: function (data) {
                    data = JSON.parse(data.d);
                    //jsondata = JSON.parse(data.d);
                    //$("#tbldisplay").find("tr").remove();
                    $("#tbldisplay").find("tr:gt(0)").remove();
                    for (var i = 0; i < data.length; i++) {
                        $("#tbldisplay").append('<tr>  <td>' + data[i].empid + '</td> <td>' + data[i].name + '</td>  <td>' + data[i].address + '</td>  <td>' + data[i].age + '</td>  <td><input type="button" id="btndelete" value="Delete" onclick="return DeleteData(' + data[i].empid + ')" /></td>    <td><input type="button" id="btnedit" value="Edit" onclick="EditData(' + data[i].empid + ')" /></td> </tr>');
                    }
                    //for (var i = 0; i < jsondata.length; i++) {
                    //    $("#tbldisplay").append('<tr>  <tr>' + jsondata[i].empid + '</td> <td>' + jsondata[i].name + '</td>  <td>' + jsondata[i].address + '</td>  <td>' + jsondata[i].age + '</td> </tr>');
                    //}
                },
                error: function () {
                    alert('Data not found !!');
                }
            });
        }

        function SaveData() {
            if ($("#btnsave").val() == "Save") {
                $.ajax({
                    url: 'EmployeeForm.aspx/Insert',
                    type: 'post',
                    contentType: 'application/json;charset:utf-8',
                    dataType: 'json',
                    data: "{name : '" + $("#txtname").val() + "',address : '" + $("#txtaddress").val() + "', age : '" + $("#txtage").val() + "' }",
                    success: function () {
                        ShowData();
                        //alert('Data saved successfully !!');
                        clear();
                    },
                    error: function () {
                        alert('Data not saved !!');
                    }
                });
            }
            else {
                $.ajax({
                    url: 'EmployeeForm.aspx/Update',
                    type: 'post',
                    contentType: 'application/json;charset:utf-8',
                    dataType: 'json',
                    data: "{empid: '" + IDD + "',name : '" + $("#txtname").val() + "',address : '" + $("#txtaddress").val() + "', age : '" + $("#txtage").val() + "' }",
                    success: function () {
                        ShowData();
                        //alert('Data updated successfully !!');
                        clear();
                    },
                    error: function () {
                        alert('Data not updated !!');
                    }
                });
            }
        }

        function DeleteData(empid) {
            if (confirm('are you sure you want to delete !!')) {
                $.ajax({
                    url: 'EmployeeForm.aspx/Delete',
                    type: 'post',
                    contentType: 'application/json;charset:utf-8',
                    dataType: 'json',
                    data: "{empid : '" + empid + "'}",
                    success: function () {
                        ShowData();
                        //alert('Data deleted successfully !!');   
                    },
                    error: function () {
                        alert('Data not deleted !!');
                    }
                });                
            }
          
        }

        function EditData(empid) {
            $.ajax({
                url: 'EmployeeForm.aspx/Edit',
                type: 'post',
                contentType: 'application/json;charset:utf-8',
                dataType: 'json',
                data: "{empid : '" + empid + "'}",
                success: function (data) {
                    data = JSON.parse(data.d)
                    $("#txtname").val(data[0].name)
                    $("#txtaddress").val(data[0].address);
                    $("#txtage").val(data[0].age);
                    $("#btnsave").val("Update");
                    IDD = empid;
                    
                },
                error: function () {
                    alert('Data not edited !!');
                }
            });
        }
    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>Name:</td>
                    <td><input type="text" id="txtname" /></td> <%--this is html control on aspx page--%>
                </tr>
                <tr>
                    <td>Address:</td>
                    <td><input type="text" id="txtaddress" /></td>
                </tr>
                <tr>
                    <td>Age:</td>
                    <td><input type="text" id="txtage"  /></td>
                </tr>
                <tr>
                    <td></td>
                    <td><input type="button" id="btnsave" value="Save" onclick="return SaveData()" /> </td> 
                </tr>
            </table>

            <table id="tbldisplay" border="1" style="background-color:gray;color:white;width:600px">
                <tr style="background-color:purple">
                    <th>Emp ID</th> <th>Emp Name</th> <th>Emp Address</th> <th>Emp Age</th> <th>Action</th> <th>Action</th> 
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
