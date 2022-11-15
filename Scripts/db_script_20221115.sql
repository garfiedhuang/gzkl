USE [gzkldb]
GO
/****** Object:  Table [dbo].[base_interface]    Script Date: 2022-11-15 17:32:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_interface](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[interface_name] [varchar](100) NOT NULL,
	[access_db_path] [varchar](250) NOT NULL,
	[access_db_name] [varchar](50) NOT NULL,
	[remark] [varchar](250) NULL,
	[is_enabled] [bit] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[create_dt] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[update_dt] [datetime] NULL,
	[update_user_id] [bigint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[base_interface_relation]    Script Date: 2022-11-15 17:32:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_interface_relation](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[interface_id] [bigint] NOT NULL,
	[test_item_no] [varchar](50) NOT NULL,
	[test_item_id] [bigint] NOT NULL,
	[test_item_name] [varchar](250) NOT NULL,
	[is_enabled] [bit] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[create_dt] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[update_dt] [datetime] NULL,
	[update_user_id] [bigint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[base_interface_test_item]    Script Date: 2022-11-15 17:32:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_interface_test_item](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[interface_id] [bigint] NOT NULL,
	[test_item_name] [varchar](200) NOT NULL,
	[table_master] [varchar](100) NOT NULL,
	[table_detail] [varchar](100) NULL,
	[table_dot] [varchar](50) NULL,
	[remark] [varchar](250) NULL,
	[is_enabled] [bit] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[create_dt] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[update_dt] [datetime] NULL,
	[update_user_id] [bigint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[base_org]    Script Date: 2022-11-15 17:32:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_org](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[org_no] [varchar](50) NOT NULL,
	[org_name] [varchar](250) NOT NULL,
	[org_level] [varchar](10) NOT NULL,
	[remark] [varchar](250) NULL,
	[is_enabled] [bit] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[create_dt] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[update_dt] [datetime] NULL,
	[update_user_id] [bigint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[base_test_item]    Script Date: 2022-11-15 17:32:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_test_item](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[test_item_no] [varchar](50) NOT NULL,
	[test_item_name] [varchar](250) NOT NULL,
	[test_piece_num] [int] NOT NULL,
	[spec] [varchar](100) NULL,
	[spec_length] [varchar](50) NULL,
	[spec_width] [varchar](50) NULL,
	[spect_height] [varchar](50) NULL,
	[is_deleted] [bit] NOT NULL,
	[create_dt] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[update_dt] [datetime] NULL,
	[update_user_id] [bigint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[base_test_type]    Script Date: 2022-11-15 17:32:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_test_type](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[test_type_no] [varchar](50) NOT NULL,
	[test_type_name] [varchar](250) NOT NULL,
	[category] [varchar](50) NOT NULL,
	[template_file] [varbinary](max) NULL,
	[list_num] [int] NOT NULL,
	[sample_no_cells] [varchar](250) NULL,
	[serial_no] [varchar](50) NULL,
	[task_block_num] [int] NULL,
	[press_flag] [bit] NULL,
	[is_enabled] [bit] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[create_dt] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[update_dt] [datetime] NULL,
	[update_user_id] [bigint] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[base_test_type_item]    Script Date: 2022-11-15 17:32:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_test_type_item](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[test_type_no] [varchar](50) NOT NULL,
	[test_item_no] [varchar](50) NOT NULL,
	[deadline] [int] NOT NULL,
	[limit] [int] NULL,
	[unit] [varchar](50) NULL,
	[cell] [varchar](50) NULL,
	[test_piece_num] [int] NULL,
	[is_enabled] [bit] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[create_dt] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[update_dt] [datetime] NULL,
	[update_user_id] [bigint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[biz_execute_test]    Script Date: 2022-11-15 17:32:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[biz_execute_test](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[org_no] [varchar](50) NOT NULL,
	[test_no] [varchar](50) NOT NULL,
	[sample_no] [varchar](50) NOT NULL,
	[test_type_no] [varchar](50) NOT NULL,
	[test_item_no] [varchar](50) NOT NULL,
	[deadline] [int] NULL,
	[is_deleted] [bit] NOT NULL,
	[create_dt] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[update_dt] [datetime] NULL,
	[update_user_id] [bigint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[biz_execute_test_detail]    Script Date: 2022-11-15 17:32:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[biz_execute_test_detail](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[test_id] [bigint] NOT NULL,
	[experiment_no] [int] NOT NULL,
	[play_time] [datetime] NOT NULL,
	[test_precept_name] [varchar](250) NULL,
	[file_name] [varchar](250) NULL,
	[sample_shape] [varchar](250) NULL,
	[area] [float] NULL,
	[gauge_length] [float] NULL,
	[up_yield_dot] [float] NULL,
	[down_yield_dot] [float] NULL,
	[max_dot] [float] NULL,
	[sample_width] [float] NULL,
	[sample_thick] [float] NULL,
	[sample_dia] [float] NULL,
	[sample_min_dia] [float] NULL,
	[sample_out_dia] [float] NULL,
	[sample_inner_dia] [float] NULL,
	[deform_sensor_name] [varchar](50) NULL,
	[is_deleted] [bit] NOT NULL,
	[create_dt] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[update_dt] [datetime] NULL,
	[update_user_id] [bigint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[biz_interface_import_detail]    Script Date: 2022-11-15 17:32:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[biz_interface_import_detail](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[interface_id] [bigint] NOT NULL,
	[test_item_no] [varchar](50) NOT NULL,
	[test_item_id] [bigint] NOT NULL,
	[test_no] [varchar](50) NULL,
	[sample_no] [varchar](50) NULL,
	[remark] [varchar](250) NULL,
	[is_enabled] [bit] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[create_dt] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[update_dt] [datetime] NULL,
	[update_user_id] [bigint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[biz_original_data]    Script Date: 2022-11-15 17:32:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[biz_original_data](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[test_id] [bigint] NOT NULL,
	[experiment_no] [int] NOT NULL,
	[play_time] [datetime] NOT NULL,
	[load_value] [varchar](50) NULL,
	[position_value] [varchar](50) NULL,
	[extend_value] [varchar](50) NULL,
	[big_deform_value] [varchar](50) NULL,
	[deform_switch] [varchar](50) NULL,
	[ctrl_step] [varchar](50) NULL,
	[extend_device1] [varchar](50) NULL,
	[extend_device2] [varchar](50) NULL,
	[extend_device3] [varchar](50) NULL,
	[extend_device4] [varchar](50) NULL,
	[extend_device5] [varchar](50) NULL,
	[extend_device6] [varchar](50) NULL,
	[posi_speed] [varchar](50) NULL,
	[stress_speed] [varchar](50) NULL,
	[is_deleted] [bit] NOT NULL,
	[create_dt] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[update_dt] [datetime] NULL,
	[update_user_id] [bigint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sys_db_backup]    Script Date: 2022-11-15 17:32:23 ******/
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
ALTER TABLE [dbo].[base_interface] ADD  CONSTRAINT [DF_base_interface_is_enabled]  DEFAULT ((1)) FOR [is_enabled]
GO
ALTER TABLE [dbo].[base_interface] ADD  CONSTRAINT [DF_base_interface_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[base_interface_relation] ADD  CONSTRAINT [DF_base_interface_relation_is_enabled]  DEFAULT ((1)) FOR [is_enabled]
GO
ALTER TABLE [dbo].[base_interface_relation] ADD  CONSTRAINT [DF_base_interface_relation_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[base_interface_test_item] ADD  CONSTRAINT [DF_base_interface_test_item_is_enabled]  DEFAULT ((1)) FOR [is_enabled]
GO
ALTER TABLE [dbo].[base_interface_test_item] ADD  CONSTRAINT [DF_base_interface_test_item_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[base_org] ADD  CONSTRAINT [DF_base_org_is_enabled]  DEFAULT ((1)) FOR [is_enabled]
GO
ALTER TABLE [dbo].[base_org] ADD  CONSTRAINT [DF_base_org_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[base_test_item] ADD  CONSTRAINT [DF_base_test_item_is_deleted]  DEFAULT ((1)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[base_test_type] ADD  CONSTRAINT [DF_base_test_type_list_num]  DEFAULT ((1)) FOR [list_num]
GO
ALTER TABLE [dbo].[base_test_type] ADD  CONSTRAINT [DF_base_test_type_press_flag]  DEFAULT ((1)) FOR [press_flag]
GO
ALTER TABLE [dbo].[base_test_type] ADD  CONSTRAINT [DF_base_test_type_is_enabled]  DEFAULT ((1)) FOR [is_enabled]
GO
ALTER TABLE [dbo].[base_test_type] ADD  CONSTRAINT [DF_base_test_type_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[base_test_type_item] ADD  CONSTRAINT [DF_base_test_type_item_is_enabled]  DEFAULT ((1)) FOR [is_enabled]
GO
ALTER TABLE [dbo].[base_test_type_item] ADD  CONSTRAINT [DF_base_test_type_item_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[biz_execute_test] ADD  CONSTRAINT [DF_biz_execute_test_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[biz_execute_test_detail] ADD  CONSTRAINT [DF_biz_execute_test_detail_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[biz_interface_import_detail] ADD  CONSTRAINT [DF_base_interface_import_detail_is_enabled]  DEFAULT ((1)) FOR [is_enabled]
GO
ALTER TABLE [dbo].[biz_interface_import_detail] ADD  CONSTRAINT [DF_base_interface_import_detail_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[biz_original_data] ADD  CONSTRAINT [DF_biz_original_data_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[sys_db_backup] ADD  CONSTRAINT [DF_sys_db_backup_is_enabled]  DEFAULT ((1)) FOR [is_enabled]
GO
ALTER TABLE [dbo].[sys_db_backup] ADD  CONSTRAINT [DF_sys_db_backup_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接口名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface', @level2type=N'COLUMN',@level2name=N'interface_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Access数据库文件路径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface', @level2type=N'COLUMN',@level2name=N'access_db_path'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Access数据库名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface', @level2type=N'COLUMN',@level2name=N'access_db_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface', @level2type=N'COLUMN',@level2name=N'is_enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface', @level2type=N'COLUMN',@level2name=N'is_deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface', @level2type=N'COLUMN',@level2name=N'create_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface', @level2type=N'COLUMN',@level2name=N'update_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface', @level2type=N'COLUMN',@level2name=N'update_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接口ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface_relation', @level2type=N'COLUMN',@level2name=N'interface_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检测项编号(base_test_item.test_item_no)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface_relation', @level2type=N'COLUMN',@level2name=N'test_item_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检测项ID(base_interface_relation.id)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface_relation', @level2type=N'COLUMN',@level2name=N'test_item_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检测项名称(base_test_item.test_item_name)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface_relation', @level2type=N'COLUMN',@level2name=N'test_item_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface_relation', @level2type=N'COLUMN',@level2name=N'is_enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface_relation', @level2type=N'COLUMN',@level2name=N'is_deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface_relation', @level2type=N'COLUMN',@level2name=N'create_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface_relation', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface_relation', @level2type=N'COLUMN',@level2name=N'update_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface_relation', @level2type=N'COLUMN',@level2name=N'update_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接口ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface_test_item', @level2type=N'COLUMN',@level2name=N'interface_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检测项名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface_test_item', @level2type=N'COLUMN',@level2name=N'test_item_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface_test_item', @level2type=N'COLUMN',@level2name=N'table_master'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'明细表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface_test_item', @level2type=N'COLUMN',@level2name=N'table_detail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表格点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface_test_item', @level2type=N'COLUMN',@level2name=N'table_dot'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface_test_item', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface_test_item', @level2type=N'COLUMN',@level2name=N'is_enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface_test_item', @level2type=N'COLUMN',@level2name=N'is_deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface_test_item', @level2type=N'COLUMN',@level2name=N'create_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface_test_item', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface_test_item', @level2type=N'COLUMN',@level2name=N'update_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_interface_test_item', @level2type=N'COLUMN',@level2name=N'update_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'机构编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'org_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'机构名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'org_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'等级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'org_level'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'is_enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'is_deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'create_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'update_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'update_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检测项编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_item', @level2type=N'COLUMN',@level2name=N'test_item_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检测项名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_item', @level2type=N'COLUMN',@level2name=N'test_item_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'试材数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_item', @level2type=N'COLUMN',@level2name=N'test_piece_num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'规格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_item', @level2type=N'COLUMN',@level2name=N'spec'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'规格长度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_item', @level2type=N'COLUMN',@level2name=N'spec_length'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'规格宽度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_item', @level2type=N'COLUMN',@level2name=N'spec_width'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'规格高度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_item', @level2type=N'COLUMN',@level2name=N'spect_height'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_item', @level2type=N'COLUMN',@level2name=N'is_deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_item', @level2type=N'COLUMN',@level2name=N'create_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_item', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_item', @level2type=N'COLUMN',@level2name=N'update_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_item', @level2type=N'COLUMN',@level2name=N'update_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检测类型编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type', @level2type=N'COLUMN',@level2name=N'test_type_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检测类型名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type', @level2type=N'COLUMN',@level2name=N'test_type_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type', @level2type=N'COLUMN',@level2name=N'category'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模板文件' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type', @level2type=N'COLUMN',@level2name=N'template_file'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'清单数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type', @level2type=N'COLUMN',@level2name=N'list_num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'样品编号单元格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type', @level2type=N'COLUMN',@level2name=N'sample_no_cells'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序列号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type', @level2type=N'COLUMN',@level2name=N'serial_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'任务锁数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type', @level2type=N'COLUMN',@level2name=N'task_block_num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'抗压标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type', @level2type=N'COLUMN',@level2name=N'press_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type', @level2type=N'COLUMN',@level2name=N'is_enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type', @level2type=N'COLUMN',@level2name=N'is_deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type', @level2type=N'COLUMN',@level2name=N'create_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type', @level2type=N'COLUMN',@level2name=N'update_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type', @level2type=N'COLUMN',@level2name=N'update_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检测类型编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type_item', @level2type=N'COLUMN',@level2name=N'test_type_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检测项编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type_item', @level2type=N'COLUMN',@level2name=N'test_item_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'期限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type_item', @level2type=N'COLUMN',@level2name=N'deadline'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限制' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type_item', @level2type=N'COLUMN',@level2name=N'limit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type_item', @level2type=N'COLUMN',@level2name=N'unit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单元格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type_item', @level2type=N'COLUMN',@level2name=N'cell'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'试验数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type_item', @level2type=N'COLUMN',@level2name=N'test_piece_num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type_item', @level2type=N'COLUMN',@level2name=N'is_enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type_item', @level2type=N'COLUMN',@level2name=N'is_deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type_item', @level2type=N'COLUMN',@level2name=N'create_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type_item', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type_item', @level2type=N'COLUMN',@level2name=N'update_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_test_type_item', @level2type=N'COLUMN',@level2name=N'update_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'机构编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test', @level2type=N'COLUMN',@level2name=N'org_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检测编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test', @level2type=N'COLUMN',@level2name=N'test_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'样品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test', @level2type=N'COLUMN',@level2name=N'sample_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检测类型编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test', @level2type=N'COLUMN',@level2name=N'test_type_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检测项编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test', @level2type=N'COLUMN',@level2name=N'test_item_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'期限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test', @level2type=N'COLUMN',@level2name=N'deadline'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test', @level2type=N'COLUMN',@level2name=N'is_deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test', @level2type=N'COLUMN',@level2name=N'create_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test', @level2type=N'COLUMN',@level2name=N'update_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test', @level2type=N'COLUMN',@level2name=N'update_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实验次数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test_detail', @level2type=N'COLUMN',@level2name=N'experiment_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test_detail', @level2type=N'COLUMN',@level2name=N'play_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'测试规范名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test_detail', @level2type=N'COLUMN',@level2name=N'test_precept_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test_detail', @level2type=N'COLUMN',@level2name=N'file_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'样品形状' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test_detail', @level2type=N'COLUMN',@level2name=N'sample_shape'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区域' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test_detail', @level2type=N'COLUMN',@level2name=N'area'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'测量长度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test_detail', @level2type=N'COLUMN',@level2name=N'gauge_length'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'向上弯曲点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test_detail', @level2type=N'COLUMN',@level2name=N'up_yield_dot'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'向下弯曲点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test_detail', @level2type=N'COLUMN',@level2name=N'down_yield_dot'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最大点值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test_detail', @level2type=N'COLUMN',@level2name=N'max_dot'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'样品宽度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test_detail', @level2type=N'COLUMN',@level2name=N'sample_width'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'样品厚度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test_detail', @level2type=N'COLUMN',@level2name=N'sample_thick'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'样品直径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test_detail', @level2type=N'COLUMN',@level2name=N'sample_dia'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'样品最小直径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test_detail', @level2type=N'COLUMN',@level2name=N'sample_min_dia'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'样品外径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test_detail', @level2type=N'COLUMN',@level2name=N'sample_out_dia'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'样品内径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test_detail', @level2type=N'COLUMN',@level2name=N'sample_inner_dia'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使变形的传感器名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test_detail', @level2type=N'COLUMN',@level2name=N'deform_sensor_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test_detail', @level2type=N'COLUMN',@level2name=N'is_deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test_detail', @level2type=N'COLUMN',@level2name=N'create_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test_detail', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test_detail', @level2type=N'COLUMN',@level2name=N'update_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_execute_test_detail', @level2type=N'COLUMN',@level2name=N'update_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接口ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_interface_import_detail', @level2type=N'COLUMN',@level2name=N'interface_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检测项编号(base_test_item.test_item_no)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_interface_import_detail', @level2type=N'COLUMN',@level2name=N'test_item_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检测项ID(base_interface_test_item.id)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_interface_import_detail', @level2type=N'COLUMN',@level2name=N'test_item_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'检测编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_interface_import_detail', @level2type=N'COLUMN',@level2name=N'test_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'样品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_interface_import_detail', @level2type=N'COLUMN',@level2name=N'sample_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_interface_import_detail', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_interface_import_detail', @level2type=N'COLUMN',@level2name=N'is_enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_interface_import_detail', @level2type=N'COLUMN',@level2name=N'is_deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_interface_import_detail', @level2type=N'COLUMN',@level2name=N'create_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_interface_import_detail', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_interface_import_detail', @level2type=N'COLUMN',@level2name=N'update_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_interface_import_detail', @level2type=N'COLUMN',@level2name=N'update_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实验次数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_original_data', @level2type=N'COLUMN',@level2name=N'experiment_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_original_data', @level2type=N'COLUMN',@level2name=N'play_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'负载值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_original_data', @level2type=N'COLUMN',@level2name=N'load_value'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'位置值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_original_data', @level2type=N'COLUMN',@level2name=N'position_value'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'扩展值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_original_data', @level2type=N'COLUMN',@level2name=N'extend_value'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最大变形值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_original_data', @level2type=N'COLUMN',@level2name=N'big_deform_value'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'变形开关' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_original_data', @level2type=N'COLUMN',@level2name=N'deform_switch'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'控制步骤' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_original_data', @level2type=N'COLUMN',@level2name=N'ctrl_step'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'扩展设备1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_original_data', @level2type=N'COLUMN',@level2name=N'extend_device1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'扩展设备2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_original_data', @level2type=N'COLUMN',@level2name=N'extend_device2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'扩展设备3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_original_data', @level2type=N'COLUMN',@level2name=N'extend_device3'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'扩展设备4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_original_data', @level2type=N'COLUMN',@level2name=N'extend_device4'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'扩展设备5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_original_data', @level2type=N'COLUMN',@level2name=N'extend_device5'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'扩展设备6' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_original_data', @level2type=N'COLUMN',@level2name=N'extend_device6'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'摆放速度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_original_data', @level2type=N'COLUMN',@level2name=N'posi_speed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'压力速度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_original_data', @level2type=N'COLUMN',@level2name=N'stress_speed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除 0-否 1-是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_original_data', @level2type=N'COLUMN',@level2name=N'is_deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_original_data', @level2type=N'COLUMN',@level2name=N'create_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_original_data', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_original_data', @level2type=N'COLUMN',@level2name=N'update_dt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'biz_original_data', @level2type=N'COLUMN',@level2name=N'update_user_id'
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
