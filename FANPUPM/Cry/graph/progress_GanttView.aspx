﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="progress_GanttView.aspx.cs" Inherits="Cry_graph_progress_GanttView" %>
<%@ Register Assembly="TeeChart" Namespace="Steema.TeeChart.Web" TagPrefix="tchart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>项目进度甘特图</title></head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%" align=left>
              <tr>
                    <td>
                        项目名称：<asp:DropDownList ID="ddlPrjname" Width="129px" AutoPostBack="true" OnSelectedIndexChanged="ddlPrjname_SelectedIndexChanged" runat="server"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td align=left>
                        <tchart:WebChart ID="gantChart" AutoPostback="false" GetChartFile="aspx" Height="440px" TempChart="Session" Width="791px" Config="AAEAAAD/////AQAAAAAAAAAMAgAAAFJUZWVDaGFydCwgVmVyc2lvbj0zLjIuMjc2My4yNjA4NCwgQ3VsdHVyZT1uZXV0cmFsLCBQdWJsaWNLZXlUb2tlbj1mYjk5Yjg0NzRmMDU3M2NlDAMAAABRU3lzdGVtLkRyYXdpbmcsIFZlcnNpb249Mi4wLjAuMCwgQ3VsdHVyZT1uZXV0cmFsLCBQdWJsaWNLZXlUb2tlbj1iMDNmNWY3ZjExZDUwYTNhBQEAAAAVU3RlZW1hLlRlZUNoYXJ0LkNoYXJ0YwAAAA4uWm9vbS5BbmltYXRlZA8uWm9vbS5EaXJlY3Rpb24PLlBhZ2UuQXV0b1NjYWxlGS5XYWxscy5CYWNrLkJydXNoLlZpc2libGUkLldhbGxzLkJhY2suQnJ1c2guR3JhZGllbnQuVXNlTWlkZGxlJi5XYWxscy5CYWNrLkJydXNoLkdyYWRpZW50Lk1pZGRsZUNvbG9yJS5XYWxscy5MZWZ0LkJydXNoLkdyYWRpZW50LlNpZ21hU2NhbGUkLldhbGxzLkxlZnQuQnJ1c2guR3JhZGllbnQuVXNlTWlkZGxlIy5XYWxscy5MZWZ0LkJydXNoLkdyYWRpZW50LkVuZENvbG9yJi5XYWxscy5MZWZ0LkJydXNoLkdyYWRpZW50Lk1pZGRsZUNvbG9yJS5XYWxscy5MZWZ0LkJydXNoLkdyYWRpZW50LlN0YXJ0Q29sb3IlLldhbGxzLkxlZnQuQnJ1c2guR3JhZGllbnQuU2lnbWFGb2N1cycuV2FsbHMuQm90dG9tLkJydXNoLkdyYWRpZW50LlNpZ21hU2NhbGUmLldhbGxzLkJvdHRvbS5CcnVzaC5HcmFkaWVudC5Vc2VNaWRkbGUlLldhbGxzLkJvdHRvbS5CcnVzaC5HcmFkaWVudC5FbmRDb2xvciguV2FsbHMuQm90dG9tLkJydXNoLkdyYWRpZW50Lk1pZGRsZUNvbG9yJy5XYWxscy5Cb3R0b20uQnJ1c2guR3JhZGllbnQuU3RhcnRDb2xvcicuV2FsbHMuQm90dG9tLkJydXNoLkdyYWRpZW50LlNpZ21hRm9jdXMYLldhbGxzLlJpZ2h0LkJydXNoLkNvbG9yJi5XYWxscy5SaWdodC5CcnVzaC5HcmFkaWVudC5TaWdtYVNjYWxlJS5XYWxscy5SaWdodC5CcnVzaC5HcmFkaWVudC5Vc2VNaWRkbGUkLldhbGxzLlJpZ2h0LkJydXNoLkdyYWRpZW50LkVuZENvbG9yJy5XYWxscy5SaWdodC5CcnVzaC5HcmFkaWVudC5NaWRkbGVDb2xvciYuV2FsbHMuUmlnaHQuQnJ1c2guR3JhZGllbnQuU3RhcnRDb2xvciYuV2FsbHMuUmlnaHQuQnJ1c2guR3JhZGllbnQuU2lnbWFGb2N1cwhTZXJpZXMuMA8uU2VyaWVzLjAuVGl0bGUXLlNlcmllcy4wLllWYWx1ZXMuVmFsdWUXLlNlcmllcy4wLllWYWx1ZXMuQ291bnQcLlNlcmllcy4wLllWYWx1ZXMuRGF0YU1lbWJlchcuU2VyaWVzLjAuWFZhbHVlcy5WYWx1ZRcuU2VyaWVzLjAuWFZhbHVlcy5Db3VudBwuU2VyaWVzLjAuWFZhbHVlcy5EYXRhTWVtYmVyGi5TZXJpZXMuMC5YVmFsdWVzLkRhdGVUaW1lFy5TZXJpZXMuMC5NYXJrcy5WaXNpYmxlHi5TZXJpZXMuMC5NYXJrcy5JdGVtcy5DYXBhY2l0eSAuU2VyaWVzLjAuTWFya3MuQ2FsbG91dC5EaXN0YW5jZR4uU2VyaWVzLjAuTWFya3MuQ2FsbG91dC5MZW5ndGgeLlNlcmllcy4wLk1hcmtzLkNhbGxvdXQuRHJhdzNEIS5TZXJpZXMuMC5NYXJrcy5DYWxsb3V0LkFycm93SGVhZB0uU2VyaWVzLjAuTWFya3MuQ2FsbG91dC5TdHlsZSUuU2VyaWVzLjAuTWFya3MuQ2FsbG91dC5BcnJvd0hlYWRTaXplIy5TZXJpZXMuMC5NYXJrcy5DYWxsb3V0LkJydXNoLkNvbG9yFS5TZXJpZXMuMC5NYXJrcy5TdHlsZSkuU2VyaWVzLjAuTWFya3MuQnJ1c2guR3JhZGllbnQuU2lnbWFTY2FsZSguU2VyaWVzLjAuTWFya3MuQnJ1c2guR3JhZGllbnQuVXNlTWlkZGxlJy5TZXJpZXMuMC5NYXJrcy5CcnVzaC5HcmFkaWVudC5FbmRDb2xvciouU2VyaWVzLjAuTWFya3MuQnJ1c2guR3JhZGllbnQuTWlkZGxlQ29sb3IpLlNlcmllcy4wLk1hcmtzLkJydXNoLkdyYWRpZW50LlN0YXJ0Q29sb3IpLlNlcmllcy4wLk1hcmtzLkJydXNoLkdyYWRpZW50LlNpZ21hRm9jdXMeLlNlcmllcy4wLk1hcmtzLlN5bWJvbC5WaXNpYmxlIi5TZXJpZXMuMC5NYXJrcy5TeW1ib2wuVHJhbnNwYXJlbnQtLlNlcmllcy4wLk1hcmtzLlN5bWJvbC5CcnVzaC5HcmFkaWVudC5WaXNpYmxlGy5TZXJpZXMuMC5Qb2ludGVyLlBlbi5Db2xvchcuU2VyaWVzLjAuUG9pbnRlci5TdHlsZRkuU2VyaWVzLjAuUG9pbnRlci5WaXNpYmxlJy5TZXJpZXMuMC5Qb2ludGVyLkJydXNoLkZvcmVncm91bmRDb2xvcisuU2VyaWVzLjAuUG9pbnRlci5CcnVzaC5HcmFkaWVudC5TdGFydENvbG9yHi5TZXJpZXMuMC5MaW5lUGVuLlRyYW5zcGFyZW5jeRcuU2VyaWVzLjAuTGluZVBlbi5Db2xvchsuU2VyaWVzLjAuU3RhcnRWYWx1ZXMuVmFsdWUbLlNlcmllcy4wLlN0YXJ0VmFsdWVzLkNvdW50IC5TZXJpZXMuMC5TdGFydFZhbHVlcy5EYXRhTWVtYmVyHi5TZXJpZXMuMC5TdGFydFZhbHVlcy5EYXRlVGltZRkuU2VyaWVzLjAuTmV4dFRhc2tzLlZhbHVlGS5TZXJpZXMuMC5OZXh0VGFza3MuQ291bnQeLlNlcmllcy4wLk5leHRUYXNrcy5EYXRhTWVtYmVyGS5TZXJpZXMuMC5FbmRWYWx1ZXMuVmFsdWUZLlNlcmllcy4wLkVuZFZhbHVlcy5Db3VudB4uU2VyaWVzLjAuRW5kVmFsdWVzLkRhdGFNZW1iZXIcLlNlcmllcy4wLkVuZFZhbHVlcy5EYXRlVGltZRMuU2VyaWVzLjAuQ29sb3JFYWNoDi5Bc3BlY3QuVmlldzNEGS5Bc3BlY3QuQ29sb3JQYWxldHRlSW5kZXgWLkFzcGVjdC5DaGFydDNEUGVyY2VudBcuTGVnZW5kLkZvbnRTZXJpZXNDb2xvchAuTGVnZW5kLkludmVydGVkGS5MZWdlbmQuVGl0bGUuUGVuLlZpc2libGUhLkxlZ2VuZC5CcnVzaC5HcmFkaWVudC5TaWdtYVNjYWxlIC5MZWdlbmQuQnJ1c2guR3JhZGllbnQuVXNlTWlkZGxlHy5MZWdlbmQuQnJ1c2guR3JhZGllbnQuRW5kQ29sb3IiLkxlZ2VuZC5CcnVzaC5HcmFkaWVudC5NaWRkbGVDb2xvciEuTGVnZW5kLkJydXNoLkdyYWRpZW50LlN0YXJ0Q29sb3IhLkxlZ2VuZC5CcnVzaC5HcmFkaWVudC5TaWdtYUZvY3VzIC5QYW5lbC5CcnVzaC5HcmFkaWVudC5TaWdtYVNjYWxlIC5QYW5lbC5CcnVzaC5HcmFkaWVudC5TaWdtYUZvY3VzEy5IZWFkZXIuQm9yZGVyUm91bmQNLkhlYWRlci5MaW5lcxIuSGVhZGVyLlNoYXBlU3R5bGUhLkhlYWRlci5CcnVzaC5HcmFkaWVudC5TaWdtYVNjYWxlIC5IZWFkZXIuQnJ1c2guR3JhZGllbnQuVXNlTWlkZGxlHy5IZWFkZXIuQnJ1c2guR3JhZGllbnQuRW5kQ29sb3IiLkhlYWRlci5CcnVzaC5HcmFkaWVudC5NaWRkbGVDb2xvciEuSGVhZGVyLkJydXNoLkdyYWRpZW50LlN0YXJ0Q29sb3IhLkhlYWRlci5CcnVzaC5HcmFkaWVudC5TaWdtYUZvY3VzGy5BeGVzLkxlZnQuTGFiZWxzLkFsdGVybmF0ZRsuQXhlcy5MZWZ0LkxhYmVscy5NdWx0aUxpbmUSLkF4ZXMuTGVmdC5WaXNpYmxlES5BeGVzLlRvcC5WaXNpYmxlAAQAAAAEAAAEBAQAAAAEBAQABAAABAQEAAEBBwABBwABAAAAAAAABAQABAQAAAQEBAAAAAAEBAAEBAAEBwABAAcAAQcAAQAAAAAAAAAAAAAEBAQAAAAABgQAAAQEBAAAAAAAAR5TdGVlbWEuVGVlQ2hhcnQuWm9vbURpcmVjdGlvbnMCAAAAAQEBFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAsBFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAACwsBFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAACxRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAALARRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAsGCAYIAQEICAgBJlN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuQXJyb3dIZWFkU3R5bGVzAgAAACRTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlBvaW50ZXJTdHlsZXMCAAAACBRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAAiU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5NYXJrc1N0eWxlcwIAAAALARRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAsBAQEUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAAJFN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuUG9pbnRlclN0eWxlcwIAAAABFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAAIFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAYIAQYIBggBAQEICAEBAQsBFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAACwsLCCZTdGVlbWEuVGVlQ2hhcnQuRHJhd2luZy5UZXh0U2hhcGVTdHlsZQIAAAALARRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAsBAQEBAgAAAAEF/P///x5TdGVlbWEuVGVlQ2hhcnQuWm9vbURpcmVjdGlvbnMBAAAAB3ZhbHVlX18ACAIAAAAAAAAAAQABBfv///8UU3lzdGVtLkRyYXdpbmcuQ29sb3IEAAAABG5hbWUFdmFsdWUKa25vd25Db2xvcgVzdGF0ZQEAAAAJBwcDAAAACgAAAAAAAAAAAAACAAAAAAABAfr////7////CgD///8AAAAAAAACAAH5////+////woAAAAAAAAAAAAAAgAB+P////v///8K/////wAAAAAAAAIAAAAAAAAAAAABAff////7////CgD///8AAAAAAAACAAH2////+////woAAAAAAAAAAAAAAgAB9f////v///8K/////wAAAAAAAAIAAAAAAAH0////+////wrAwMD/AAAAAAAAAgAAAAAAAQHz////+////woA////AAAAAAAAAgAB8v////v///8KAAAAAAAAAAAAAAIAAfH////7////Cv////8AAAAAAAACAAAAAAAGEAAAABxTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLkdhbnR0BhEAAAAGZ2FudHQxCRIAAAAAAAAABhMAAAABWQkUAAAAAAAAAAYVAAAABVN0YXJ0AQEAAAAAAAAAAAAAAAAABer///8mU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5BcnJvd0hlYWRTdHlsZXMBAAAAB3ZhbHVlX18ACAIAAAABAAAABen///8kU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5Qb2ludGVyU3R5bGVzAQAAAAd2YWx1ZV9fAAgCAAAAAAAAAAgAAAAB6P////v///8KAAAAAAAAAAAjAAEABef///8iU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5NYXJrc1N0eWxlcwEAAAAHdmFsdWVfXwAIAgAAAAQAAAAAAAAAAQHm////+////woA////AAAAAAAAAgAB5f////v///8KAAAAAAAAAAAAAAIAAeT////7////Cv////8AAAAAAAACAAAAAAABAQEB4/////v///8KYj0p/wAAAAAAAAIAAeL////p////AAAAAAEB4f////v///8KAAAAAAAAAAAAAAAAAeD////7////CqNmRP8AAAAAAAACADIAAAAB3/////v///8KYj0p/wAAAAAAAAIACSIAAAAAAAAACRUAAAABCSQAAAAAAAAABiUAAAAITmV4dFRhc2sJJgAAAAAAAAAGJwAAAANFbmQBAQAAAAAACgAAAAEBAAAAAAABAdj////7////CgD///8AAAAAAAACAAHX////+////woAAAAAAAAAAAAAAgAB1v////v///8K/////wAAAAAAAAIAAAAAAAAAAAAAAAAACAAAAAkrAAAABdT///8mU3RlZW1hLlRlZUNoYXJ0LkRyYXdpbmcuVGV4dFNoYXBlU3R5bGUBAAAAB3ZhbHVlX18ACAIAAAABAAAAAAAAAAEB0/////v///8KAP///wAAAAAAAAIAAdL////7////CgAAAAAAAAAAAAACAAHR////+////wr/////AAAAAAAAAgAAAAAAAQEAAA8SAAAAAAAAAAYPFAAAAAAAAAAGDyIAAAAAAAAABg8kAAAAAAAAAAYPJgAAAAAAAAAGESsAAAABAAAABjAAAAAV6aG555uu6L+b5bqm55SY54m55Zu+Cw==" runat="server"></tchart:WebChart>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>