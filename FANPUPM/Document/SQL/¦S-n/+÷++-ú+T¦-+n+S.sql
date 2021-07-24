-----------系统桌面 模块  syf  2011.10.31
--------begin-------------
INSERT [PT_MK] ([C_MKDM],[V_MKMC],[C_BS],[I_XH],[I_ID],[i_ChildNum],[IsBasic],[IsMaintainable],[Isdisplay]) VALUES ( '3817','桌面管理','y',1,2225,3,'0','0','1')
INSERT [PT_MK] ([C_MKDM],[V_MKMC],[V_CDLJ],[C_BS],[I_XH],[I_ID],[i_ChildNum],[IsBasic],[IsMaintainable],[Isdisplay]) VALUES ( '381701','桌面显示设置','/TableTop/ModelList.aspx','y',1,2255,0,'1','0','1')
INSERT [PT_MK] ([C_MKDM],[V_MKMC],[V_CDLJ],[C_BS],[I_XH],[I_ID],[i_ChildNum],[IsBasic],[IsMaintainable],[Isdisplay]) VALUES ( '381703','桌面导航设置','/TableTop/menuList.aspx','y',5,2268,0,'1','0','1')
INSERT [PT_MK] ([C_MKDM],[V_MKMC],[V_CDLJ],[C_BS],[I_XH],[I_ID],[i_ChildNum],[IsBasic],[IsMaintainable],[Isdisplay]) VALUES ( '381705','外部快捷连接','/TableTop/WebLink.aspx','y',5,2257,0,'1','0','1')

INSERT [PT_MK] ([C_MKDM],[V_MKMC],[V_CDLJ],[C_BS],[I_XH],[I_ID],[i_ChildNum],[IsBasic],[IsMaintainable],[Isdisplay]) VALUES ( '2801','待办工作','/SysFrame/PTDBSJList.aspx','n',1,2247,0,'1','0','0')
----syf 2011-11-28 11:50
INSERT [PT_MK] ([C_MKDM],[V_MKMC],[V_CDLJ],[C_BS],[I_XH],[I_ID],[i_ChildNum],[IsBasic],[IsMaintainable],[Isdisplay]) VALUES ( '2827','内部新闻','/WEB/WebManagerList.aspx?c_xwlxdm=99&c_xwlxmc=公司新闻&browse=true','y',10,2227,0,'0','0','1')
---------end--------------
----2011.11.24 hxp
update [PT_MK] set [V_MKMC]='内部应用一', [V_CDLJ]='/TableTop/menuList.aspx?op=1' where [C_MKDM]='381703'
update [PT_MK] set [V_MKMC]='收藏夹' where [C_MKDM]='381705'
INSERT [PT_MK] ([C_MKDM],[V_MKMC],[V_CDLJ],[C_BS],[I_XH],[I_ID],[i_ChildNum],[IsBasic],[IsMaintainable],[Isdisplay]) VALUES ( '381707','内部应用二','/TableTop/menuList.aspx?op=2','y',5,2268,0,'1','0','1')
INSERT [PT_MK] ([C_MKDM],[V_MKMC],[V_CDLJ],[C_BS],[I_XH],[I_ID],[i_ChildNum],[IsBasic],[IsMaintainable],[Isdisplay]) VALUES ( '381708','内部应用三','/TableTop/menuList.aspx?op=3','y',5,2268,0,'1','0','1')
INSERT [PT_MK] ([C_MKDM],[V_MKMC],[V_CDLJ],[C_BS],[I_XH],[I_ID],[i_ChildNum],[IsBasic],[IsMaintainable],[Isdisplay]) VALUES ( '381709','内部应用四','/TableTop/menuList.aspx?op=4','y',5,2268,0,'1','0','1')
INSERT [PT_MK] ([C_MKDM],[V_MKMC],[V_CDLJ],[C_BS],[I_XH],[I_ID],[i_ChildNum],[IsBasic],[IsMaintainable],[Isdisplay]) VALUES ( '381710','内部应用五','/TableTop/menuList.aspx?op=5','y',5,2268,0,'1','0','1')
INSERT [PT_MK] ([C_MKDM],[V_MKMC],[V_CDLJ],[C_BS],[I_XH],[I_ID],[i_ChildNum],[IsBasic],[IsMaintainable],[Isdisplay]) VALUES ( '381711','内部应用六','/TableTop/menuList.aspx?op=6','y',5,2268,0,'1','0','1')