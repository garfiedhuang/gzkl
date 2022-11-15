USE [gzkldb]
GO
/****** Object:  Table [dbo].[sys_config]    Script Date: 2022/11/15 22:47:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sys_config](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[category] [varchar](50) NOT NULL,
	[value] [varchar](100) NOT NULL,
	[text] [varchar](100) NOT NULL,
	[remark] [varchar](200) NULL,
	[is_enabled] [bit] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[create_dt] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[update_dt] [datetime] NULL,
	[update_user_id] [bigint] NULL,
 CONSTRAINT [PK_sys_config] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sys_db_backup]    Script Date: 2022/11/15 22:47:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sys_db_backup](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[backup_no] [varchar](50) NOT NULL,
	[path] [varchar](500) NOT NULL,
	[file_name] [varchar](100) NOT NULL,
	[remark] [varchar](250) NULL,
	[is_enabled] [bit] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[create_dt] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[update_dt] [datetime] NULL,
	[update_user_id] [bigint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sys_menu]    Script Date: 2022/11/15 22:47:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sys_menu](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[parent_id] [bigint] NULL,
	[name] [varchar](50) NOT NULL,
	[url] [varchar](200) NULL,
	[icon] [varchar](100) NOT NULL,
	[type] [varchar](10) NOT NULL,
	[sort] [int] NOT NULL,
	[is_enabled] [bit] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[create_dt] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[update_dt] [datetime] NULL,
	[update_user_id] [bigint] NULL,
 CONSTRAINT [PK_sys_menu] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sys_params_backup]    Script Date: 2022/11/15 22:47:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sys_params_backup](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[backup_no] [varchar](50) NOT NULL,
	[json_content] [varchar](max) NOT NULL,
	[remark] [varchar](200) NULL,
	[is_enabled] [bit] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[create_dt] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[update_dt] [datetime] NULL,
	[update_user_id] [bigint] NULL,
 CONSTRAINT [PK_sys_params_backup] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sys_role]    Script Date: 2022/11/15 22:47:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sys_role](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[remark] [varchar](250) NULL,
	[is_deleted] [bit] NOT NULL,
	[create_dt] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[update_dt] [datetime] NULL,
	[update_user_id] [bigint] NULL,
 CONSTRAINT [PK_sys_role] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sys_role_menu]    Script Date: 2022/11/15 22:47:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sys_role_menu](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[role_id] [bigint] NOT NULL,
	[menu_id] [bigint] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[create_dt] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[update_dt] [datetime] NULL,
	[update_user_id] [bigint] NULL,
 CONSTRAINT [PK_sys_role_menu] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sys_user]    Script Date: 2022/11/15 22:47:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sys_user](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[head_img] [varchar](250) NULL,
	[phone] [varchar](50) NULL,
	[email] [varchar](50) NULL,
	[sex] [tinyint] NULL,
	[birthday] [date] NULL,
	[is_enabled] [bit] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[create_dt] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[update_dt] [datetime] NULL,
	[update_user_id] [bigint] NULL,
 CONSTRAINT [PK_sys_user] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sys_user_role]    Script Date: 2022/11/15 22:47:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sys_user_role](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[user_id] [bigint] NOT NULL,
	[role_id] [bigint] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[create_dt] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[update_dt] [datetime] NULL,
	[update_user_id] [bigint] NULL,
 CONSTRAINT [PK_sys_user_role] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[sys_config] ADD  CONSTRAINT [DF_sys_config_is_enabled]  DEFAULT ((0)) FOR [is_enabled]
GO
ALTER TABLE [dbo].[sys_config] ADD  CONSTRAINT [DF_sys_config_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[sys_db_backup] ADD  CONSTRAINT [DF_sys_db_backup_is_enabled]  DEFAULT ((1)) FOR [is_enabled]
GO
ALTER TABLE [dbo].[sys_db_backup] ADD  CONSTRAINT [DF_sys_db_backup_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[sys_menu] ADD  CONSTRAINT [DF_sys_menu_is_enabled]  DEFAULT ((0)) FOR [is_enabled]
GO
ALTER TABLE [dbo].[sys_menu] ADD  CONSTRAINT [DF_sys_menu_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[sys_params_backup] ADD  CONSTRAINT [DF_sys_params_backup_is_enabled]  DEFAULT ((0)) FOR [is_enabled]
GO
ALTER TABLE [dbo].[sys_params_backup] ADD  CONSTRAINT [DF_sys_params_backup_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[sys_role] ADD  CONSTRAINT [DF_sys_role_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[sys_role_menu] ADD  CONSTRAINT [DF_sys_role_menu_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[sys_user] ADD  CONSTRAINT [DF_sys_user_sex]  DEFAULT ((0)) FOR [sex]
GO
ALTER TABLE [dbo].[sys_user] ADD  CONSTRAINT [DF_sys_user_is_enable]  DEFAULT ((0)) FOR [is_enabled]
GO
ALTER TABLE [dbo].[sys_user] ADD  CONSTRAINT [DF_sys_user_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[sys_user_role] ADD  CONSTRAINT [DF_sys_user_role_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_config', @level2type=N'COLUMN',@level2name=N'id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分类' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_config', @level2type=N'COLUMN',@level2name=N'category'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_config', @level2type=N'COLUMN',@level2name=N'value'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_config', @level2type=N'COLUMN',@level2name=N'text'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_config', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_config', @level2type=N'COLUMN',@level2name=N'is_enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_config', @level2type=N'COLUMN',@level2name=N'is_deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_config', @level2type=N'COLUMN',@level2name=N'create_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_config', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_config', @level2type=N'COLUMN',@level2name=N'update_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_config', @level2type=N'COLUMN',@level2name=N'update_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备份编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_db_backup', @level2type=N'COLUMN',@level2name=N'backup_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'保存路径(绝对路径)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_db_backup', @level2type=N'COLUMN',@level2name=N'path'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件名(包含扩展名)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_db_backup', @level2type=N'COLUMN',@level2name=N'file_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_db_backup', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_db_backup', @level2type=N'COLUMN',@level2name=N'is_enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_db_backup', @level2type=N'COLUMN',@level2name=N'is_deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_db_backup', @level2type=N'COLUMN',@level2name=N'create_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_db_backup', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_db_backup', @level2type=N'COLUMN',@level2name=N'update_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_db_backup', @level2type=N'COLUMN',@level2name=N'update_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_menu', @level2type=N'COLUMN',@level2name=N'id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父菜单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_menu', @level2type=N'COLUMN',@level2name=N'parent_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_menu', @level2type=N'COLUMN',@level2name=N'name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单链接' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_menu', @level2type=N'COLUMN',@level2name=N'url'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图标' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_menu', @level2type=N'COLUMN',@level2name=N'icon'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型 button-按钮, group-分组, menu-菜单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_menu', @level2type=N'COLUMN',@level2name=N'type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_menu', @level2type=N'COLUMN',@level2name=N'sort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_menu', @level2type=N'COLUMN',@level2name=N'is_enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_menu', @level2type=N'COLUMN',@level2name=N'is_deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_menu', @level2type=N'COLUMN',@level2name=N'create_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_menu', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_menu', @level2type=N'COLUMN',@level2name=N'update_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_menu', @level2type=N'COLUMN',@level2name=N'update_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_params_backup', @level2type=N'COLUMN',@level2name=N'id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备份编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_params_backup', @level2type=N'COLUMN',@level2name=N'backup_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参数报文' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_params_backup', @level2type=N'COLUMN',@level2name=N'json_content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_params_backup', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_params_backup', @level2type=N'COLUMN',@level2name=N'is_enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_params_backup', @level2type=N'COLUMN',@level2name=N'is_deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_params_backup', @level2type=N'COLUMN',@level2name=N'create_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_params_backup', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_params_backup', @level2type=N'COLUMN',@level2name=N'update_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_params_backup', @level2type=N'COLUMN',@level2name=N'update_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_role', @level2type=N'COLUMN',@level2name=N'id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_role', @level2type=N'COLUMN',@level2name=N'name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_role', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_role', @level2type=N'COLUMN',@level2name=N'is_deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_role', @level2type=N'COLUMN',@level2name=N'create_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_role', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_role', @level2type=N'COLUMN',@level2name=N'update_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_role', @level2type=N'COLUMN',@level2name=N'update_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_role_menu', @level2type=N'COLUMN',@level2name=N'id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_role_menu', @level2type=N'COLUMN',@level2name=N'role_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_role_menu', @level2type=N'COLUMN',@level2name=N'menu_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_role_menu', @level2type=N'COLUMN',@level2name=N'is_deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_role_menu', @level2type=N'COLUMN',@level2name=N'create_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_role_menu', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_role_menu', @level2type=N'COLUMN',@level2name=N'update_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_role_menu', @level2type=N'COLUMN',@level2name=N'update_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user', @level2type=N'COLUMN',@level2name=N'id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user', @level2type=N'COLUMN',@level2name=N'name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user', @level2type=N'COLUMN',@level2name=N'password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'头像' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user', @level2type=N'COLUMN',@level2name=N'head_img'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user', @level2type=N'COLUMN',@level2name=N'phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电子邮箱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user', @level2type=N'COLUMN',@level2name=N'email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'性别 0-未知 1-男 2-女' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user', @level2type=N'COLUMN',@level2name=N'sex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生日' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user', @level2type=N'COLUMN',@level2name=N'birthday'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user', @level2type=N'COLUMN',@level2name=N'is_enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user', @level2type=N'COLUMN',@level2name=N'is_deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user', @level2type=N'COLUMN',@level2name=N'create_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user', @level2type=N'COLUMN',@level2name=N'update_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user', @level2type=N'COLUMN',@level2name=N'update_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user_role', @level2type=N'COLUMN',@level2name=N'id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user_role', @level2type=N'COLUMN',@level2name=N'user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user_role', @level2type=N'COLUMN',@level2name=N'role_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user_role', @level2type=N'COLUMN',@level2name=N'is_deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user_role', @level2type=N'COLUMN',@level2name=N'create_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user_role', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user_role', @level2type=N'COLUMN',@level2name=N'update_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user_role', @level2type=N'COLUMN',@level2name=N'update_user_id'
GO
